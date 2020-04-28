using IdleTransport.Databases;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Managers;
using IdleTransport.ScriptableObjectData;
using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {
    public class ElevatorUpgradeTests {
        private UnitBaseParameters _unitBaseParameters;

        [OneTimeSetUp]
        public void BeforeTests() {
            _unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
        }

        [TestCase(1, 2.5)]
        [TestCase(2, 2.49)]
        [TestCase(5, 2.46)]
        [TestCase(10, 2.41)]
        [TestCase(25, 2.26)]
        [TestCase(50, 2.01)]
        [TestCase(100, 1.51)]
        public void Elevator_MovementSpeed_Upgrade_Value_Is_Correct(int upgradeLevel, double expectedUpgradeValue) {
            var loadingRampsManager = new LoadingRampsManager(_unitBaseParameters);
            var upgrade = new ElevatorUpgrade(_unitBaseParameters.elevatorUpgradeData, loadingRampsManager);
            var upgradeValue = (double)upgrade.MovementSpeed.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(expectedUpgradeValue, upgradeValue, delta: 0.001f);
        }

        [TestCase(1, "20")]
        [TestCase(2, "26")]
        [TestCase(5, "54")]
        [TestCase(10, "198")]
        [TestCase(25, "10085")]
        [TestCase(50, "7115345")]
        [TestCase(100, "3542937493568")]
        public void Elevator_Capacity_Upgrade_Value_Is_Correct(int upgradeLevel, string expectedUpgradeValue) {
            var loadingRampsManager = new LoadingRampsManager(_unitBaseParameters);
            var upgrade = new ElevatorUpgrade(_unitBaseParameters.elevatorUpgradeData, loadingRampsManager);
            var upgradeValue = (BigInteger)upgrade.Capacity.GetUpgradeValue(upgradeLevel);
            Assert.AreEqual(new BigInteger(expectedUpgradeValue.Replace(" ", string.Empty)), upgradeValue);
        }
    }
}
