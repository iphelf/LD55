using System.Collections.Generic;
using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class ShopCtrl : PlaceCtrlBase
    {
        [SerializeField] private Button item1;
        [SerializeField] private Button item2;
        [SerializeField] private Button item3;
        private readonly Dictionary<int, Button> _buttonDict = new();
        private const int ItemCount = 3;

        private void Start()
        {
            _buttonDict.Add(1, item1);
            _buttonDict.Add(2, item2);
            _buttonDict.Add(3, item3);
            for (int i = 1; i <= ItemCount; ++i)
            {
                if (_state is not null && _state.PurchasedItems.Contains(i))
                {
                    _buttonDict[i].gameObject.SetActive(false);
                }
                else
                {
                    int item = i;
                    _buttonDict[i].onClick.AddListener(() => OnClickItem(item));
                }
            }
        }

        private class ShopState : PlaceState
        {
            public readonly HashSet<int> PurchasedItems = new();
        }

        private ShopState _state;

        public override void OnEnterPlace(PlaceState state = null)
        {
            _state = state as ShopState;
            _state ??= new ShopState();
        }

        public override PlaceState OnExitPlace()
        {
            return _state;
        }

        private void Update()
        {
            bool[] itemIsDesired = new bool[ItemCount + 1];
            foreach (var questId in QuestManager.OngoingQuests)
            {
                var quest = QuestManager.GetQuestInfo(questId);
                if (quest.Type != QuestType.PurchaseItem) continue;
                var args = quest.Args as QuestArgsOfPurchaseItem;
                int targetItem = args!.TargetItem;
                itemIsDesired[targetItem] = true;
            }

            for (int i = 1; i <= ItemCount; ++i)
                _buttonDict[i].interactable = itemIsDesired[i];
        }

        private void OnClickItem(int item)
        {
            bool success = false;
            foreach (var questId in QuestManager.OngoingQuests)
            {
                var quest = QuestManager.GetQuestInfo(questId);
                if (quest.Type != QuestType.PurchaseItem) continue;
                var args = quest.Args as QuestArgsOfPurchaseItem;
                int targetItem = args!.TargetItem;
                if (item == targetItem)
                {
                    success = true;
                    QuestManager.EndQuest(questId);
                }
            }

            if (success)
            {
                AudioManager.PlaySfx(SfxKey.SceneInteractiveClick);
                _buttonDict[item].gameObject.SetActive(false);
                _state.PurchasedItems.Add(item);
            }
        }
    }
}