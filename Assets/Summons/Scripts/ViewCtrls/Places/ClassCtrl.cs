using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class ClassCtrl : PlaceCtrlBase
    {
        [SerializeField] private Button startEnglish;
        [SerializeField] private Button startMath;
        [SerializeField] private englishClassCtrl englishClassCtrl;
        [SerializeField] private mathClassCtrl mathClassCtrl;
        private QuestInfo _running; // 若不为null，则小游戏正在运行中，且执行的是该任务

        private void Start()
        {
            startEnglish.onClick.AddListener(() =>
            {
                if (_running != null) return;
                var quest = QuestManager.GetNextOngoingQuestOfType(QuestType.DoEnglishQuiz);
                if (quest == null) return;
                _running = quest;
                englishClassCtrl.gameObject.SetActive(true);
                englishClassCtrl.Setup(quest.Args, () =>
                {
                    englishClassCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            startMath.onClick.AddListener(() =>
            {
                if (_running != null) return;
                var quest = QuestManager.GetNextOngoingQuestOfType(QuestType.DoMathQuiz);
                if (quest == null) return;
                _running = quest;
                mathClassCtrl.gameObject.SetActive(true);
                mathClassCtrl.Setup(quest.Args, () =>
                {
                    mathClassCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            englishClassCtrl.gameObject.SetActive(false);
            mathClassCtrl.gameObject.SetActive(false);
            QuestManager.OnQuestEnd.AddListener(OnQuestEnd);
        }

        private void Update()
        {
            bool showsEnglishEntry = false;
            bool showsMathEntry = false;
            if (_running == null)
            {
                foreach (var id in QuestManager.OngoingQuests)
                {
                    var quest = QuestManager.GetQuestInfo(id);
                    showsEnglishEntry = showsEnglishEntry || quest.Type == QuestType.DoEnglishQuiz;
                    showsMathEntry = showsMathEntry || quest.Type == QuestType.DoMathQuiz;
                }
            }

            startEnglish.gameObject.SetActive(showsEnglishEntry);
            startMath.gameObject.SetActive(showsMathEntry);
        }

        private void OnDestroy()
        {
            QuestManager.OnQuestEnd.RemoveListener(OnQuestEnd);
        }

        private void OnQuestEnd(int id)
        {
            if (_running?.Id != id) return;

            if (_running.Type == QuestType.DoEnglishQuiz)
            {
                englishClassCtrl.gameObject.SetActive(false);
            }
            else if (_running.Type == QuestType.DoMathQuiz)
            {
                mathClassCtrl.gameObject.SetActive(false);
            }

            _running = null;
        }
    }
}