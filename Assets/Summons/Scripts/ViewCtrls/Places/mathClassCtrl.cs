using System.Collections;
using System.Collections.Generic;
using Summons.Scripts.ViewCtrls;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class mathClassCtrl : PlaceCtrlBase
{
    [SerializeField] private TextMeshProUGUI mathQuestionText;
    [SerializeField] private TMP_InputField answerText;
    [SerializeField] private TextMeshProUGUI scoreShowText;
    [SerializeField] private TextMeshProUGUI levelShowText;
    private string[] questionOptions;
    [SerializeField] private Button submit;
    private string[] answerOptions;
    private int levelmath=1;
    private string answer;
    private int score = 0;
    void SetQuestionOptions(int levelCount)
    {
        questionOptions = new string[2];
        answerOptions = new string[2];
        int head = 0;
        int head2 = 0;
        int rear1 = 0;
        int rear2 = 0;
        if (levelCount == 1)//只写了第一关
        {
            head = Random.Range(1, 10);
            rear1 = Random.Range(1, 10);
            rear2 = Random.Range(0, head);
            questionOptions[0] = head.ToString() + " + " +rear1.ToString()+ " = ? ";
            questionOptions[1] = head.ToString() + " - " + rear2.ToString()+ " = ? ";
            answerOptions[0] = (head + rear1).ToString();
            answerOptions[1] = (head - rear2).ToString();
        }
        if (levelCount == 2)
        {
            head = Random.Range(10, 100);
            rear1 = Random.Range(10, 100);
            rear2 = Random.Range(0, head);
            questionOptions[0] = head.ToString() + " + " +rear1.ToString()+ " = ? ";
            questionOptions[1] = head.ToString() + " - " + rear2.ToString()+ " = ? ";
            answerOptions[0] = (head + rear1).ToString();
            answerOptions[1] = (head - rear2).ToString();
        }

        if (levelCount > 3)
        {
            Debug.Log("结束游戏");
        }

        if (levelCount == 3)
        {
            head = Random.Range(1, 10);
            head2 = Random.Range(1, 20);
            rear1 = Random.Range(1, 10);
            do
            {
                rear2 = Random.Range(1, 10);
            } while (head2 % rear2 != 0);
            questionOptions[0] = head.ToString() + " * " +rear1.ToString()+ " = ? ";
            questionOptions[1] = head2.ToString() + " / " + rear2.ToString()+ " = ? ";
            answerOptions[0] = (head * rear1).ToString();
            answerOptions[1] = (head2 / rear2).ToString();
        }
    }
    string ShowQuestion()
    {
        SetQuestionOptions(levelmath);
        if (questionOptions != null && questionOptions.Length > 0)
        {
            int randomTextIndex = Random.Range(0, questionOptions.Length);
            mathQuestionText.text = questionOptions[randomTextIndex];
            answer = answerOptions[randomTextIndex];
            return mathQuestionText.text;
            Debug.Log("随机生成成功");
        }
        Debug.Log("选项随机失败");
        return null;
    }

    void initialize()
    {
        ShowQuestion();
        levelShowText.text = "Level: "+levelmath.ToString() + " / 3";
        scoreShowText.text = "Score: "+score.ToString() + " / 3";
        answerText.text = "";
    }

    void checkAnswer()
    {
        if (answerText.text.Replace(" ","") == answer)
        {
            score++;
            Debug.Log("答案正确");
        }

        if (score == 3)
        {
            levelmath++;
            score = 0;
        }
        initialize();
    }
    void Start()
    {
        initialize();
        submit.onClick.AddListener(checkAnswer);
        //通关加levelwerText)
    }
}
