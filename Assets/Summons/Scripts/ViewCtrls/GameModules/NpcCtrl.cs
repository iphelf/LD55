using System;
using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class NpcCtrl : MonoBehaviour
    {
        [SerializeField] private QuestType questType = QuestType.None;
        [SerializeField] private GameObject questHint;
        private const float ScaleOnHover = 1.03f;
        private Vector3 _baseScale;
        public UnityEvent<QuestInfo> onSummonRespond = new();

        private bool _interactable = true;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value) return;
                if (value == false)
                    transform.localScale = _baseScale;
                _interactable = value;
            }
        }

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _baseScale = transform.localScale;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (questHint != null)
            {
                var quest = QuestManager.GetNextOngoingQuestOfType(questType);
                questHint.SetActive(quest is not null);
            }
        }

        private void OnMouseEnter()
        {
            if (!_interactable) return;
            transform.localScale = _baseScale * ScaleOnHover;
        }

        private void OnMouseExit()
        {
            if (!_interactable) return;
            transform.localScale = _baseScale;
        }

        private void OnMouseUpAsButton()
        {
            if (!_interactable) return;
            var quest = QuestManager.GetNextOngoingQuestOfType(questType);
            if (quest is null || questType == QuestType.PurchaseItem)
                DialogManager.ShowDefaultMessage(questType, _spriteRenderer.sprite);
            else
                DialogManager.ShowSummonMessage(
                    questType, _spriteRenderer.sprite, () => { onSummonRespond.Invoke(quest); });
        }
    }
}