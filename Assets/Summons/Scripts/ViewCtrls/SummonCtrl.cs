using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls
{
    public class SummonCtrl : MonoBehaviour
    {
        public UnityEvent onSummonComplete = new();
        [SerializeField] private Slider slider;
        private bool _fPressed;
        private bool _jPressed;
        public int steps = 8;
        public bool nextIsLeft = true;
        private float _increment; //总进度条为100
        [SerializeField] private GameObject content;
        private bool _running;


        private void Start()
        {
            _increment = slider.maxValue / steps;
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private bool StepLeft()
        {
            return Input.GetKeyDown(KeyCode.F);
        }

        private bool StepRight()
        {
            return Input.GetKeyDown(KeyCode.J);
        }

        void Update()
        {
            if (!_running) return;

            if ((nextIsLeft && StepLeft())
                || (!nextIsLeft && StepRight()))
            {
                slider.value += _increment;
                nextIsLeft = !nextIsLeft;
            }
        }

        void OnSliderValueChanged(float value)
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