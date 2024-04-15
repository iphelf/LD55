using Summons.Scripts.General;
using Summons.Scripts.ViewCtrls.MiniGames.BallGame;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class CramSchoolManager : Singleton<CramSchoolManager>
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject BallGameNPC;
        [SerializeField] private Button BallGameNPCSign;

        private bool BallGameFlag;

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void ShowBallGameNPCSign(bool status)
        {
            BallGameNPCSign.gameObject.SetActive(status);
        }

        public void StartBallGame()
        {
            BallGameCtrl.Instance.ShowBallGamePanel(true);
            ShowBallGameNPCSign(false);
        }
    }
}