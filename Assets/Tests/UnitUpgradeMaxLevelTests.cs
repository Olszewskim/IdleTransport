using IdleTransport.Databases;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.ScriptableObjectData;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class UnitUpgradeMaxLevelTests {
        private UnitBaseParameters _unitBaseParameters;

        [OneTimeSetUp]
        public void BeforeTests() {
            _unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
        }

        [Test]
        public void Upgrade_Level_Cannot_Be_Higher_Than_Max_Level() {
            var upgrade = new WarehouseUpgrade(_unitBaseParameters.warehouseUpgradeData);
            var maxLevel = _unitBaseParameters.warehouseUpgradeData.maxUpgradeLevel;
            upgrade.SetLevel(1);
            upgrade.IncreaseUpgradeLevel(maxLevel+100);
            Assert.AreEqual(maxLevel, upgrade.UpgradeLevel);
        }

        [Test]
        public void Attempt_Of_Getting_Cost_Of_Level_Higher_Than_Max_Returns_Cost_Of_Max_Level() {
            var upgrade = new WarehouseUpgrade(_unitBaseParameters.warehouseUpgradeData);
            var maxLevel = _unitBaseParameters.warehouseUpgradeData.maxUpgradeLevel;
            var maxLevelUpgradeCost = upgrade.GetUpgradeCost(maxLevel);
            var higherThanMaxLevelUpgradeCost = upgrade.GetUpgradeCost(maxLevel + 100);
            Assert.AreEqual(maxLevelUpgradeCost, higherThanMaxLevelUpgradeCost);
        }

        [Test]
        public void GetPossibleUpgradesCount_Returns_Upgrades_Count_Remains_To_Max_Level() {
            var upgrade = new TruckUpgrade(_unitBaseParameters.truckUpgradeData);
            var maxLevel = _unitBaseParameters.truckUpgradeData.maxUpgradeLevel;
            var currency =
                new BigInteger(
                    "999999999999999999999999999999999999999999999999999999999999999999999999999");
            upgrade.SetLevel(maxLevel - 5);
            Assert.AreEqual(5, upgrade.GetPossibleUpgradesCount(currency));

        }
    }
}

