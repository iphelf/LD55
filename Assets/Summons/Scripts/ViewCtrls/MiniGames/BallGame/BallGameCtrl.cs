using System;
using System.Collections;
using Summons.Scripts.General;
using Summons.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames.BallGame
{
    public class BallGameCtrl : Singleton<BallGameCtrl>, IMiniGameCtrl
    {
        //[SerializeField] private Button completeButton;
        private Action _onComplete;

        public void Setup(QuestArgs args, Action onComplete)
        {
            var questArgs = args as QuestArgsOfPracticeVolleyball;
            winScore = questArgs!.WinScore;
            _onComplete = onComplete;
        }

        [SerializeField] private int score;
        [SerializeField] private TextMeshProUGUI scoreTMP;
        public int winScore = 10;


        public Action OnComplete;

        // Update is called once per frame
        private void Update()
        {
            DetectGameOver();
            scoreTMP.text = $"Score: {score}/{winScore}";
        }

        public void AddScore(int addscore)
        {
            score += addscore;
        }

        public void SubScore(int subscore)
        {
            score -= subscore;
        }

        public void RestartBallGame(Ball ball)
        {
            StartCoroutine(RestartBall1(ball));
        }

        private IEnumerator RestartBall1(Ball ball)
        {
            ball.transform.position = ball.StartPosition;
            ball.Movement = ball.StartPosition;
            yield return new WaitForSeconds(2f);
            ball.gameObject.SetActive(true);
        }

        public void ShowBallGamePanel(bool status)
        {
            gameObject.SetActive(status);
            // Debug.Log("点1");
        }

        public void BallGameWin()
        {
            _onComplete();
            ShowBallGamePanel(false);
            //CramSchoolCtrl.Instance.ShowBallGameNPCSign(false);
            // OnComplete?.Invoke();
        }

        public void DetectGameOver()
        {
            if (score >= winScore) BallGameWin();
        }
    }
}