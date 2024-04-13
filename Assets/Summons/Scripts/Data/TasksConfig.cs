using System;
using System.Collections.Generic;
using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Tasks Config", fileName = "tasks")]
    public class TasksConfig : ScriptableObject
    {
        [Serializable]
        public class TaskEntry
        {
            public int id;
            public List<int> predecessors;
            public float delay;
            public float duration;
            public TaskType type;
            public string args;

            public Task ToTask(Dictionary<int, Task> taskDict)
            {
                Task task = new(id);
                foreach (int predecessor in predecessors)
                    task.Predecessors.Add(taskDict[predecessor]);
                task.Delay = delay;
                task.Duration = duration;
                task.Type = type;
                task.Args = Task.ParseArgs(type, args);
                return task;
            }
        }

        public List<TaskEntry> tasks;

        public Dictionary<int, Task> ToTaskDict()
        {
            List<TaskEntry> sortedTasks = new(tasks);
            sortedTasks.Sort((a, b) => a.id.CompareTo(b.id));
            Dictionary<int, Task> taskDict = new();
            foreach (TaskEntry entry in sortedTasks)
                taskDict.Add(entry.id, entry.ToTask(taskDict));
            return taskDict;
        }
    }
}