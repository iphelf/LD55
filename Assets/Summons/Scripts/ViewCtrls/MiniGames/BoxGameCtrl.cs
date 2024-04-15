using Summons.Scripts.General;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames;
using Summons.Scripts.ViewCtrls.MiniGames.BoxGame;
using Summons.Scripts.ViewCtrls.Places;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxGameCtrl : Singleton<BoxGameCtrl>, IMiniGameCtrl
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
    [SerializeField] private int winScore = 10;
    [SerializeField] private List<Item> ItemList;

    [SerializeField] private int IncreaseAddSpeedTime = 5;

    private float nextAddSpeedTime;

    // Start is called before the first frame update
    private void Start()
    {
        ItemList[newItemID()].gameObject.SetActive(true);
        nextAddSpeedTime = Time.deltaTime + IncreaseAddSpeedTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (score > winScore) BoxGameWin();
        scoreTMP.text = "Score:" + score;
        if (Time.time > nextAddSpeedTime)
        {
            AllItemAddSpeed();
            nextAddSpeedTime = Time.time + IncreaseAddSpeedTime;
        }
    }

    public void AddScore(int addscore)
    {
        score += addscore;
    }

    public void SubScore(int subscore)
    {
        score -= subscore;
    }

    public void ShowBoxGamePanel(bool status)
    {
        gameObject.SetActive(status);
        // Debug.Log("µã1");
    }

    public void BoxGameWin()
    {
        _onComplete();
        ShowBoxGamePanel(false);
        CramSchoolManager.Instance.ShowBallGameNPCSign(false);
    }

    public void RestartBoxGame(Item item)
    {
        StartCoroutine(RestartBox(item));
    }

    private IEnumerator RestartBox(Item item)
    {
        item.transform.position = item.StartPosition;
        item.Movement = item.StartPosition;
        item.ResetMoveSpeedX();
        yield return new WaitForSeconds(2f);
        ItemList[newItemID()].gameObject.SetActive(true);
    }

    public int newItemID()
    {
        return UnityEngine.Random.Range(0, ItemList.Count() - 1);
    }

    public void AllItemAddSpeed()
    {
        for (var i = 0; i < ItemList.Count(); i++) ItemList[i].AddMoveSpeedX(1f);
    }
}
