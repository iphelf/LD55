using System;
using System.Collections.Generic;
using Summons.Scripts.Managers;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Audio Config", fileName = "audio")]
    public class AudioConfig : ScriptableObject
    {
        public List<SfxEntry> sfxList;

        public List<MusicEntry> musicList;

        [Serializable]
        public struct SfxEntry
        {
            public SfxKey key;
            public AudioClip sfx;
        }

        [Serializable]
        public struct MusicEntry
        {
            public MusicKey key;
            public AudioClip music;
        }
    }
}