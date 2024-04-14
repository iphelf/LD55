using System;
using Summons.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.MiniGames
{
    public class DemoMiniGame : MonoBehaviour, IMiniGameCtrl
    {
        [SerializeField] private TMP_Text questInfoText;
        [SerializeField] private Button completeButton;
        private Action _onComplete;

        private void Start()
        {
            completeButton.onClick.AddListener(() =>
            {
                Debug.Log("小游戏完成了");
                _onComplete?.Invoke();
            });
        }

        public void Setup(QuestArgs args, Action onComplete)
        {
            questInfoText.text = args.GetType().Name;
            _onComplete = onComplete;
        }
    }
}