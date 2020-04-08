using System.Collections.Generic;
using IdleTransport.Databases;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.UI {
    public class UpgradeUnitWindow : WindowBehaviour<UpgradeUnitWindow> {
        [SerializeField] private TextMeshProUGUI _upgradingUnitTitleText;
        [SerializeField] private Image _upgradingUnitIcon;
        [SerializeField] private TextMeshProUGUI _upgradeCostText;
        [SerializeField] private ButtonWithText _upgradeButton;
        [SerializeField] private StatInfoRowUI _statInfoRowUIPrefab;
        [SerializeField] private ToggleGroup _upgradesMultiplierToggleGroupGroup;

        private UpgradeMultiplierMode _currentUpgradeMultiplierMode;
        private readonly List<StatInfoRowUI> _statInfoRowUIList = new List<StatInfoRowUI>();

        protected override void Awake() {
            base.Awake();
            _statInfoRowUIPrefab.SetActive(false);
            InitUpgradesMultiplierToggleGroup();
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
        }

        public void ShowWindow(UnitData unitData) {
            base.ShowWindow();
            _upgradingUnitTitleText.text = unitData.UnitType.ToString();
            _upgradingUnitIcon.sprite = GameResourcesDatabase.GetUnitSprite(unitData.UnitType);

        }
    }
}
