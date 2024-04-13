using System;
using System.Collections.Generic;

namespace Summons.Scripts.Models
{
    public class Task
    {
        public readonly int Id;
        public readonly List<Task> Predecessors = new();
        public float Delay;

        public float Duration;

        public TaskType Type;
        public TaskArgs Args;

        public Task(int id)
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