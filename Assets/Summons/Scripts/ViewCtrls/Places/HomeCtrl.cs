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
        [SerializeField] private BoxGameCtrl contentB;
        [SerializeField] private Button cleanstart;
        [SerializeField] private Button boxstart;
        [SerializeField] private Button hideclean;
        [SerializeField] private Button hidebox;
        [SerializeField] private Canvas canvas;

        private void Start()
        {
            contentA.gameObject.SetActive(false);
            contentB.gameObject.SetActive(false);

            hideclean.onClick.AddListener(() => { contentA.gameObject.SetActive(!contentA.gameObject.activeSelf); });
            hidebox.onClick.AddListener(() => { contentB.gameObject.SetActive(!contentB.gameObject.activeSelf); });
            canvas.worldCamera = Camera.main;
            cleanstart.onClick.AddListener(() =>
            {
                var questInfo = QuestManager.GetNextOngoingQuestOfType(QuestType.WipeStains);
                if (questInfo == null) return;
                contentA.gameObject.SetActive(true);
                LaunchMiniGameForQuest(
                    contentA, questInfo, () => { contentA.gameObject.SetActive(false); }
                );
            });
            boxstart.onClick.AddListener(() =>
            {
                var questInfo = QuestManager.GetNextOngoingQuestOfType(QuestType.OrganizeStuff);
                if (questInfo == null) return;
                contentB.gameObject.SetActive(true);
                LaunchMiniGameForQuest(
                    contentB, questInfo, () => { contentB.gameObject.SetActive(false); }
                );
            });
        }
    }
}