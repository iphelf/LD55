using System;
using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class mathClassCtrl : MonoBehaviour, IMiniGameCtrl
    {
        [SerializeField] private TextMeshProUGUI mathQuestionText;
        [SerializeField] private TMP_InputField answerText;
        [SerializeField] private TextMeshProUGUI scoreShowText;
        [SerializeField] private TextMeshProUGUI levelShowText;
        [SerializeField] private Button submit;
        private string answer;
        private string[] answerOptions;
        private int levelmath = 1;
        private string[] questionOptions;
        private int score;

        private Action _onComplete;

        private void Start()
        {
            initialize();
            submit.onClick.AddListener(checkAnswer);
            //通关加levelwerText)
        }

        private void SetQuestionOptions(int levelCount)
        {
            questionOptions = new string[2];
            answerOptions = new string[2];
            var head = 0;
            var head2 = 0;
            var rear1 = 0;
            var rear2 = 0;
            if (levelCount == 1) //只写了第一关
            {
                head = Random.Range(1, 10);
                rear1 = Random.Range(1, 10);
                rear2 = Random.Range(0, head);
                questionOptions[0] = head + " + " + rear1 + " = ? ";
                questionOptions[1] = head + " - " + rear2 + " = ? ";
                answerOptions[0] = (head + rear1).ToString();
                answerOptions[1] = (head - rear2).ToString();
            }

            if (levelCount == 2)
            {
                head = Random.Range(10, 100);
                rear1 = Random.Range(10, 100);
                rear2 = Random.Range(0, head);
                questionOptions[0] = head + " + " + rear1 + " = ? ";
                questionOptions[1] = head + " - " + rear2 + " = ? ";
                answerOptions[0] = (head + rear1).ToString();
                answerOptions[1] = (head - rear2).ToString();
            }

            // if (levelCount > 3) Debug.Log("结束游戏");

            if (levelCount == 3)
            {
                head = Random.Range(1, 10);
                head2 = Random.Range(1, 20);
                rear1 = Random.Range(1, 10);
                do
                {
                    rear2 = Random.Range(1, 10);
                } while (head2 % rear2 != 0);

                questionOptions[0] = head + " * " + rear1 + " = ? ";
                questionOptions[1] = head2 + " / " + rear2 + " = ? ";
                answerOptions[0] = (head * rear1).ToString();
                answerOptions[1] = (head2 / rear2).ToString();
            }
        }

        private string ShowQuestion()
        {
            SetQuestionOptions(levelmath);
            if (questionOptions != null && questionOptions.Length > 0)
            {
                var randomTextIndex = Random.Range(0, questionOptions.Length);
                mathQuestionText.text = questionOptions[randomTextIndex];
                answer = answerOptions[randomTextIndex];
                return mathQuestionText.text;
                // Debug.Log("随机生成成功");
            }

            // Debug.Log("选项随机失败");
            return null;
        }

        private void initialize()
        {
            ShowQuestion();
            levelShowText.text = "Level: " + levelmath + " / 3";
            scoreShowText.text = "Score: " + score + " / 3";
            answerText.text = "";
        }

        private void checkAnswer()
        {
            AudioManager.PlaySfx(SfxKey.MiniGameClick);
            if (answerText.text.Replace(" ", "") == answer)
            {
                score++;
                AudioManager.PlaySfx(SfxKey.MiniGameScore);
                // Debug.Log("答案正确");
            }

            if (score == 3)
            {
                // levelmath++;
                score = 0;
                _onComplete();
            }

            initialize();
        }

        public void Setup(QuestArgs args, Action onComplete)
        {
            var questArgsOfDoMathQuiz = args as QuestArgsOfDoMathQuiz;
            _onComplete = onComplete;
            levelmath = questArgsOfDoMathQuiz!.Level;
            initialize();
        }
    }
}