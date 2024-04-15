using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.GameModules;
using UnityEngine;

namespace Summons.Scripts.SceneCtrls
{
    public class GameCtrl : MonoBehaviour
    {
        [SerializeField] private PlaceManager placeManager;
        [SerializeField] private DialogManager dialogManager;
        [SerializeField] private QuestsCtrl questsCtrl;
        [SerializeField] private MapCtrl mapCtrl;
        [SerializeField] private SummonCtrl summonCtrl;
        [SerializeField] private StatsManager statsManager;

        private PlaceType _summonTarget;

        private void Start()
        {
            mapCtrl.onHeadForPlace.AddListener(StartSummoning);
            mapCtrl.Hide();
        }

        private void StartSummoning(PlaceType placeType)
        {
            if (placeType == PlaceManager.Current) return;
            _summonTarget = placeType;
            summonCtrl.onSummonComplete.AddListener(FinishSummoning);
            summonCtrl.Run();
        }

        private void FinishSummoning()
        {
            mapCtrl.Hide();
            placeManager.Load(_summonTarget);
        }
    }
}