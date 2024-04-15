using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class HomeCtrl : PlaceCtrlBase
    {
        [SerializeField] private Ggl contentA;
        //[SerializeField] private GameObject contentB;
        [SerializeField] private Button cleanstart;
        [SerializeField] private Button boxstart;
        [SerializeField] private Button hideclean;
        [SerializeField] private Button hidebox;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button completeQuest1;
        [SerializeField] private GameObject statefulContent;

        

        private void Start()
        {
            contentA.gameObject.SetActive(false);
            //contentB.SetActive(false);

            hideclean.onClick.AddListener(() => { contentA.gameObject.SetActive(!contentA.gameObject.activeSelf); });
            //hidebox.onClick.AddListener(() => { contentB.SetActive(!contentB.activeSelf); });
            canvas.worldCamera = Camera.main;
            completeQuest1.onClick.AddListener(() => { QuestManager.EndQuest(1); });
            cleanstart.onClick.AddListener(() =>
            {
                var questInfo = DetectQuestForMiniGame();
                if (questInfo == null) return;
                contentA.gameObject.SetActive(true);
                LaunchMiniGameForQuest(
                    contentA, questInfo, () => { contentA.gameObject.SetActive(false); }
                );
            });
            // box.onClick.AddListener(() =>
            // {
            //     var questInfo = DetectQuestForMiniGame();
            //     if (questInfo == null) return;
            //     contentA.gameObject.SetActive(true);
            //     LaunchMiniGameForQuest(
            //         contentB, questInfo, () => { contentB.gameObject.SetActive(false); }
            //     );
            // });
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
    }
}