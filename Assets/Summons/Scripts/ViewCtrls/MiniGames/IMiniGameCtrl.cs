using System;
using Summons.Scripts.Models;

namespace Summons.Scripts.ViewCtrls.MiniGames
{
    public interface IMiniGameCtrl
    {
        /// 启动小游戏后根据任务配置初始化
        public void Setup(QuestArgs args, Action onComplete);
    }
}