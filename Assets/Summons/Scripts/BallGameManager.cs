using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BallGameManager : Singleton<BallGameManager>
{
    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private int winScore = 10;
    [SerializeField] private GameObject BallGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score > winScore) BallGameWin();
        scoreTMP.text = "Score:" + score.ToString();
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
    IEnumerator RestartBall1(Ball ball)
    {
        ball.transform.position = ball.StartPosition;
        ball.Movement = ball.StartPosition;
        yield return new WaitForSeconds(2f);
        ball.gameObject.SetActive(true);
    }

    public void ShowBallGamePanel(bool status)
    {
        BallGamePanel.SetActive(status);
        Debug.Log("µã1");
    }
    public void BallGameWin()
    {
        ShowBallGamePanel(false);
        CramSchoolManager.Instance.ShowBallGameNPCSign(false);
    }
}
