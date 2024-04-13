using Summons.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.SceneCtrls
{
    public class CreditsCtrl : MonoBehaviour
    {
        [SerializeField] private Button closeButton;

        private void Start()
        {
            closeButton.onClick.AddListener(GameManager.CloseCreditsAndOpenTitle);
        }
    }
}