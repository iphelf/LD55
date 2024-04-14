using System.Collections.Generic;
using Summons.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    public class QuestsCtrl : MonoBehaviour
    {
        [SerializeField] private Transform listRoot;
        [SerializeField] private GameObject questPrefab;
        [SerializeField] private TMP_InputField questId;
        [SerializeField] private Button completeButton;
        private readonly Dictionary<int, QuestCtrl> _questCtrlDict = new();

        private void Start()
        {
            QuestManager.OnQuestBegin.AddListener(OnQuestBegin);
            QuestManager.OnQuestEnd.AddListener(OnQuestEnd);
            completeButton.onClick.AddListener(() =>
            {
                var id = int.Parse(questId.text);
                QuestManager.EndQuest(id);
            });
        }

        private void Update()
        {
            QuestManager.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            QuestManager.OnQuestBegin.RemoveListener(OnQuestBegin);
            QuestManager.OnQuestEnd.RemoveListener(OnQuestEnd);
        }

        private void OnQuestBegin(int id)
        {
            var questInfo = QuestManager.GetQuestInfo(id);
            // Debug.Log($"Quest {id} ({questInfo.Duration}) begins at {QuestManager.ElapsedTime}.");
            var instance = Instantiate(questPrefab, listRoot);
            var questCtrl = instance.GetComponent<QuestCtrl>();
            questCtrl.SetQuest(questInfo);
            _questCtrlDict.Add(id, questCtrl);
        }

        private void OnQuestEnd(int id)
        {
            // var questInfo = QuestManager.GetQuestInfo(id);
            // Debug.Log($"Quest {id} ({questInfo.Duration}) ends at {QuestManager.ElapsedTime}.");
            var questCtrl = _questCtrlDict.GetValueOrDefault(id, null);
            if (questCtrl == null) return;
            Destroy(questCtrl.gameObject);
        }
    }
}