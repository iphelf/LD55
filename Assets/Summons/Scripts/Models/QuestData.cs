using System;
using System.Collections.Generic;

namespace Summons.Scripts.Models
{
    public class QuestData
    {
        public readonly int Id;
        public readonly List<QuestData> Predecessors = new();
        public readonly List<QuestData> Successors = new();
        public float Delay;

        public float Duration;
        public float Elapsed = 0.0f;

        public QuestType Type;
        public QuestArgs Args;
        public string Description;
        public QuestResult Result;

        public QuestData(int id)
        {
            Id = id;
        }

        public static QuestArgs ParseArgs(QuestType type, string args)
        {
            return type switch
            {
                QuestType.WipeStains => new QuestArgsOfWipeStains(),
                QuestType.OrganizeStuff => new QuestArgsOfOrganizeStuff(),
                QuestType.DoEnglishQuiz => new QuestArgsOfDoEnglishQuiz(),
                QuestType.DoMathQuiz => new QuestArgsOfDoMathQuiz(),
                QuestType.PurchaseItem => new QuestArgsOfPurchaseItem(),
                QuestType.PracticeVolleyball => new QuestArgsOfPracticeVolleyball(),
                _ => throw new NotImplementedException($"QuestType: {type}"),
            };
        }
    }

    public enum QuestResult
    {
        None = 0,
        Timeout = 1,
        Aborted = 2,
        Success = 3,
    }

    public class QuestArgs
    {
    }

    public class QuestArgsOfWipeStains : QuestArgs
    {
    }

    public class QuestArgsOfOrganizeStuff : QuestArgs
    {
    }

    public class QuestArgsOfDoEnglishQuiz : QuestArgs
    {
    }

    public class QuestArgsOfDoMathQuiz : QuestArgs
    {
    }

    public class QuestArgsOfPurchaseItem : QuestArgs
    {
    }

    public class QuestArgsOfPracticeVolleyball : QuestArgs
    {
    }
}