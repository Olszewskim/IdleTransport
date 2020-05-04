using System.Collections.Generic;
using IdleTransport.Databases;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using static IdleTransport.Utilities.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTransport.StatsTest {
    public class StatsTestUI : SerializedMonoBehaviour {
        [SerializeField] private Dictionary<UnitType, Button> _unitTypeButtons;
        [SerializeField] private RowUI _headerRowUI;
        [SerializeField] private RowUI _rowUIPrefab;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_InputField _numberOfFloorsInputField;

        private List<RowUI> _rowUIs = new List<RowUI>();
        private UnitType _currentUnitType;
        private int _numberOfFloors = 1;

        private void Awake() {
            _rowUIPrefab.SetActive(false);
            _numberOfFloorsInputField.onEndEdit.AddListener(SetNumberOfFloors);
            _numberOfFloorsInputField.text = _numberOfFloors.ToString();
            InitButtons();
            RefreshView();
            ColorButtons();
        }

        private void InitButtons() {
            foreach (var button in _unitTypeButtons) {
                var key = button.Key;
                button.Value.onClick.AddListener(() => ChangeUnitType(key));
            }
        }

        private void ChangeUnitType(UnitType unitType) {
            _currentUnitType = unitType;
            ColorButtons();
            RefreshView();
        }

        private void ColorButtons() {
            foreach (var button in _unitTypeButtons) {
                var color = button.Key == _currentUnitType ? Constants.GREEN : Constants.WHITE;
                button.Value.image.color = color;
            }
        }

        private void RefreshView() {
            TurnOffRows();
            var upgrade = GetUnitUpgrade();
            var upgradeTypes = upgrade.GetUpgradesTypes();
            _headerRowUI.DisableUnusedCols(upgradeTypes);
            for (int i = 0; i < 100; i++) {
                if (i <= _rowUIs.Count) {
                    _rowUIs.Add(Instantiate(_rowUIPrefab, _rowUIPrefab.transform.parent));
                }

                _rowUIs[i].ShowRow(upgrade, upgradeTypes);
                upgrade.IncreaseUpgradeLevel(1);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }

        private void TurnOffRows() {
            for (int i = 0; i < _rowUIs.Count; i++) {
                _rowUIs[i].SetActive(false);
            }
        }

        private UnitUpgrade GetUnitUpgrade() {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            switch (_currentUnitType) {
                case UnitType.Warehouse:
                    return new WarehouseUpgrade(unitBaseParameters.warehouseUpgradeData);
                case UnitType.Trolley:
                    return new TrolleyUpgrade(unitBaseParameters.trolleyUpgradeData);
                case UnitType.Elevator:
                    var loadingRampsManager = new LoadingRampsManager(unitBaseParameters, _numberOfFloors);
                    return new ElevatorUpgrade(unitBaseParameters.elevatorUpgradeData, loadingRampsManager);
                case UnitType.Loader:
                    return new LoaderUpgrade(unitBaseParameters.loaderUpgradeData);
                case UnitType.Truck:
                    return new TruckUpgrade(unitBaseParameters.truckUpgradeData);
                default:
                    return null;
            }
        }

        private void SetNumberOfFloors(string text) {
            var number = int.Parse(text);
            if (number < 1) {
                number = 1;
                _numberOfFloorsInputField.text = number.ToString();
            }

            _numberOfFloors = number;
            RefreshView();
        }
    }
}
