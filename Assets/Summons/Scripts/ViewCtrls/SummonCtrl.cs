using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls
{
    public class SummonCtrl : MonoBehaviour
    {
        public UnityEvent onSummonComplete = new();
        [SerializeField] private Slider slider;
        private bool fPressed = false;
        private bool jPressed = false;
        private float addvalue = 5; //总进度条为100
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject map;
        [SerializeField] private GameObject place;


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
                content.SetActive(false);
                place.SetActive(false);
                map.SetActive(true);
                slider.value = 0f;
                

                onSummonComplete.Invoke();
                
            }
        }
        public void Run()
        {
            content.SetActive(true);
            // 开启交互进度条
            map.SetActive(false);
            //关闭地图
            
        }
    }

        

        
    }
