using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Summons.Scripts.ViewCtrls;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class englishClassCtrl : PlaceCtrlBase
{
    [SerializeField] private TextMeshProUGUI questionText; // 显示问题的文本组件
    [SerializeField] private TextMeshProUGUI changeText;
    [SerializeField] private TextMeshProUGUI levelText;// 接收玩家输入的文本组件
    private string answer; // 正确答案
    [SerializeField] private string[] textOptions;
    private int level = 1;
    string inputsum = "";
    string ShowRandomText(int levelnum)
    {
        int head=0;
        int rear=0;
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
            int randomTextIndex = Random.Range(head, rear);
            questionText.text = textOptions[randomTextIndex];
            return questionText.text;
        }
        else
        {
            Debug.LogWarning("No text to show.");
            return " ";
        }
    }

    void Initialize()
    {
        levelText.text = level.ToString() + " / 3";
        Input.ResetInputAxes();
        inputsum = "";
        changeText.text ="";
        changeText.color = Color.black;
        answer=ShowRandomText(level);
        //changeText.text = ShowRandomText();
        Debug.Log("题目：");
        Debug.Log(changeText.text);
        //answer = changeText.text.ToLower();
        Debug.Log("查找answer");
        Debug.Log(answer);
    }
    private void Start()
    {
       Initialize();
    }
    
    private void Update()
    {
        // 检测玩家是否按下了键盘上的任意一个按键
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString.ToLower();
            Debug.Log("玩家按下了键：" + keyPressed);
            if (keyPressed[0] == answer[0])
            {
                answer = answer.Remove(0, 1);
                Debug.Log(changeText.text);
                changeText.text = inputsum + keyPressed ;
                inputsum +=  keyPressed ;
            }

            if (answer.Length == 0)
            {
                Debug.Log("通关");
                level++;
                changeText.text = "";
                Initialize();
                if (level == 3)
                {
                    Debug.Log("结束");
                }
            }
        }
    }
}