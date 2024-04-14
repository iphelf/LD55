using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class DemoPlaceCtrl : PlaceCtrlBase
    {
        [SerializeField] private GameObject statefulContent;
        [SerializeField] private MiniGameCtrlBase miniGame;
        [SerializeField] private Button startButton;
        [SerializeField] private Button toggleButton;

        private void Start()
        {
            miniGame.gameObject.SetActive(false);

            toggleButton.onClick.AddListener(() => { statefulContent.SetActive(!statefulContent.activeSelf); });

            startButton.onClick.AddListener(() =>
            {
                var questInfo = DetectQuestForMiniGame();
                if (questInfo == null) return;
                miniGame.gameObject.SetActive(true);
                LaunchMiniGameForQuest(
                    miniGame, questInfo, () => { miniGame.gameObject.SetActive(false); }
                );
            });
        }

        private QuestInfo DetectQuestForMiniGame()
        {
            if (QuestManager.OngoingQuests.Count == 0) return null;
            int questId = QuestManager.OngoingQuests[0];
            var questInfo = QuestManager.GetQuestInfo(questId);
            return questInfo;
        }

        #region 地方状态信息

        public override void OnEnterPlace(PlaceState state = null)
        {
            if (state is PlaceStateOfDemoPlace placeStateOfDemoPlace)
            {
                var isPresent = placeStateOfDemoPlace.IsPresent;
                statefulContent.SetActive(isPresent);
            }
        }

        public override PlaceState OnExitPlace()
        {
            return new PlaceStateOfDemoPlace
            {
                IsPresent = statefulContent.activeSelf
            };
        }

        #endregion
    }
}