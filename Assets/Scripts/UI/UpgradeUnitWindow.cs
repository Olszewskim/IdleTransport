using System;
using System.Collections.Generic;
using IdleTransport.Databases;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Currencies;
using IdleTransport.GameCore.Models;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.UI {
    public class UpgradeUnitWindow : WindowBehaviour<UpgradeUnitWindow> {
        [SerializeField] private TextMeshProUGUI _upgradingUnitTitleText;
        [SerializeField] private Image _upgradingUnitIcon;
        [SerializeField] private ButtonWithCurrencyCost _upgradeButton;
        [SerializeField] private StatInfoRowUI _statInfoRowUIPrefab;
        [SerializeField] private ToggleGroup _upgradesMultiplierToggleGroupGroup;

        private UpgradeMultiplierMode _currentUpgradeMultiplierMode;
        private readonly List<StatInfoRowUI> _statInfoRowUIList = new List<StatInfoRowUI>();

        private UnitData _currentlyUpgradingUnit;
        private int _numberOfUpgrades;
        private Currency _playerCurrency;

        protected override void Awake() {
            base.Awake();
            _statInfoRowUIPrefab.SetActive(false);
            _upgradeButton.OnClickAction += UpgradeUnit;
            InitUpgradesMultiplierToggleGroup();
        }

        private void Start() {
            _playerCurrency = PlayerManager.Instance.Player.GetCurrencyType(CurrencyType.Gold);
            _playerCurrency.OnCurrencyAmountChanged += TryRefreshMaxNumberOfUpgrade;
        }

        private void InitUpgradesMultiplierToggleGroup() {
            var toggles = _upgradesMultiplierToggleGroupGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; i++) {
                var upgradeMultiplierMode = (UpgradeMultiplierMode) i;
                toggles[i].onValueChanged.AddListener(state => {
                    if (state) {
                        SwitchUpgradesMultiplierMode(upgradeMultiplierMode);
                    }
                });
            }
        }

        private void SwitchUpgradesMultiplierMode(UpgradeMultiplierMode upgradeMultiplierMode) {
            _currentUpgradeMultiplierMode = upgradeMultiplierMode;
            RefreshUpgradeButton();
        }

        public void ShowWindow(UnitData unitData) {
            base.ShowWindow();
            _currentlyUpgradingUnit = unitData;
            _upgradingUnitIcon.sprite = GameResourcesDatabase.GetUnitSprite(unitData.UnitType);
            RefreshView();
        }

        private void RefreshView() {
            _upgradingUnitTitleText.text =
                $"{GameTexts.GetUnitName(_currentlyUpgradingUnit.UnitType)} {GameTexts.GetLevelText(_currentlyUpgradingUnit.UnitUpgrade.UpgradeLevel)}";
            RefreshUpgradeButton();
            ShowUnitStats();
        }

        private void RefreshUpgradeButton() {
            _numberOfUpgrades = GetNumberOfUpgrades();
            var upgradeCost = _currentlyUpgradingUnit.UnitUpgrade.GetNextNUpgradesCost(_numberOfUpgrades);
            _upgradeButton.SetNewCurrencyCost(upgradeCost);
            _upgradeButton.SetButtonText(GameTexts.GetLevelUpMultiplierText(_numberOfUpgrades));
        }

        private void UpgradeUnit() {
            _currentlyUpgradingUnit.UpgradeUnit(_numberOfUpgrades);
            RefreshView();
        }

        private void ShowUnitStats() {
            TurnOffAllStatInfoRows();
            var unitStats = _currentlyUpgradingUnit.GetUnitStats();
            for (int i = 0; i < unitStats.Count; i++) {
                if (i >= _statInfoRowUIList.Count) {
                    _statInfoRowUIList.Add(Instantiate(_statInfoRowUIPrefab, _statInfoRowUIPrefab.transform.parent));
                }

                _statInfoRowUIList[i].InitView(unitStats[i]);
            }
        }

        private void TurnOffAllStatInfoRows() {
            for (int i = 0; i < _statInfoRowUIList.Count; i++) {
                _statInfoRowUIList[i].SetActive(false);
            }
        }

        private int GetNumberOfUpgrades() {
            switch (_currentUpgradeMultiplierMode) {
                case UpgradeMultiplierMode.x1:
                    return 1;
                case UpgradeMultiplierMode.x10:
                    return 10;
                case UpgradeMultiplierMode.x50:
                    return 50;
                case UpgradeMultiplierMode.Max:
                    return _currentlyUpgradingUnit.UnitUpgrade.GetPossibleUpgradesCount(_playerCurrency.CurrencyAmount);
                default:
                    return 1;
            }
        }

        private void TryRefreshMaxNumberOfUpgrade(BigInteger currencyAmount) {
            if (_currentUpgradeMultiplierMode == UpgradeMultiplierMode.Max && IsOpen) {
                RefreshView();
            }
        }
    }
}
