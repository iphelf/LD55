using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;

namespace Summons.Scripts.Managers
{
    public class TaskManager
    {
        private static Dictionary<int, Task> _taskDict;

        public static void Reset(TasksConfig config)
        {
            _taskDict = config.ToTaskDict();
        }
    }
}