using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using static IdleTransport.Utilities.Enums;
using UnityEngine;

namespace IdleTransport.StatsTest {
    public class RowUI : SerializedMonoBehaviour {
        private const int SCROLL_BAR_WIDTH = 20;
        private const int SPACING = 10;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private TextMeshProUGUI _totalProductionText;
        [SerializeField] private Dictionary<UpgradeType, TextMeshProUGUI> _upgradeTexts;

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

        public void ShowRow(UnitUpgrade upgrade, List<UpgradeType> upgradeTypes) {
            DisableUnusedCols(upgradeTypes);
            _levelText.text = upgrade.UpgradeLevel.ToString();
            _costText.text = $"{Sprites.goldSprite} {upgrade.GetUpgradeCost(upgrade.UpgradeLevel).FormatHugeNumber()}";
            _totalProductionText.text = upgrade.GetTotalProductionDesc();
            for (int i = 0; i < upgradeTypes.Count; i++) {
                _upgradeTexts[upgradeTypes[i]].text = upgrade.GetUpgradeValueDesc(upgradeTypes[i]);
            }

            gameObject.SetActive(true);
        }
    }
}
