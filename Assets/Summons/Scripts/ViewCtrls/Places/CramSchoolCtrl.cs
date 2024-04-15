using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.GameModules;
using Summons.Scripts.ViewCtrls.MiniGames.BallGame;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class CramSchoolCtrl : PlaceCtrlBase
    {
        [SerializeField] private NpcCtrl sportsTeacher;
        [SerializeField] private BallGameCtrl ballGameCtrl;

        private QuestInfo _running; // 若不为null，则小游戏正在运行中，且执行的是该任务

        private void Start()
        {
            ballGameCtrl.gameObject.SetActive(false);

            sportsTeacher.onSummonRespond.AddListener(quest =>
            {
                if (_running != null) return;
                _running = quest;
                ballGameCtrl.gameObject.SetActive(true);
                ballGameCtrl.Setup(quest.Args, () =>
                {
                    ballGameCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            QuestManager.OnQuestEnd.AddListener(OnQuestEnd);
        }

        private void Update()
        {
            bool allowInput = _running == null;
            sportsTeacher.Interactable = allowInput;
        }

        private void OnDestroy()
        {
            QuestManager.OnQuestEnd.RemoveListener(OnQuestEnd);
        }

        private void OnQuestEnd(int id)
        {
            if (_running?.Id != id) return;

            if (_running.Type == QuestType.PracticeVolleyball)
            {
                ballGameCtrl.gameObject.SetActive(false);
            }

            _running = null;
        }
    }
}