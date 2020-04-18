using System;
using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public abstract class UnitUpgrade {
        public event Action OnUpgradeLevelUp;

        [ShowInInspector] public int UpgradeLevel { get; private set; }

        private UpgradeCost _upgradeCost;
        private readonly Dictionary<int, BigInteger> _upgradeCostMap = new Dictionary<int, BigInteger>();

        public UnitUpgrade(UpgradeCost upgradeCost) {
            UpgradeLevel = 1;
            _upgradeCost = upgradeCost;
        }

        public void SetLevel(int level) {
            UpgradeLevel = level;
        }

        public BigInteger GetNextNUpgradesCost(int upgradesCount) {
            var totalCost = new BigInteger(0);
            var startFromLevel = UpgradeLevel + 1;
            for (int i = startFromLevel; i < startFromLevel + upgradesCount; i++) {
                var upgradeCost = GetUpgradeCost(i);
                totalCost += upgradeCost;
                TryAddCostToDictionary(i, upgradeCost);
            }

            return totalCost;
        }

        public BigInteger GetUpgradeCost(int upgradeLevel) {
            if (_upgradeCostMap.ContainsKey(upgradeLevel)) {
                return _upgradeCostMap[upgradeLevel];
            }

            if (upgradeLevel < 2) {
                return 0;
            }

            if (upgradeLevel == 2) {
                return _upgradeCost.GetBaseCost();
            }

            return GetUpgradeCost(upgradeLevel - 1).MultipleByDouble(_upgradeCost.costMultiplier);
        }

        public int GetPossibleUpgradesCount(BigInteger currencyAmount) {
            var upgradesCount = 1;
            var totalCost = new BigInteger(0);
            while (currencyAmount >= totalCost) {
                var currentLevel = UpgradeLevel + upgradesCount;
                var upgradeCost = GetUpgradeCost(currentLevel);
                totalCost += upgradeCost;
                TryAddCostToDictionary(currentLevel, upgradeCost);
                upgradesCount++;
            }

            return Mathf.Max(upgradesCount - 2, 1);
        }

        private void TryAddCostToDictionary(int upgradeLevel, BigInteger upgradeCost) {
            if (!_upgradeCostMap.ContainsKey(upgradeLevel)) {
                _upgradeCostMap.Add(upgradeLevel, upgradeCost);
            }
        }

        public void IncreaseUpgradeLevel(int levelsToAdd) {
            UpgradeLevel += levelsToAdd;
            OnUpgradeLevelUp?.Invoke();
        }

        protected abstract UpgradeData GetUpgradeData(UpgradeType upgradeType);

        public object GetUpgradeValue(UpgradeType upgradeType, int level) {
            return GetUpgradeData(upgradeType)?.GetUpgradeValue(level);
        }

        public string GetUpgradeValueDesc(UpgradeType upgradeType) {
            return GetUpgradeData(upgradeType)?.GetUpgradeValueDesc(UpgradeLevel);
        }

        public string GetAfterUpgradeBonus(UpgradeType upgradeType, int afterUpgradeLevel) {
            return GetUpgradeData(upgradeType)?.GetAfterUpgradeBonus(UpgradeLevel, afterUpgradeLevel);
        }
    }
}
