using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls
{
    public class SummonCtrl : MonoBehaviour
    {
        public UnityEvent onSummonComplete = new();
        [SerializeField] private GameObject content;
        [SerializeField] private Button complete;

        private void Start()
        {
            complete.onClick.AddListener(Complete);
        }

        public void Run()
        {
            content.SetActive(true);
            // 开启交互进度条
        }

        public void Complete()
        {
            content.SetActive(false);
            onSummonComplete.Invoke();
        }
    }
}