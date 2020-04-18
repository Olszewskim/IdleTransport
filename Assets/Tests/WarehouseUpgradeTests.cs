using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class WarehouseUpgradeTests {
        [TestCase(1, "20")]
        [TestCase(2, "22")]
        [TestCase(5, "28")]
        [TestCase(10, "42")]
        [TestCase(25, "163")]
        [TestCase(50, "1719")]
        [TestCase(100, "201216")]
        public void Warehouse_CargoPerCycle_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new WarehouseUpgrade();
            var upgradeValue = upgrade.CargoPerCycle.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }

        [TestCase(1, 7.0)]
        [TestCase(2, 6.95)]
        [TestCase(5, 6.8)]
        [TestCase(10, 6.55)]
        [TestCase(25, 5.8)]
        [TestCase(50, 4.55)]
        [TestCase(100, 2.05)]
        public void Warehouse_WorkCycleTime_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var upgrade = new WarehouseUpgrade();
            var upgradeValue = (double) upgrade.WorkCycleTime.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, "60")]
        [TestCase(2, "78")]
        [TestCase(5, "170")]
        [TestCase(10, "629")]
        [TestCase(25, "32130")]
        [TestCase(50, "22671299")]
        [TestCase(100, "11288701574364")]
        public void Warehouse_Capacity_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new WarehouseUpgrade();
            var upgradeValue = (BigInteger) upgrade.Capacity.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }
    }
}
