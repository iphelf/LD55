using System;
using System.Collections.Generic;
using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Quests Config", fileName = "quests")]
    public class QuestsConfig : ScriptableObject
    {
        public List<QuestEntry> quests;

        public SortedDictionary<int, QuestData> ToQuestDict()
        {
            List<QuestEntry> sortedQuests = new(quests);
            sortedQuests.Sort((a, b) => a.id.CompareTo(b.id));
            SortedDictionary<int, QuestData> questDict = new();
            foreach (var entry in sortedQuests)
                questDict.Add(entry.id, entry.ToQuest(questDict));
            return questDict;
        }

        [Serializable]
        public class QuestEntry
        {
            public int id;
            public List<int> predecessors;
            public float delay;
            public float duration;
            public QuestType type;
            public string args;
            public string description;

            public QuestData ToQuest(SortedDictionary<int, QuestData> questDict)
            {
                QuestData quest = new(id);
                foreach (var predecessor in predecessors)
                {
                    var preQuest = questDict[predecessor];
                    quest.Predecessors.Add(preQuest);
                    preQuest.Successors.Add(quest);
                }

                quest.Delay = delay;
                quest.Duration = duration;
                quest.Type = type;
                quest.Args = QuestData.ParseArgs(type, args);
                quest.Description = description;
                return quest;
            }
        }
    }
}