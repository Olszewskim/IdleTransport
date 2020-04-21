using IdleTransport.Databases;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.ScriptableObjectData;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class LoaderUpgradeTests {
        private UnitBaseParameters _unitBaseParameters;

        [OneTimeSetUp]
        public void BeforeTests() {
            _unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
        }

        [TestCase(1, 4.0)]
        [TestCase(2, 3.95)]
        [TestCase(5, 3.8)]
        [TestCase(10, 3.55)]
        [TestCase(25, 2.8)]
        [TestCase(50, 1.55)]
        [TestCase(100, -0.95)]
        public void Loader_WorkCycleTime_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var upgrade = new LoaderUpgrade(_unitBaseParameters.loaderUpgradeData);
            var upgradeValue = (double) upgrade.WorkCycleTime.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, 4.0)]
        [TestCase(2, 3.95)]
        [TestCase(5, 3.8)]
        [TestCase(10, 3.55)]
        [TestCase(25, 2.8)]
        [TestCase(50, 1.55)]
        [TestCase(100, -0.95)]
        public void Loader_MovementSpeed_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var upgrade = new LoaderUpgrade(_unitBaseParameters.loaderUpgradeData);
            var upgradeValue = (double) upgrade.MovementSpeed.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, "25")]
        [TestCase(2, "32")]
        [TestCase(5, "68")]
        [TestCase(10, "249")]
        [TestCase(25, "12650")]
        [TestCase(50, "8925502")]
        [TestCase(100, "4444267362580")]
        public void Loader_Capacity_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new LoaderUpgrade(_unitBaseParameters.loaderUpgradeData);
            var upgradeValue = (BigInteger) upgrade.Capacity.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }

        [TestCase(1, "1")]
        [TestCase(10, "2")]
        [TestCase(50, "3")]
        [TestCase(100, "4")]
        [TestCase(200, "5")]
        [TestCase(400, "6")]
        public void Loader_NumberOfUnits_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var upgrade = new LoaderUpgrade(_unitBaseParameters.loaderUpgradeData);
            var upgradeValue = (BigInteger) upgrade.NumberOfUnits.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }
    }
}
