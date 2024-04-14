using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Summons.Scripts.General;
using Summons.Scripts.ViewCtrls.Places;
using TMPro;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames.BoxGame
{
    public class BoxGameManager : Singleton<BoxGameManager>
    {
        [SerializeField] private int score;
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private int winScore = 10;
        [SerializeField] private GameObject BoxGamePanel;
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
            BoxGamePanel.SetActive(status);
            // Debug.Log("点1");
        }

        public void BoxGameWin()
        {
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
            return Random.Range(0, ItemList.Count() - 1);
        }

        public void AllItemAddSpeed()
        {
            for (var i = 0; i < ItemList.Count(); i++) ItemList[i].AddMoveSpeedX(1f);
        }
    }
}