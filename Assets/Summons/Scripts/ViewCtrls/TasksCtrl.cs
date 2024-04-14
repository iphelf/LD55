using Summons.Scripts.Managers;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls
{
    public class TasksCtrl : MonoBehaviour
    {
        private void Start()
        {
            TaskManager.OnTaskBegin.AddListener(OnTaskBegin);
            TaskManager.OnTaskEnd.AddListener(OnTaskEnd);
        }

        private void OnTaskBegin(int id)
        {
            Debug.Log($"Task {id} begins at {TaskManager.ElapsedTime}.");
        }

        private void OnTaskEnd(int id)
        {
            Debug.Log($"Task {id} ends at {TaskManager.ElapsedTime}.");
        }

        private void Update()
        {
            TaskManager.Update(Time.deltaTime);
        }
    }
}