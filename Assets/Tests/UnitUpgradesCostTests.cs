using IdleTransport.Databases;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Managers;
using IdleTransport.ScriptableObjectData;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class UnitUpgradesCostTests {
        private UnitBaseParameters _unitBaseParameters;

        [OneTimeSetUp]
        public void BeforeTests() {
            _unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
        }

        [TestCase(1, "0")]
        [TestCase(2, "10")]
        [TestCase(5, "13")]
        [TestCase(10, "25")]
        [TestCase(25, "215")]
        [TestCase(50, "8671")]
        [TestCase(100, "14481724")]
        public void Warehouse_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new WarehouseUpgrade(_unitBaseParameters.warehouseUpgradeData);
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost.Replace(" ", string.Empty)), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "240")]
        [TestCase(5, "414")]
        [TestCase(10, "1027")]
        [TestCase(25, "15789")]
        [TestCase(50, "1506038")]
        [TestCase(100, "13705588286")]
        public void Trolley_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new TrolleyUpgrade(_unitBaseParameters.trolleyUpgradeData);
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost.Replace(" ", string.Empty)), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "240")]
        [TestCase(5, "414")]
        [TestCase(10, "1027")]
        [TestCase(25, "15789")]
        [TestCase(50, "1506038")]
        [TestCase(100, "13705588286")]
        public void Elevator_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var loadingRampsManager = new LoadingRampsManager(_unitBaseParameters);
            var upgrade = new ElevatorUpgrade(_unitBaseParameters.elevatorUpgradeData, loadingRampsManager);
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost.Replace(" ", string.Empty)), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "240")]
        [TestCase(5, "414")]
        [TestCase(10, "1027")]
        [TestCase(25, "15789")]
        [TestCase(50, "1506038")]
        [TestCase(100, "13705588286")]
        public void Loader_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new LoaderUpgrade(_unitBaseParameters.loaderUpgradeData);
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost.Replace(" ", string.Empty)), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "300")]
        [TestCase(5, "518")]
        [TestCase(10, "1286")]
        [TestCase(25, "19786")]
        [TestCase(50, "1887348")]
        [TestCase(100, "17175674984")]
        public void Truck_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new TruckUpgrade(_unitBaseParameters.truckUpgradeData);
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost.Replace(" ", string.Empty)), upgradeCost);
        }
    }
}
