using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;
using UnityEngine.Events;

namespace Summons.Scripts.Managers
{
    /// 控制任务的开始和结束
    static class TaskManager
    {
        private static SortedDictionary<int, TaskData> _taskDict;

        public static readonly UnityEvent<int> OnTaskBegin = new();
        public static readonly UnityEvent<int> OnTaskEnd = new();
        public static readonly List<TaskData> OngoingTasks = new();

        private static readonly Dictionary<int, int> PredCount = new(); // 尚未满足的前置条件数量
        private static readonly List<TaskData> PendingTasks = new(); // 前置条件已满足，但仍在delay
        private static readonly SortedSet<int> NewlyBegunTasks = new(); // delay已完成，即将begin
        private static readonly SortedSet<int> NewlyEndedTasks = new(); // 任务已完成，即将end
        private static readonly SortedSet<int> ManualEndingTasks = new(); // 手动完成任务，将进入NewlyEnded

        public static float ElapsedTime { get; private set; }

        public static void Reset(TasksConfig config)
        {
            _taskDict = config.ToTaskDict();

            OngoingTasks.Clear();

            PredCount.Clear();
            PendingTasks.Clear();
            NewlyBegunTasks.Clear();
            NewlyEndedTasks.Clear();
            ManualEndingTasks.Clear();

            ElapsedTime = 0.0f;

            foreach (var task in _taskDict.Values)
            {
                PredCount.Add(task.Id, task.Predecessors.Count);
                if (task.Predecessors.Count == 0)
                    PendingTasks.Add(task);
            }
        }

        // 由TasksCtrl驱动；也可由其暂停
        public static void Update(float deltaTime)
        {
            // 先响应小游戏结算并据此更新任务状态
            UpdateStatesByLastFrameInput();
            // 然后处理计时器并据此更新任务状态
            UpdateStatesByLastFrameTime(deltaTime);
            // 最后处理回调，先处理NewlyBegunTasks，然后处理NewlyEndedTasks
            SendEventsAndHandleCallbacks();
        }

        private static void UpdateStatesByLastFrameInput()
        {
            for (int i = OngoingTasks.Count - 1; i >= 0; --i)
                if (ManualEndingTasks.Contains(OngoingTasks[i].Id))
                {
                    OngoingTasks.RemoveAt(i);
                    NewlyEndedTasks.Add(OngoingTasks[i].Id);
                }

            ManualEndingTasks.Clear();
        }

        private static void UpdateStatesByLastFrameTime(float deltaTime)
        {
            ElapsedTime += deltaTime;

            for (int i = PendingTasks.Count - 1; i >= 0; --i)
            {
                var task = PendingTasks[i];
                task.Elapsed += deltaTime;
                if (task.Elapsed >= task.Delay)
                {
                    PendingTasks.RemoveAt(i);
                    task.Elapsed -= task.Delay;
                    // 假设：不存在在一个frame内就完成delay和duration全过程的任务
                    NewlyBegunTasks.Add(task.Id);
                }
            }

            for (int i = OngoingTasks.Count - 1; i >= 0; --i)
            {
                var task = OngoingTasks[i];
                task.Elapsed += deltaTime;
                if (task.Elapsed >= task.Duration)
                {
                    OngoingTasks.RemoveAt(i);
                    NewlyEndedTasks.Add(task.Id);
                }
            }
        }

        private static void SendEventsAndHandleCallbacks()
        {
            foreach (int taskId in NewlyBegunTasks)
            {
                OngoingTasks.Add(_taskDict[taskId]);
                OnTaskBegin.Invoke(taskId);
            }

            NewlyBegunTasks.Clear();

            foreach (int taskId in NewlyEndedTasks)
            {
                OnTaskEnd.Invoke(taskId);
                foreach (var successor in _taskDict[taskId].Successors)
                {
                    --PredCount[successor.Id];
                    if (PredCount[successor.Id] == 0)
                        PendingTasks.Add(successor);
                }
            }

            NewlyEndedTasks.Clear();
        }

        /// 由小游戏在完成时调用
        public static void EndTask(int id)
        {
            ManualEndingTasks.Add(id);
        }

        private class TaskInfoImpl : TaskInfo
        {
            private readonly TaskData _data;

            public override int Id => _data.Id;
            public override float Elapsed => _data.Elapsed;
            public override float Duration => _data.Duration;

            public TaskInfoImpl(TaskData data)
            {
                _data = data;
            }
        }
    }
}