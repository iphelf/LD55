using System;
using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames
{
    public class MiniGameCtrlBase : MonoBehaviour
    {
        /// 启动小游戏后根据任务配置初始化
        public virtual void Setup(QuestArgs args, Action onComplete)
        {
        }
    }
}