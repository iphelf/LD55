using Summons.Scripts.ViewCtrls.GameModules;
using UnityEngine;

namespace Summons.Scripts.Managers
{
    public class StatsManager : MonoBehaviour
    {
        private static int _initialHp;

        public static void Initialize(int initialHp)
        {
            _initialHp = initialHp;
        }

        private int _hp;
        [SerializeField] private HeartsCtrl heartsCtrl;

        private void Start()
        {
            _hp = _initialHp;
            QuestManager.OnQuestTimeout.AddListener(OnQuestTimeout);

            heartsCtrl.SetHearts(_hp);
        }

        private void Update()
        {
            if (QuestManager.RemainingQuestCount <= 0)
                GameManager.EndGame(QuestManager.ElapsedTime, _hp);
        }

        private void OnDestroy()
        {
            QuestManager.OnQuestTimeout.RemoveListener(OnQuestTimeout);
        }

        private void OnQuestTimeout(int id)
        {
            _hp = Mathf.Max(0, _hp - 1);
            AudioManager.PlaySfx(SfxKey.MissionFail);
            if (_hp == 0)
            {
                GameManager.EndGame(QuestManager.ElapsedTime, _hp);
                return;
            }

            if (_hp == 2)
            {
                AudioManager.PlayMusic(MusicKey.Sad);
            }

            heartsCtrl.SetHearts(_hp);
        }
    }
}