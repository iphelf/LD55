using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using Summons.Scripts.ViewCtrls.MiniGames.BallGame;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class CramSchoolCtrl :PlaceCtrlBase
    {// Start is called before the first frame update
        [SerializeField] private GameObject BallGameNPC;

        [SerializeField] private BallGameCtrl ballGameCtrl;
        [SerializeField] private Button ballstart;
        //[SerializeField] private Button hideball;
        private QuestInfo _running; // 若不为null，则小游戏正在运行中，且执行的是该任务

        private void Start()
        {
            ballGameCtrl.gameObject.SetActive(false);

            ballstart.onClick.AddListener(() =>
            {
                if (_running != null) return;
                var quest = QuestManager.GetNextOngoingQuestOfType(QuestType.PracticeVolleyball);
                if (quest == null) return;
                _running = quest;
                ballGameCtrl.gameObject.SetActive(true);
                ballGameCtrl.Setup(quest.Args, () =>
                {
                    ballGameCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
                QuestManager.OnQuestEnd.AddListener(OnQuestEnd);
            });

        }

        private void Update()
        {
            //DetectQuestForStartButton();
            bool showsBallEntry = false;
            if (_running == null)
            {
                foreach (var id in QuestManager.OngoingQuests)
                {
                    var quest = QuestManager.GetQuestInfo(id);
                    showsBallEntry = showsBallEntry || quest.Type == QuestType.PracticeVolleyball;
                }
            }

            ballstart.gameObject.SetActive(showsBallEntry);
        }

        private void DetectQuestForStartButton()
        {
            if (QuestManager.OngoingQuests.Count == 0)
            {
                ballstart.gameObject.SetActive(false);
                return;
            }
            var questInfo = QuestManager.GetNextOngoingQuestOfType(QuestType.PracticeVolleyball);
            if (questInfo==null||questInfo.Type != QuestType.PracticeVolleyball) 
            {
                 ballstart.gameObject.SetActive(false);
                 return;
            }
            if(_running==null)ballstart.gameObject.SetActive(true);
            else ballstart.gameObject.SetActive(false);
        }
        private QuestInfo DetectQuestForMiniGame()
        {
            if (QuestManager.OngoingQuests.Count == 0) return null;
            var questInfo = QuestManager.GetNextOngoingQuestOfType(QuestType.PracticeVolleyball);
            if (questInfo==null) return null;
            return questInfo;
        }
        public override void OnEnterPlace(PlaceState state = null)
        {
            if (state is PlaceStateOfDemoPlace placeStateOfDemoPlace)
            {
                var isPresent = placeStateOfDemoPlace.IsPresent;
            }
        }

        public override PlaceState OnExitPlace()
        {
            return new PlaceStateOfDemoPlace
            {
            };
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