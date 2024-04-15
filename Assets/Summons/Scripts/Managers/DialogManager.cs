using System;
using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.Managers
{
    public class DialogManager : MonoBehaviour
    {
        private static Dictionary<QuestType, NpcData> _npcDict;
        private static DialogManager _instance;

        public static void Initialize(NpcConfig config)
        {
            _npcDict = config.ToDict();
        }

        [SerializeField] private GameObject content;
        [SerializeField] private TMP_Text dialog;
        [SerializeField] private Image portrait;
        [SerializeField] private TMP_Text npcName;
        [SerializeField] private Button closeButton;

        private Action _onClose;

        private void Start()
        {
            _instance = this;
            closeButton.onClick.AddListener(OnClose);
            content.SetActive(false);
        }

        public static void ShowDefaultMessage(QuestType questType, Sprite sprite = null, Action onClose = null)
        {
            var npc = _npcDict[questType];
            _instance.content.SetActive(true);
            _instance.portrait.sprite = sprite;
            _instance.dialog.text = npc.DefaultMessage;
            _instance.npcName.text = npc.Name;
            _instance._onClose = onClose;
        }

        public static void ShowSummonMessage(QuestType questType, Sprite sprite = null, Action onClose = null)
        {
            var npc = _npcDict[questType];
            _instance.content.SetActive(true);
            _instance.portrait.sprite = sprite;
            _instance.dialog.text = npc.SummonMessage;
            _instance.npcName.text = npc.Name;
            _instance._onClose = onClose;
        }

        private void OnClose()
        {
            var onClose = _onClose;
            _onClose = null;
            content.SetActive(false);
            onClose?.Invoke();
        }
    }
}