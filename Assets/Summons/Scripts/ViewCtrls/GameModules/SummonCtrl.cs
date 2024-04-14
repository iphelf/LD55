using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    public class SummonCtrl : MonoBehaviour
    {
        public UnityEvent onSummonComplete = new();
        [SerializeField] private Slider slider;
        public int steps = 8;
        public bool nextIsLeft = true;
        [SerializeField] private GameObject content;
        private bool _fPressed;
        private float _increment; //总进度条为100
        private bool _jPressed;
        private bool _running;


        private void Start()
        {
            _increment = slider.maxValue / steps;
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void Update()
        {
            if (!_running) return;

            if ((nextIsLeft && StepLeft())
                || (!nextIsLeft && StepRight()))
            {
                slider.value += _increment;
                nextIsLeft = !nextIsLeft;
            }
        }

        private bool StepLeft()
        {
            return Input.GetKeyDown(KeyCode.F);
        }

        private bool StepRight()
        {
            return Input.GetKeyDown(KeyCode.J);
        }

        private void OnSliderValueChanged(float value)
        {
            if (Mathf.Approximately(value, slider.maxValue))
            {
                content.SetActive(false);
                slider.value = 0f;
                _running = false;

                onSummonComplete.Invoke();
            }
        }

        public void Run()
        {
            content.SetActive(true);
            // 开启交互进度条
            _running = true;
        }
    }
}