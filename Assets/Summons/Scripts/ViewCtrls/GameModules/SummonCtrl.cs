using UnityEngine;
using UnityEngine.Events;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    public class SummonCtrl : MonoBehaviour
    {
        public UnityEvent onSummonComplete = new();
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject target;
        private Vector3 _sourcePosition;
        private Vector3 _targetPosition;
        public int steps = 8;
        private int _currentProgress = 0;

        private int CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = Mathf.Min(value, steps);
                head.transform.position =
                    _sourcePosition + (_targetPosition - _sourcePosition) / steps * _currentProgress;
            }
        }

        public bool nextIsLeft = true;
        [SerializeField] private GameObject content;
        private bool _running;


        private void Start()
        {
            _sourcePosition = head.transform.position;
            _targetPosition = target.transform.position;
            target.SetActive(false);
        }

        private void Update()
        {
            if (!_running) return;

            if ((nextIsLeft && StepLeft())
                || (!nextIsLeft && StepRight()))
            {
                ++CurrentProgress;
                if (CurrentProgress >= steps)
                {
                    CurrentProgress = 0;
                    content.SetActive(false);
                    _running = false;

                    onSummonComplete.Invoke();
                }

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

        public void Run()
        {
            content.SetActive(true);
            // 开启交互进度条
            _running = true;
        }
    }
}