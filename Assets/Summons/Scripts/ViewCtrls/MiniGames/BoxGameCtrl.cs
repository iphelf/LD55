using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Summons.Scripts.General;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.MiniGames.BoxGame;
using TMPro;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames
{
    public class BoxGameCtrl : Singleton<BoxGameCtrl>, IMiniGameCtrl
    {
        private Action _onComplete;

        public void Setup(QuestArgs args, Action onComplete)
        {
            var questArgs = args as QuestArgsOfOrganizeStuff;
            winScore = questArgs!.WinScore;
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
            SendNextItem();
            nextAddSpeedTime = Time.deltaTime + IncreaseAddSpeedTime;
        }

        private void SendNextItem()
        {
            // Debug.Log(nameof(SendNextItem));
            ItemList[newItemID()].gameObject.SetActive(true);
        }

        // Update is called once per frame
        private void Update()
        {
            if (score >= winScore) BoxGameWin();
            scoreTMP.text = $"Score: {score}/{winScore}";
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
            // Debug.Log("��1");
        }

        public void BoxGameWin()
        {
            _onComplete();
            ShowBoxGamePanel(false);
            // CramSchoolManager.Instance.ShowBallGameNPCSign(false);
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
            SendNextItem();
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
}