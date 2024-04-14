using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;
using UnityEngine.Events;

namespace Summons.Scripts.Managers
{
    /// 控制任务的开始和结束
    internal static class QuestManager
    {
        private static SortedDictionary<int, QuestData> _questDict;

        /// 任务开始倒计时
        public static readonly UnityEvent<int> OnQuestBegin = new();

        /// 任务结束（要么倒计时结束、要么通过小游戏提前完成）
        public static readonly UnityEvent<int> OnQuestEnd = new();

        /// 当前正在倒计时的全部任务
        public static readonly List<int> OngoingQuests = new();

        private static readonly Dictionary<int, int> PredCount = new(); // 尚未满足的前置条件数量
        private static readonly List<QuestData> PendingQuests = new(); // 前置条件已满足，但仍在delay
        private static readonly SortedSet<int> NewlyBegunQuests = new(); // delay已完成，即将begin
        private static readonly SortedSet<int> NewlyEndedQuests = new(); // 任务已完成，即将end
        private static readonly SortedSet<int> ManualEndingQuests = new(); // 手动完成任务，将进入NewlyEnded

        /// 任务系统自启动以来总共运行了多少秒
        public static float ElapsedTime { get; private set; }

        /// 由GameManager负责初始化/重置
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
            for (var i = OngoingQuests.Count - 1; i >= 0; --i)
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

            for (var i = PendingQuests.Count - 1; i >= 0; --i)
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

            for (var i = OngoingQuests.Count - 1; i >= 0; --i)
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
            foreach (var questId in NewlyBegunQuests)
            {
                OngoingQuests.Add(questId);
                OnQuestBegin.Invoke(questId);
            }

            NewlyBegunQuests.Clear();

            foreach (var questId in NewlyEndedQuests)
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

        /// 获取任务信息。如果不够的话，可以在这里补充信息，例如任务类型、召唤位置、召唤人
        public static QuestInfo GetQuestInfo(int id)
        {
            return new QuestInfoImpl(_questDict[id]);
        }

        private class QuestInfoImpl : QuestInfo
        {
            private readonly QuestData _data;

            public QuestInfoImpl(QuestData data)
            {
                _data = data;
            }

            public override int Id => _data.Id;
            public override float Elapsed => _data.Elapsed;
            public override float Duration => _data.Duration;
            public override string Description => _data.Description;
        }
    }
}