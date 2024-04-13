using Summons.Scripts.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls
{
    public class MapCtrl : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Button goHome;
        [SerializeField] private Button goToSchool;
        [SerializeField] private Button goToShop;
        [SerializeField] private Button goToInterest;
        [SerializeField] private Button toggle;
        public UnityEvent<PlaceType> onHeadForPlace;

        private void Start()
        {
            goHome.onClick.AddListener(() => onHeadForPlace.Invoke(PlaceType.Home));
            goToSchool.onClick.AddListener(() => onHeadForPlace.Invoke(PlaceType.School));
            goToShop.onClick.AddListener(() => onHeadForPlace.Invoke(PlaceType.Shop));
            goToInterest.onClick.AddListener(() => onHeadForPlace.Invoke(PlaceType.Interest));
            toggle.onClick.AddListener(() =>
            {
                if (content.activeSelf) Hide();
                else Show();
            });
        }

        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }
    }
}