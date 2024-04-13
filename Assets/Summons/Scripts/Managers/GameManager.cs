using Summons.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Summons.Scripts.Managers
{
    /// 宏观管理游戏全流程
    public static class GameManager
    {
        private static bool _initialized;
        private static GameConfig _gameConfig;

        #region (Anywhere)

        public static void InitializeGameOnce(Configuration configuration, AudioSource audioSource)
        {
            if (_initialized) return;

            _gameConfig = configuration.gameConfig;
            AudioManager.Initialize(_gameConfig.audioConfig, audioSource);
            TaskManager.Reset(_gameConfig.tasksConfig);

            _initialized = true;
            Debug.Log("Initialized.");
        }

        public static void QuitGame()
        {
            Application.Quit();
        }

        #endregion

        #region Title

        public static void StartGame()
        {
        }

        public static void OpenCredits()
        {
            SceneManager.LoadScene(_gameConfig.creditsScene);
        }

        #endregion

        #region Credits

        public static void CloseCreditsAndOpenTitle()
        {
            SceneManager.LoadScene(_gameConfig.titleScene);
        }

        #endregion

        #region Game Over

        public static void ReturnToTitle()
        {
            SceneManager.LoadScene(_gameConfig.titleScene);
        }

        public static void RestartGame()
        {
            StartGame();
        }

        #endregion
    }
}