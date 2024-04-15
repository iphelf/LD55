using UnityEngine;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    public class HeartsCtrl : MonoBehaviour
    {
        [SerializeField] private Transform heartsRoot;
        [SerializeField] private GameObject heartPrefab;

        public void SetHearts(int heartCount)
        {
            if (heartsRoot.childCount > heartCount)
                for (int i = heartsRoot.childCount - 1; i >= heartCount; --i)
                    Destroy(heartsRoot.GetChild(i).gameObject);
            else if (heartsRoot.childCount < heartCount)
                for (int i = heartsRoot.childCount; i < heartCount; ++i)
                    Instantiate(heartPrefab, heartsRoot);
        }
    }
}