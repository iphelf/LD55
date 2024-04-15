using System;
using System.Collections.Generic;
using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/NPC Config", fileName = "npc")]
    public class NpcConfig : ScriptableObject
    {
        [Serializable]
        public class NpcEntry
        {
            public string name;
            public QuestType type;
            [TextArea] public string defaultMessage;
            [TextArea] public string summonMessage;

            public NpcData ToData()
            {
                return new NpcData
                {
                    Name = name,
                    DefaultMessage = defaultMessage,
                    SummonMessage = summonMessage,
                };
            }
        }

        public List<NpcEntry> npc = new();

        public Dictionary<QuestType, NpcData> ToDict()
        {
            Dictionary<QuestType, NpcData> dict = new();
            foreach (var entry in npc)
                dict.Add(entry.type, entry.ToData());
            return dict;
        }
    }
}