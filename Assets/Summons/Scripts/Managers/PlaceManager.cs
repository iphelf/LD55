using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls;
using Summons.Scripts.ViewCtrls.Places;
using UnityEngine;

namespace Summons.Scripts.Managers
{
    public class PlaceManager : MonoBehaviour
    {
        private static readonly Dictionary<PlaceType, GameObject> PlacePrefabDict = new();
        private static PlaceType _startingPlace;
        private readonly Dictionary<PlaceType, PlaceState> _placeStates = new();
        private PlaceCtrlBase _currentPlaceCtrl;
        public static PlaceType Current { get; private set; }

        public void Start()
        {
            Load(_startingPlace);
        }

        public static void Initialize(PlacesConfig config)
        {
            foreach (var entry in config.places)
                PlacePrefabDict.Add(entry.type, entry.placePrefab);
            _startingPlace = config.startingPlace;
            Current = PlaceType.None;
        }

        public void Load(PlaceType placeType)
        {
            if (Current == placeType) return;

            var placePrefab = PlacePrefabDict[placeType];
            var instance = Instantiate(placePrefab, transform);
            var placeCtrl = instance.GetComponent<PlaceCtrlBase>();

            PlaceState state;

            if (_currentPlaceCtrl != null)
            {
                state = _currentPlaceCtrl.OnExitPlace();
                _placeStates[Current] = state;
                Destroy(_currentPlaceCtrl.gameObject);
            }

            state = _placeStates.GetValueOrDefault(placeType, null);
            Current = placeType;
            _currentPlaceCtrl = placeCtrl;
            _currentPlaceCtrl.OnEnterPlace(state);
        }
    }
}