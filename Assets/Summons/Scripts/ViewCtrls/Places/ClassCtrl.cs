using Summons.Scripts.Managers;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls.GameModules;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.Places
{
    public class ClassCtrl : PlaceCtrlBase
    {
        [SerializeField] private NpcCtrl englishTeacher;
        [SerializeField] private NpcCtrl mathTeacher;
        [SerializeField] private englishClassCtrl englishClassCtrl;
        [SerializeField] private mathClassCtrl mathClassCtrl;
        private QuestInfo _running; // 若不为null，则小游戏正在运行中，且执行的是该任务

        private void Start()
        {
            englishTeacher.onSummonRespond.AddListener(quest =>
            {
                if (_running != null) return;
                _running = quest;
                englishClassCtrl.gameObject.SetActive(true);
                englishClassCtrl.Setup(quest.Args, () =>
                {
                    englishClassCtrl.gameObject.SetActive(false);
                    QuestManager.EndQuest(quest.Id);
                    _running = null;
                });
            });
            mathTeacher.onSummonRespond.AddListener(quest =>
            {
                if (_running != null) return;
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
            bool allowInput = _running == null;
            // TODO: 得想一个更全局更自动化的做法
            englishTeacher.Interactable = allowInput;
            mathTeacher.Interactable = allowInput;
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