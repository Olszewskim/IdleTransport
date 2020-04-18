using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class TruckUpgradeTests {
        [TestCase(1, 7.0)]
        [TestCase(2, 6.95)]
        [TestCase(5, 6.8)]
        [TestCase(10, 6.55)]
        [TestCase(25, 5.8)]
        [TestCase(50, 4.55)]
        [TestCase(100, 2.05)]
        public void Truck_WorkCycleTime_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var upgrade = new TruckUpgrade();
            var upgradeValue = (double)upgrade.WorkCycleTime.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, "30")]
        [TestCase(2, "39")]
        [TestCase(5, "84")]
        [TestCase(10, "308")]
        [TestCase(25, "15709")]
        [TestCase(50, "11083837")]
        [TestCase(100, "5518965775142")]
        public void Truck_Capacity_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new TruckUpgrade();
            var upgradeValue = (BigInteger)upgrade.Capacity.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }
    }
}
