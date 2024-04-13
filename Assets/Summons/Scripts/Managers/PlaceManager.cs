using System.Collections.Generic;
using Summons.Scripts.Data;
using Summons.Scripts.Models;
using Summons.Scripts.ViewCtrls;
using UnityEngine;

namespace Summons.Scripts.Managers
{
    public class PlaceManager : MonoBehaviour
    {
        private static readonly Dictionary<PlaceType, GameObject> PlacePrefabDict = new();
        private static PlaceType _startingPlace;
        public static PlaceType Current { get; private set; }
        private PlaceCtrlBase _currentPlaceCtrl;
        private readonly Dictionary<PlaceType, PlaceState> _placeStates = new();

        public static void Initialize(PlacesConfig config)
        {
            foreach (var entry in config.places)
                PlacePrefabDict.Add(entry.type, entry.placePrefab);
            _startingPlace = config.startingPlace;
            Current = PlaceType.None;
        }

        public void Start()
        {
            Load(_startingPlace);
        }

        public void Load(PlaceType placeType)
        {
            if (Current == placeType) return;

            GameObject placePrefab = PlacePrefabDict[placeType];
            GameObject instance = Instantiate(placePrefab, transform);
            PlaceCtrlBase placeCtrl = instance.GetComponent<PlaceCtrlBase>();

            PlaceState state;

            if (_currentPlaceCtrl != null)
            {
                state = _currentPlaceCtrl.OnExitPlace();
                _placeStates[Current] = state;
                Destroy(_currentPlaceCtrl);
            }

            state = _placeStates.GetValueOrDefault(placeType, null);
            Current = placeType;
            _currentPlaceCtrl = placeCtrl;
            _currentPlaceCtrl.OnEnterPlace(state);
        }
    }
}