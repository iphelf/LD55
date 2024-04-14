using Summons.Scripts.Managers;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls
{
    public class QuestsCtrl : MonoBehaviour
    {
        private void Start()
        {
            QuestManager.OnQuestBegin.AddListener(OnQuestBegin);
            QuestManager.OnQuestEnd.AddListener(OnQuestEnd);
        }

        private void OnQuestBegin(int id)
        {
            var quest = QuestManager.GetQuestInfo(id);
            Debug.Log($"Quest {id} ({quest.Duration}) begins at {QuestManager.ElapsedTime}.");
        }

        private void OnQuestEnd(int id)
        {
            var quest = QuestManager.GetQuestInfo(id);
            Debug.Log($"Quest {id} ({quest.Duration}) ends at {QuestManager.ElapsedTime}.");
        }

        private void Update()
        {
            QuestManager.Update(Time.deltaTime);
        }
    }
}