using System;
using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class PlaceCtrlBase : MonoBehaviour
    {
        public virtual void OnEnterPlace(PlaceState state = null)
        {
        }

        public virtual PlaceState OnExitPlace()
        {
            return null;
        }

        protected bool IsMiniGameRunning { get; private set; }
        private Action _onQuestEnd;

        protected void LaunchMiniGameForQuest(
            IMiniGameCtrl miniGameCtrl, QuestInfo questInfo, Action onQuestEnd = null)
        {
            if (IsMiniGameRunning) return;
            IsMiniGameRunning = true;
            _onQuestEnd = onQuestEnd;
            miniGameCtrl.Setup(questInfo.Args, () =>
            {
                QuestManager.EndQuest(questInfo.Id);
                OnComplete();
            });
        }

        private void OnComplete()
        {
            IsMiniGameRunning = false;
            Action onQuestEnd = _onQuestEnd;
            _onQuestEnd = null;
            onQuestEnd?.Invoke();
        }
    }
}