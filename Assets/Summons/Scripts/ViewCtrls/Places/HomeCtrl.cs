using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class HomeCtrl : PlaceCtrlBase
    {
        [SerializeField] private GameObject contentA;
        [SerializeField] private GameObject contentB;
        [SerializeField] private Button clean;
        [SerializeField] private Button box;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button completeQuest1;

        // public override void OnEnterPlace(PlaceState state = null)
        // {
        //     if (state is PlaceStateOfDemoPlace placeStateOfDemoPlace)
        //     {
        //         bool hidden = placeStateOfDemoPlace.Hidden;
        //         contentA.SetActive(hidden);
        //         contentB.SetActive(hidden);
        //     }
        // }
        //
        // public override PlaceState OnExitPlace()
        // {
        //     return new PlaceStateOfDemoPlace
        //     {
        //         Hidden = contentA.activeSelf
        //     };
        // }

        private void Start()
        {
            contentA.SetActive(false);
            contentB.SetActive(false);
            
            clean.onClick.AddListener(() =>
            {
                contentA.SetActive(!contentA.activeSelf);
            });
            box.onClick.AddListener(() =>
            {
                contentB.SetActive(!contentB.activeSelf);
            });
            canvas.worldCamera = Camera.main;
            completeQuest1.onClick.AddListener(() => { QuestManager.EndQuest(1); });
        }
    }
}