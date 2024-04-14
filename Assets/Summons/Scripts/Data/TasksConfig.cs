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

            public TaskData ToTask(SortedDictionary<int, TaskData> taskDict)
            {
                TaskData task = new(id);
                foreach (int predecessor in predecessors)
                {
                    var preTask = taskDict[predecessor];
                    task.Predecessors.Add(preTask);
                    preTask.Successors.Add(task);
                }

                task.Delay = delay;
                task.Duration = duration;
                task.Type = type;
                task.Args = TaskData.ParseArgs(type, args);
                return task;
            }
        }

        public List<TaskEntry> tasks;

        public SortedDictionary<int, TaskData> ToTaskDict()
        {
            List<TaskEntry> sortedTasks = new(tasks);
            sortedTasks.Sort((a, b) => a.id.CompareTo(b.id));
            SortedDictionary<int, TaskData> taskDict = new();
            foreach (TaskEntry entry in sortedTasks)
                taskDict.Add(entry.id, entry.ToTask(taskDict));
            return taskDict;
        }
    }
}