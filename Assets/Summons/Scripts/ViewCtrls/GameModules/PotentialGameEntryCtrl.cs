﻿using Summons.Scripts.Data;
using Summons.Scripts.Managers;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.GameModules
{
    /// 在进入游戏前完成初始化
    public class PotentialGameEntryCtrl : MonoBehaviour
    {
        [SerializeField] private Configuration configuration;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            var objs = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");

            if (objs.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            GameManager.InitializeGameOnce(configuration, audioSource);
        }
    }
}