using Summons.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.SceneCtrls
{
    public class EndCtrl : MonoBehaviour
    {
        [SerializeField] private TMP_Text header;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TextMeshProUGUI score;

        private void Start()
        {
            restartButton.onClick.AddListener(GameManager.RestartGame);
            menuButton.onClick.AddListener(GameManager.ReturnToTitle);
            quitButton.onClick.AddListener(GameManager.QuitGame);

            string scoreText = $"HP={GameManager.LastGameHp}, " +
                               $"Duration={Mathf.CeilToInt(GameManager.LastGameDuration)}s";
            score.text = scoreText;
            bool isGoodEnding = GameManager.LastGameHp > 0;
            header.text = isGoodEnding ? "Good Ending" : "Bad Ending";
        }
    }
}