using System.Collections.Generic;

namespace Summons.Scripts.General
{
    public static class ListUtility
    {
        public static void RemoveBatch<T>(List<T> list, IEnumerable<int> sortedIndices)
        {
            int i = 0;
            int j = 0;
            int removed = 0;
            foreach (int toRemove in sortedIndices)
            {
                while (i < list.Count)
                {
                    if (toRemove == i)
                    {
                        ++i;
                        break;
                    }

                    list[j] = list[i];
                    ++i;
                    ++j;
                }

                ++removed;
            }

            list.RemoveRange(list.Count - removed, removed);
        }
    }
}