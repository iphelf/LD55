namespace Summons.Scripts.Models
{
    public abstract class QuestInfo
    {
        public abstract int Id { get; }
        public abstract float Elapsed { get; }
        public abstract float Duration { get; }
        public abstract string Description { get; }
        public abstract QuestArgs Args { get; }

        // type
        // npc
    }
}