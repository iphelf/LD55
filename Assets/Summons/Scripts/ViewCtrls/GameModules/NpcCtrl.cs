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

        private void Start()
        {
            _baseScale = transform.localScale;
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
                DialogManager.ShowDefaultMessage(questType);
            else
                DialogManager.ShowSummonMessage(questType, () => { onSummonRespond.Invoke(quest); });
        }
    }
}