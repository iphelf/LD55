using Summons.Scripts.General;
using UnityEngine;

namespace Summons.Scripts.Data
{
    /// 游戏流程的配置
    [CreateAssetMenu(menuName = "Scriptable Object/Game Config", fileName = "game")]
    public class GameConfig : ScriptableObject
    {
        public TasksConfig tasksConfig;
        public PlacesConfig placesConfig;
        [Header("Scenes"), Scene] public string titleScene = "Title";
        [Scene] public string creditsScene = "Credits";
        [Scene] public string gameOverScene = "GameOver";
        [Space] public AudioConfig audioConfig;
    }
}