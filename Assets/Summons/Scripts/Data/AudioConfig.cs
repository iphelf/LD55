using System;
using System.Collections.Generic;
using Summons.Scripts.Managers;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Audio Config", fileName = "audio")]
    public class AudioConfig : ScriptableObject
    {
        [Serializable]
        public struct SfxEntry
        {
            public SfxKey key;
            public AudioClip sfx;
        }

        public List<SfxEntry> sfxList;

        [Serializable]
        public struct MusicEntry
        {
            public MusicKey key;
            public AudioClip music;
        }

        public List<MusicEntry> musicList;
    }
}