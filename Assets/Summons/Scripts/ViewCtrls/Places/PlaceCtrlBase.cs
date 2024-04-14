using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class PlaceCtrlBase : MonoBehaviour
    {
        public virtual void OnEnterPlace(PlaceState state = null)
        {
        }

        public virtual PlaceState OnExitPlace()
        {
            return null;
        }
    }
}