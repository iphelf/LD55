using System;
using System.Collections.Generic;

namespace Summons.Scripts.Models
{
    public class TaskData
    {
        public readonly int Id;
        public readonly List<TaskData> Predecessors = new();
        public readonly List<TaskData> Successors = new();
        public float Delay;

        public float Duration;
        public float Elapsed = 0.0f;

        public TaskType Type;
        public TaskArgs Args;
        public TaskResult Result;

        public TaskData(int id)
        {
            Id = id;
        }

        public static TaskArgs ParseArgs(TaskType type, string args)
        {
            return type switch
            {
                TaskType.WipeStains => new TaskArgsOfWipeStains(),
                TaskType.OrganizeStuff => new TaskArgsOfOrganizeStuff(),
                TaskType.DoAssignment => new TaskArgsOfDoAssignment(),
                TaskType.PurchaseItem => new TaskArgsOfPurchaseItem(),
                TaskType.PracticeVolleyball => new TaskArgsOfPracticeVolleyball(),
                _ => throw new NotImplementedException($"TaskType: {type}"),
            };
        }
    }

    public enum TaskResult
    {
        None = 0,
        Timeout = 1,
        Aborted = 2,
        Success = 3,
    }

    public class TaskArgs
    {
    }

    public class TaskArgsOfWipeStains : TaskArgs
    {
    }

    public class TaskArgsOfOrganizeStuff : TaskArgs
    {
    }

    public class TaskArgsOfDoAssignment : TaskArgs
    {
    }

    public class TaskArgsOfPurchaseItem : TaskArgs
    {
    }

    public class TaskArgsOfPracticeVolleyball : TaskArgs
    {
    }
}