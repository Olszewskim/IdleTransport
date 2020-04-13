using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class UnitUpgradesCostTests {
        [TestCase(1, "0")]
        [TestCase(2, "10")]
        [TestCase(5, "13")]
        [TestCase(10, "25")]
        [TestCase(25, "215")]
        [TestCase(50, "8671")]
        [TestCase(100, "14481724")]
        public void Warehouse_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new WarehouseUpgrade();
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "240")]
        [TestCase(5, "414")]
        [TestCase(10, "1027")]
        [TestCase(25, "15789")]
        [TestCase(50, "1506038")]
        [TestCase(100, "13705588286")]
        public void Trolley_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new TrolleyUpgrade();
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "240")]
        [TestCase(5, "414")]
        [TestCase(10, "1027")]
        [TestCase(25, "15789")]
        [TestCase(50, "1506038")]
        [TestCase(100, "13705588286")]
        public void Elevator_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new ElevatorUpgrade();
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "240")]
        [TestCase(5, "414")]
        [TestCase(10, "1027")]
        [TestCase(25, "15789")]
        [TestCase(50, "1506038")]
        [TestCase(100, "13705588286")]
        public void Loader_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new LoaderUpgrade();
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost), upgradeCost);
        }

        [TestCase(1, "0")]
        [TestCase(2, "300")]
        [TestCase(5, "518")]
        [TestCase(10, "1286")]
        [TestCase(25, "19786")]
        [TestCase(50, "1887348")]
        [TestCase(100, "17175674984")]
        public void Truck_Upgrade_Level_Up_Cost_Is_Correct(int upgradeLevel, string expectedUpgradeCost) {
            var upgrade = new TruckUpgrade();
            var upgradeCost = upgrade.GetUpgradeCost(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeCost), upgradeCost);
        }
    }
}
