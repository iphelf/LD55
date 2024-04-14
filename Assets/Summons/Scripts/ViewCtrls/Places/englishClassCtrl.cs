using TMPro;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class englishClassCtrl : PlaceCtrlBase
    {
        [SerializeField] private TextMeshProUGUI questionText; // 显示问题的文本组件
        [SerializeField] private TextMeshProUGUI changeText;
        [SerializeField] private TextMeshProUGUI levelText; // 接收玩家输入的文本组件
        [SerializeField] private string[] textOptions;
        private string answer; // 正确答案
        private string inputsum = "";
        private int level = 1;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            // 检测玩家是否按下了键盘上的任意一个按键
            if (Input.anyKeyDown)
            {
                var keyPressed = Input.inputString.ToLower();
                // Debug.Log("玩家按下了键：" + keyPressed);
                if (keyPressed[0] == answer[0])
                {
                    answer = answer.Remove(0, 1);
                    // Debug.Log(changeText.text);
                    changeText.text = inputsum + keyPressed;
                    inputsum += keyPressed;
                }

                if (answer.Length == 0)
                {
                    // Debug.Log("通关");
                    level++;
                    changeText.text = "";
                    Initialize();
                    // if (level == 3) Debug.Log("结束");
                }
            }
        }

        private string ShowRandomText(int levelnum)
        {
            var head = 0;
            var rear = 0;
            if (levelnum == 1)
            {
                head = 0;
                rear = 1;
            }

            if (levelnum == 2)
            {
                head = 2;
                rear = 5;
            }

            if (levelnum == 3)
            {
                head = 6;
                rear = 7;
            }

            if (textOptions != null && textOptions.Length > 0)
            {
                var randomTextIndex = Random.Range(head, rear);
                questionText.text = textOptions[randomTextIndex];
                return questionText.text;
            }

            // Debug.LogWarning("No text to show.");
            return " ";
        }

        private void Initialize()
        {
            levelText.text = level + " / 3";
            Input.ResetInputAxes();
            inputsum = "";
            changeText.text = "";
            changeText.color = Color.black;
            answer = ShowRandomText(level);
            //changeText.text = ShowRandomText();
            // Debug.Log("题目：");
            // Debug.Log(changeText.text);
            //answer = changeText.text.ToLower();
            // Debug.Log("查找answer");
            // Debug.Log(answer);
        }
    }
}