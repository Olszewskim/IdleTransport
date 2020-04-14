using System;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public abstract class UnitUpgrade {
        public event Action OnUpgradeLevelUp;

        [ShowInInspector] public int UpgradeLevel { get; private set; }

        private UpgradeCost _upgradeCost;

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
                totalCost += GetUpgradeCost(i);
            }

            return totalCost;
        }

        public BigInteger GetUpgradeCost(int upgradeLevel) {
            if (upgradeLevel < 2) {
                return 0;
            }

            if (upgradeLevel == 2) {
                return _upgradeCost.BaseCost;
            }

            return GetUpgradeCost(upgradeLevel - 1).MultipleByDouble(_upgradeCost.CostMultiplier);
        }

        public int GetPossibleUpgradesCount(BigInteger currencyAmount) {
            var upgradesCount = 1;
            var totalCost = new BigInteger(0);
            while (currencyAmount >= totalCost) {
                totalCost += GetUpgradeCost(UpgradeLevel + upgradesCount);
                upgradesCount++;
            }

            return upgradesCount - 2;
        }

        public void IncreaseUpgradeLevel() {
            IncreaseUpgradeLevel(1);
        }

        public void IncreaseUpgradeLevel(int levelsToAdd) {
            UpgradeLevel += levelsToAdd;
            OnUpgradeLevelUp?.Invoke();
        }

        public abstract object GetUpgradeValue(UpgradeType upgradeType);
    }
}
