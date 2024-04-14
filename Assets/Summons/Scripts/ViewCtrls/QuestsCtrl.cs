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
            Debug.Log($"Quest {id} begins at {QuestManager.ElapsedTime}.");
        }

        private void OnQuestEnd(int id)
        {
            Debug.Log($"Quest {id} ends at {QuestManager.ElapsedTime}.");
        }

        private void Update()
        {
            QuestManager.Update(Time.deltaTime);
        }
    }
}