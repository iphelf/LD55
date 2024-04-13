using Summons.Scripts.Managers;
using Summons.Scripts.ViewCtrls;
using UnityEngine;

namespace Summons.Scripts.SceneCtrls
{
    public class GameCtrl : MonoBehaviour
    {
        [SerializeField] private PlaceManager placeManager;
        [SerializeField] private DialogsCtrl dialogsCtrl;
        [SerializeField] private TasksCtrl tasksCtrl;
        [SerializeField] private MapCtrl mapCtrl;
        [SerializeField] private SummonCtrl summonCtrl;

        private void Start()
        {
            mapCtrl.onHeadForPlace.AddListener(placeType => { placeManager.Load(placeType); });
            mapCtrl.Hide();
            summonCtrl.gameObject.SetActive(false);
            dialogsCtrl.gameObject.SetActive(false);
        }
    }
}