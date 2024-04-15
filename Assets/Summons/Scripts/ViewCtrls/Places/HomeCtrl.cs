using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class HomeCtrl : PlaceCtrlBase
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CleanGameCtrl cleanGameCtrl;
        [SerializeField] private BoxGameCtrl boxGameCtrl;
        [SerializeField] private Button cleanstart;
        [SerializeField] private Button boxstart;
        private QuestInfo _running; // ���Ϊnull����С��Ϸ���������У���ִ�е��Ǹ�����

        private void Start()
        {
            canvas.worldCamera = Camera.main;
            boxstart.onClick.AddListener(() =>
            {
                if (_running != null) return;
                var quest = QuestManager.GetNextOngoingQuestOfType(QuestType.OrganizeStuff);
                if (quest == null) return;
                _running = quest;
                boxGameCtrl.gameObject.SetActive(true);
                boxGameCtrl.Setup(quest.Args, () =>
                {
                    boxGameCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            cleanstart.onClick.AddListener(() =>
            {
                if (_running != null) return;
                var quest = QuestManager.GetNextOngoingQuestOfType(QuestType.WipeStains);
                if (quest == null) return;
                _running = quest;
                cleanGameCtrl.gameObject.SetActive(true);
                cleanGameCtrl.Setup(quest.Args, () =>
                {
                    cleanGameCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            boxGameCtrl.gameObject.SetActive(false);
            cleanGameCtrl.gameObject.SetActive(false);
            QuestManager.OnQuestEnd.AddListener(OnQuestEnd);
        }

        private void Update()
        {
            bool showsCleanEntry = false;
            bool showsBoxEntry = false;
            if (_running == null)
            {
                foreach (var id in QuestManager.OngoingQuests)
                {
                    var quest = QuestManager.GetQuestInfo(id);
                    showsCleanEntry = showsCleanEntry || quest.Type == QuestType.WipeStains;
                    showsBoxEntry = showsBoxEntry || quest.Type == QuestType.OrganizeStuff;
                }
            }

            boxstart.gameObject.SetActive(showsBoxEntry);
            cleanstart.gameObject.SetActive(showsCleanEntry);
        }

        private void OnDestroy()
        {
            QuestManager.OnQuestEnd.RemoveListener(OnQuestEnd);
        }

        private void OnQuestEnd(int id)
        {
            if (_running?.Id != id) return;

            if (_running.Type == QuestType.OrganizeStuff)
            {
               boxGameCtrl.gameObject.SetActive(false);
            }
            else if (_running.Type == QuestType.WipeStains)
            {
                cleanGameCtrl.gameObject.SetActive(false);
            }

            _running = null;
        }
    }
}