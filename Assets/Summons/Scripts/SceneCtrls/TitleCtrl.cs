using Summons.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.SceneCtrls
{
    public class TitleCtrl : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            startButton.onClick.AddListener(() =>
            {
                AudioManager.PlaySfx(SfxKey.MainMenuClick);
                GameManager.StartGame();
            });
            creditsButton.onClick.AddListener(() =>
            {
                AudioManager.PlaySfx(SfxKey.MainMenuClick);
                GameManager.OpenCredits();
            });
            quitButton.onClick.AddListener(() =>
            {
                AudioManager.PlaySfx(SfxKey.MainMenuClick);
                GameManager.QuitGame();
            });

            AudioManager.PlayMusic(MusicKey.Happy);
        }
    }
}