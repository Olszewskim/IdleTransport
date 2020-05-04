using System;
using System.Collections.Generic;
using System.Linq;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using IdleTransport.Utilities.DynamicScrollList;
using Sirenix.OdinInspector;
using TMPro;
using static IdleTransport.Utilities.Enums;
using UnityEngine;

namespace IdleTransport.StatsTest {
    public class RowUIData {
        public string level;
        public string cost;
        public string totalProduction;
        public Dictionary<UpgradeType, string> upgrades;

        public RowUIData(UnitUpgrade upgrade, List<UpgradeType> upgradeTypes) {
            level = upgrade.UpgradeLevel.ToString();
            cost = $"{Sprites.goldSprite} {upgrade.GetUpgradeCost(upgrade.UpgradeLevel).FormatHugeNumber()}";
            totalProduction = upgrade.GetTotalProductionDesc();
            upgrades = new Dictionary<UpgradeType, string>();
            for (int i = 0; i < upgradeTypes.Count; i++) {
                upgrades.Add(upgradeTypes[i], upgrade.GetUpgradeValueDesc(upgradeTypes[i]));
            }
        }
    }

    public class RowUI : DynamicScrollObject<RowUIData> {
        private const int SCROLL_BAR_WIDTH = 20;
        private const int SPACING = 10;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private TextMeshProUGUI _totalProductionText;
        [SerializeField] private Dictionary<UpgradeType, TextMeshProUGUI> _upgradeTexts;

        public override float CurrentHeight { get; set; }
        public override float CurrentWidth { get; set; }

        private void Awake() {
            var rectTransform = transform as RectTransform;
            CurrentHeight = rectTransform.rect.height;
            CurrentWidth = rectTransform.rect.width;
        }

        public void DisableUnusedCols(List<UpgradeType> upgradeTypes) {
            var noOfElements = upgradeTypes.Count + 3;
            var widthPerElement = (Screen.width - SCROLL_BAR_WIDTH - (noOfElements - 1) * SPACING) / noOfElements;

            foreach (var upgradeText in _upgradeTexts) {
                upgradeText.Value.SetActive(upgradeTypes.Contains(upgradeText.Key));
                upgradeText.Value.rectTransform.sizeDelta =
                    new Vector2(widthPerElement, upgradeText.Value.rectTransform.sizeDelta.y);
            }

            _levelText.rectTransform.sizeDelta = new Vector2(widthPerElement, _levelText.rectTransform.sizeDelta.y);
            _costText.rectTransform.sizeDelta = new Vector2(widthPerElement, _costText.rectTransform.sizeDelta.y);
            _totalProductionText.rectTransform.sizeDelta =
                new Vector2(widthPerElement, _totalProductionText.rectTransform.sizeDelta.y);
        }

        public override void UpdateScrollObject(RowUIData item, int index) {
            base.UpdateScrollObject(item, index);
            DisableUnusedCols(item.upgrades.Keys.ToList());
            _levelText.text = item.level;
            _costText.text = item.cost;
            _totalProductionText.text = item.totalProduction;
            foreach (var upgrade in item.upgrades) {
                _upgradeTexts[upgrade.Key].text = upgrade.Value;
            }

            gameObject.SetActive(true);
        }
    }
}
