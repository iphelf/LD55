using System;
using System.Collections.Generic;
using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Quests Config", fileName = "quests")]
    public class QuestsConfig : ScriptableObject
    {
        [Serializable]
        public class QuestEntry
        {
            public int id;
            public List<int> predecessors;
            public float delay;
            public float duration;
            public QuestType type;
            public string args;

            public QuestData ToQuest(SortedDictionary<int, QuestData> questDict)
            {
                QuestData quest = new(id);
                foreach (int predecessor in predecessors)
                {
                    var preQuest = questDict[predecessor];
                    quest.Predecessors.Add(preQuest);
                    preQuest.Successors.Add(quest);
                }

                quest.Delay = delay;
                quest.Duration = duration;
                quest.Type = type;
                quest.Args = QuestData.ParseArgs(type, args);
                return quest;
            }
        }

        public List<QuestEntry> quests;

        public SortedDictionary<int, QuestData> ToQuestDict()
        {
            List<QuestEntry> sortedQuests = new(quests);
            sortedQuests.Sort((a, b) => a.id.CompareTo(b.id));
            SortedDictionary<int, QuestData> questDict = new();
            foreach (QuestEntry entry in sortedQuests)
                questDict.Add(entry.id, entry.ToQuest(questDict));
            return questDict;
        }
    }
}