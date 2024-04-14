using Summons.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    public class QuestCtrl : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text desc;
        [SerializeField] private Slider progress;
        [SerializeField] private TMP_Text countDown;

        private QuestInfo _questInfo;

        private void Update()
        {
            if (_questInfo == null) return;
            var remainingTime = _questInfo.Duration - _questInfo.Elapsed;
            progress.value = remainingTime;
            countDown.text = Mathf.CeilToInt(remainingTime).ToString();
        }

        public void SetQuest(QuestInfo questInfo)
        {
            _questInfo = questInfo;
            desc.text = _questInfo.Description;
            progress.maxValue = _questInfo.Duration;
        }
    }
}