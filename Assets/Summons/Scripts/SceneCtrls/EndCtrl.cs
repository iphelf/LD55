using DG.Tweening;
using Summons.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.SceneCtrls
{
    public class EndCtrl : MonoBehaviour
    {
        [SerializeField] private GameObject buttonGroup;
        [SerializeField] private TMP_Text conclusion;
        [TextArea] [SerializeField] private string goodConclusion;
        [TextArea] [SerializeField] private string badConclusion;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private GameObject goodEndImage;
        [SerializeField] private GameObject badEndImage;

        private async void Start()
        {
            restartButton.onClick.AddListener(GameManager.RestartGame);
            menuButton.onClick.AddListener(GameManager.ReturnToTitle);
            quitButton.onClick.AddListener(GameManager.QuitGame);

            string scoreText = $"HP={GameManager.LastGameHp}, " +
                               $"Duration={Mathf.CeilToInt(GameManager.LastGameDuration)}s";
            score.text = scoreText;

            bool isGoodEnding = GameManager.LastGameHp > 0;

            if (isGoodEnding)
            {
                goodEndImage.SetActive(true);
                conclusion.text = goodConclusion;
                await Awaitable.WaitForSecondsAsync(3.0f);
                goodEndImage.SetActive(false);
                await Awaitable.WaitForSecondsAsync(3.0f);
            }
            else
            {
                conclusion.text = badConclusion;
                await Awaitable.WaitForSecondsAsync(3.0f);
                badEndImage.SetActive(true);
                await Awaitable.WaitForSecondsAsync(3.0f);
            }

            buttonGroup.SetActive(true);
        }
    }
}