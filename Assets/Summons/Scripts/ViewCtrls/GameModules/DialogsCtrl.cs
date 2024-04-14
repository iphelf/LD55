using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    public class DialogsCtrl : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private TMP_Text dialog;
        [SerializeField] private Image portrait;
        [SerializeField] private Button closeButton;

        public UnityEvent onClose = new();

        private void Start()
        {
            closeButton.onClick.AddListener(OnClose);
        }

        public void Show(string message)
        {
            content.SetActive(true);
            dialog.text = message;
        }

        private void OnClose()
        {
            onClose.Invoke();
            content.SetActive(false);
        }
    }
}