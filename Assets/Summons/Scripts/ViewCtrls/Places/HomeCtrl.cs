using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class HomeCtrl : PlaceCtrlBase
    {
        [SerializeField] private Ggl ggl;
        [SerializeField] private BoxGameCtrl boxGameCtrl;
        [SerializeField] private Button cleanstart;
        [SerializeField] private Button boxstart;
        private QuestInfo _running; // 若不为null，则小游戏正在运行中，且执行的是该任务

        private void Start()
        {
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
                ggl.gameObject.SetActive(true);
                ggl.Setup(quest.Args, () =>
                {
                    ggl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            boxGameCtrl.gameObject.SetActive(false);
            ggl.gameObject.SetActive(false);
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
                ggl.gameObject.SetActive(false);
            }

            _running = null;
        }
    }
}