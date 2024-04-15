using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using Summons.Scripts.ViewCtrls.MiniGames.BallGame;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class CramSchoolCtrl :PlaceCtrlBase
    {// Start is called before the first frame update
        [SerializeField] private GameObject BallGameNPC;
        [SerializeField] private Button BallGameNPCSign;

        [SerializeField] private BallGameCtrl content;
        [SerializeField] private Button ballstart;
        //[SerializeField] private Button hideball;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button completeQuest1;
        [SerializeField] private GameObject statefulContent;



        private void Start()
        {
            content.gameObject.SetActive(false);

           
            canvas.worldCamera = Camera.main;
            completeQuest1.onClick.AddListener(() => { QuestManager.EndQuest(1); });
            ballstart.onClick.AddListener(() =>
            {
                var questInfo = DetectQuestForMiniGame();
                if (questInfo == null) return;
                content.gameObject.SetActive(true);
                LaunchMiniGameForQuest(
                    content, questInfo, () => { content.gameObject.SetActive(false); }
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

        //public void ShowBallGameNPCSign(bool status)
        //{
        //    BallGameNPCSign.gameObject.SetActive(status);
        //}

        //public void StartBallGame()
        //{
        //    BallGameManager.Instance.ShowBallGamePanel(true);
        //    ShowBallGameNPCSign(false);
        //}
    }
}