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

        #region Credits

        public static void CloseCreditsAndOpenTitle()
        {
            SceneManager.LoadScene(_gameConfig.titleScene);
        }

        #endregion

        #region (Anywhere)

        public static bool IsCheatingEnabled { get; private set; }

        public static void InitializeGameOnce(Configuration configuration, AudioSource audioSource)
        {
            if (_initialized) return;

            _gameConfig = configuration.gameConfig;
            AudioManager.Initialize(_gameConfig.audioConfig, audioSource);
            QuestManager.ResetData(_gameConfig.questsConfig);
            PlaceManager.ResetData(_gameConfig.placesConfig);
            DialogManager.Initialize(_gameConfig.npcConfig);
            StatsManager.Initialize(_gameConfig.initialHp);
            IsCheatingEnabled = _gameConfig.enableCheat;

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
            QuestManager.ResetData(_gameConfig.questsConfig);
            PlaceManager.ResetData(_gameConfig.placesConfig);
            SceneManager.LoadScene(_gameConfig.gameScene);
        }

        public static void OpenCredits()
        {
            SceneManager.LoadScene(_gameConfig.creditsScene);
        }

        #endregion

        #region Game

        public static float LastGameDuration { get; private set; }
        public static int LastGameHp { get; private set; }

        public static void EndGame(float duration, int hp)
        {
            LastGameDuration = duration;
            LastGameHp = hp;
            SceneManager.LoadScene(_gameConfig.endScene);
        }

        #endregion

        #region End

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