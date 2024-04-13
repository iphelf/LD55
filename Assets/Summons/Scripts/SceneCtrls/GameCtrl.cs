using Summons.Scripts.ViewCtrls;
using UnityEngine;

namespace Summons.Scripts.SceneCtrls
{
    public class GameCtrl : MonoBehaviour
    {
        [SerializeField] private Transform placeRoot;
        [SerializeField] private Transform miniGameRoot;
        [SerializeField] private Transform miniGameUIRoot;
        [SerializeField] private DialogsCtrl dialogsCtrl;
        [SerializeField] private TasksCtrl tasksCtrl;
        [SerializeField] private MapCtrl mapCtrl;
        [SerializeField] private SummonCtrl summonCtrl;
    }
}