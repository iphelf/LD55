using System;
using System.Collections.Generic;

namespace Summons.Scripts.Models
{
    public class QuestData
    {
        public readonly int Id;
        public readonly List<QuestData> Predecessors = new();
        public readonly List<QuestData> Successors = new();
        public QuestArgs Args;
        public float Delay;
        public string Description;

        public float Duration;
        public float Elapsed = 0.0f;
        public QuestResult Result;

        public QuestType Type;

        public QuestData(int id)
        {
            Id = id;
        }

        public static QuestArgs ParseArgs(QuestType type, string args)
        {
            return type switch
            {
                QuestType.WipeStains => new QuestArgsOfWipeStains(args),
                QuestType.OrganizeStuff => new QuestArgsOfOrganizeStuff(args),
                QuestType.DoEnglishQuiz => new QuestArgsOfDoEnglishQuiz(args),
                QuestType.DoMathQuiz => new QuestArgsOfDoMathQuiz(args),
                QuestType.PurchaseItem => new QuestArgsOfPurchaseItem(args),
                QuestType.PracticeVolleyball => new QuestArgsOfPracticeVolleyball(args),
                _ => throw new NotImplementedException($"QuestType: {type}")
            };
        }
    }

    public enum QuestResult
    {
        None = 0,
        Timeout = 1,
        Aborted = 2,
        Success = 3
    }

    public class QuestArgs
    {
        protected QuestArgs(string args)
        {
        }
    }

    public class QuestArgsOfWipeStains : QuestArgs
    {
        public QuestArgsOfWipeStains(string args) : base(args)
        {
        }
    }

    public class QuestArgsOfOrganizeStuff : QuestArgs
    {
        public QuestArgsOfOrganizeStuff(string args) : base(args)
        {
        }
    }

    public class QuestArgsOfDoEnglishQuiz : QuestArgs
    {
        public QuestArgsOfDoEnglishQuiz(string args) : base(args)
        {
        }
    }

    public class QuestArgsOfDoMathQuiz : QuestArgs
    {
        public QuestArgsOfDoMathQuiz(string args) : base(args)
        {
        }
    }

    public class QuestArgsOfPurchaseItem : QuestArgs
    {
        public QuestArgsOfPurchaseItem(string args) : base(args)
        {
        }
    }

    public class QuestArgsOfPracticeVolleyball : QuestArgs
    {
        public QuestArgsOfPracticeVolleyball(string args) : base(args)
        {
        }
    }
}