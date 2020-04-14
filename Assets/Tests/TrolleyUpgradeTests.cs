using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class TrolleyUpgradeTests {

        [TestCase(1, 4.0)]
        [TestCase(2, 3.95)]
        [TestCase(5, 3.8)]
        [TestCase(10, 3.55)]
        [TestCase(25, 2.8)]
        [TestCase(50, 1.55)]
        [TestCase(100, -0.95)]
        public void Trolley_WorkCycleTime_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var upgrade = new TrolleyUpgrade();
            var upgradeValue = upgrade.WorkCycleTime.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, 3.0)]
        [TestCase(2, 2.95)]
        [TestCase(5, 2.8)]
        [TestCase(10, 2.55)]
        [TestCase(25, 1.8)]
        [TestCase(50, 0.55)]
        [TestCase(100, -1.95)]
        public void Trolley_MovementSpeed_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var upgrade = new TrolleyUpgrade();
            var upgradeValue = upgrade.MovementSpeed.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, "25")]
        [TestCase(2, "32")]
        [TestCase(5, "68")]
        [TestCase(10, "249")]
        [TestCase(25, "12650")]
        [TestCase(50, "8925502")]
        [TestCase(100, "4444267362580")]
        public void Trolley_Capacity_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new TrolleyUpgrade();
            var upgradeValue = upgrade.Capacity.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }

        [TestCase(1, "1")]
        [TestCase(10, "2")]
        [TestCase(50, "3")]
        [TestCase(100, "4")]
        [TestCase(200, "5")]
        [TestCase(400, "6")]
        public void Trolley_NumberOfUnits_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new TrolleyUpgrade();
            var upgradeValue = upgrade.NumberOfUnits.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }

    }
}
