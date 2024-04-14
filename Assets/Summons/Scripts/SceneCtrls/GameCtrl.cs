using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls;
using UnityEngine;

namespace Summons.Scripts.SceneCtrls
{
    public class GameCtrl : MonoBehaviour
    {
        [SerializeField] private PlaceManager placeManager;
        [SerializeField] private DialogsCtrl dialogsCtrl;
        [SerializeField] private QuestsCtrl questsCtrl;
        [SerializeField] private MapCtrl mapCtrl;
        [SerializeField] private SummonCtrl summonCtrl;

        private void Start()
        {
            mapCtrl.onHeadForPlace.AddListener(StartSummoning);
            mapCtrl.Hide();
            dialogsCtrl.gameObject.SetActive(false);
        }

        private PlaceType _summonTarget;

        private void StartSummoning(PlaceType placeType)
        {
            _summonTarget = placeType;
            summonCtrl.onSummonComplete.AddListener(FinishSummoning);
            summonCtrl.Run();
        }

        private void FinishSummoning()
        {
            placeManager.Load(_summonTarget);
        }
    }
}