using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls
{
    
    public class ProgressBarCtrl : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private bool fPressed = false;
        private bool jPressed = false;
        private float addvalue = 5; //总进度条为100


        private void Start()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                fPressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                jPressed = true;
            }

            if (fPressed && jPressed)
            {
                IncreaseProgress(addvalue);
                
                fPressed = false;
                jPressed = false;
            }
            
        }

        void IncreaseProgress(float amount)
        {
            float newValue = Mathf.Clamp01(slider.value + amount / 100f);
            slider.value = newValue;
        }
        void OnSliderValueChanged(float value)
        {
            if (Mathf.Approximately(value, slider.maxValue))
            {
                Debug.Log("Slider is at max value!");
                // 这里可以调用你的触发函数
            }
        }
    }
}
