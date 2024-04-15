using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.GameModules;
using Summons.Scripts.ViewCtrls.MiniGames;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class HomeCtrl : PlaceCtrlBase
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CleanGameCtrl cleanGameCtrl;
        [SerializeField] private BoxGameCtrl boxGameCtrl;
        [SerializeField] private NpcCtrl father;
        [SerializeField] private NpcCtrl mother;
        private QuestInfo _running;

        private void Start()
        {
            canvas.worldCamera = Camera.main;
            mother.onSummonRespond.AddListener(quest =>
            {
                if (_running != null) return;
                _running = quest;
                boxGameCtrl.gameObject.SetActive(true);
                boxGameCtrl.Setup(quest.Args, () =>
                {
                    boxGameCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            father.onSummonRespond.AddListener(quest =>
            {
                if (_running != null) return;
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
            bool allowInput = _running == null;
            mother.Interactable = allowInput;
            father.Interactable = allowInput;
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