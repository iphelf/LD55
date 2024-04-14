using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;
using UnityEngine.Events;

namespace Summons.Scripts.Managers
{
    /// 控制任务的开始和结束
    static class QuestManager
    {
        private static SortedDictionary<int, QuestData> _questDict;

        public static readonly UnityEvent<int> OnQuestBegin = new();
        public static readonly UnityEvent<int> OnQuestEnd = new();
        public static readonly List<int> OngoingQuests = new();

        private static readonly Dictionary<int, int> PredCount = new(); // 尚未满足的前置条件数量
        private static readonly List<QuestData> PendingQuests = new(); // 前置条件已满足，但仍在delay
        private static readonly SortedSet<int> NewlyBegunQuests = new(); // delay已完成，即将begin
        private static readonly SortedSet<int> NewlyEndedQuests = new(); // 任务已完成，即将end
        private static readonly SortedSet<int> ManualEndingQuests = new(); // 手动完成任务，将进入NewlyEnded

        public static float ElapsedTime { get; private set; }

        public static void Reset(QuestsConfig config)
        {
            _questDict = config.ToQuestDict();

            OnQuestBegin.RemoveAllListeners();
            OnQuestEnd.RemoveAllListeners();
            OngoingQuests.Clear();

            PredCount.Clear();
            PendingQuests.Clear();
            NewlyBegunQuests.Clear();
            NewlyEndedQuests.Clear();
            ManualEndingQuests.Clear();

            ElapsedTime = 0.0f;

            foreach (var quest in _questDict.Values)
            {
                PredCount.Add(quest.Id, quest.Predecessors.Count);
                if (quest.Predecessors.Count == 0)
                    PendingQuests.Add(quest);
            }
        }

        // 由QuestsCtrl驱动；也可由其暂停
        public static void Update(float deltaTime)
        {
            // 先响应小游戏结算并据此更新任务状态
            UpdateStatesByLastFrameInput();
            // 然后处理计时器并据此更新任务状态
            UpdateStatesByLastFrameTime(deltaTime);
            // 最后处理回调，先处理NewlyBegunQuests，然后处理NewlyEndedQuests
            SendEventsAndHandleCallbacks();
        }

        private static void UpdateStatesByLastFrameInput()
        {
            for (int i = OngoingQuests.Count - 1; i >= 0; --i)
                if (ManualEndingQuests.Contains(OngoingQuests[i]))
                {
                    NewlyEndedQuests.Add(OngoingQuests[i]);
                    OngoingQuests.RemoveAt(i);
                }

            ManualEndingQuests.Clear();
        }

        private static void UpdateStatesByLastFrameTime(float deltaTime)
        {
            ElapsedTime += deltaTime;

            for (int i = PendingQuests.Count - 1; i >= 0; --i)
            {
                var quest = PendingQuests[i];
                quest.Elapsed += deltaTime;
                if (quest.Elapsed >= quest.Delay)
                {
                    PendingQuests.RemoveAt(i);
                    quest.Elapsed -= quest.Delay;
                    // 假设：不存在在一个frame内就完成delay和duration全过程的任务
                    NewlyBegunQuests.Add(quest.Id);
                }
            }

            for (int i = OngoingQuests.Count - 1; i >= 0; --i)
            {
                var quest = _questDict[OngoingQuests[i]];
                quest.Elapsed += deltaTime;
                if (quest.Elapsed >= quest.Duration)
                {
                    OngoingQuests.RemoveAt(i);
                    NewlyEndedQuests.Add(quest.Id);
                }
            }
        }

        private static void SendEventsAndHandleCallbacks()
        {
            foreach (int questId in NewlyBegunQuests)
            {
                OngoingQuests.Add(questId);
                OnQuestBegin.Invoke(questId);
            }

            NewlyBegunQuests.Clear();

            foreach (int questId in NewlyEndedQuests)
            {
                OnQuestEnd.Invoke(questId);
                foreach (var successor in _questDict[questId].Successors)
                {
                    --PredCount[successor.Id];
                    if (PredCount[successor.Id] == 0)
                        PendingQuests.Add(successor);
                }
            }

            NewlyEndedQuests.Clear();
        }

        /// 由小游戏在完成时调用
        public static void EndQuest(int id)
        {
            ManualEndingQuests.Add(id);
        }

        public static QuestInfo GetQuestInfo(int id) => new QuestInfoImpl(_questDict[id]);

        private class QuestInfoImpl : QuestInfo
        {
            private readonly QuestData _data;

            public override int Id => _data.Id;
            public override float Elapsed => _data.Elapsed;
            public override float Duration => _data.Duration;

            public QuestInfoImpl(QuestData data)
            {
                _data = data;
            }
        }
    }
}