using System.Collections;
using Summons.Scripts.General;
using Summons.Scripts.ViewCtrls.Places;
using TMPro;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames.BallGame
{
    public class BallGameManager : Singleton<BallGameManager>
    {
        [SerializeField] private int score;
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private int winScore = 10;

        [SerializeField] private GameObject BallGamePanel;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (score > winScore) BallGameWin();
            scoreTMP.text = "Score:" + score;
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
            BallGamePanel.SetActive(status);
            Debug.Log("点1");
        }

        public void BallGameWin()
        {
            ShowBallGamePanel(false);
            CramSchoolManager.Instance.ShowBallGameNPCSign(false);
        }
    }
}