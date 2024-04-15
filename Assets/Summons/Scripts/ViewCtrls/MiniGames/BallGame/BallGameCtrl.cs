using Summons.Scripts.General;
using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using Summons.Scripts.ViewCtrls.MiniGames.BallGame;
using Summons.Scripts.ViewCtrls.Places;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallGameCtrl : Singleton<BallGameCtrl>, IMiniGameCtrl
{
    [SerializeField] private TMP_Text questInfoText;
    //[SerializeField] private Button completeButton;
    private Action _onComplete;
    public void Setup(QuestArgs args, Action onComplete)
    {
        questInfoText.text = args.GetType().Name;
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
        gameObject.SetActive(status);
        // Debug.Log("µã1");
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
        if (score > winScore) BallGameWin();
    }

    public void SetQuestInfo(string quest)
    {
        questInfoText.text = quest;
    }
}
