using System;
using System.Collections.Generic;
using Summons.Scripts.Models;
using UnityEngine;

namespace Summons.Scripts.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Places Config", fileName = "places")]
    public class PlacesConfig : ScriptableObject
    {
        public List<PlaceEntry> places = new();
        public PlaceType startingPlace;

        [Serializable]
        public class PlaceEntry
        {
            public PlaceType type;
            public GameObject placePrefab;
        }
    }
}