using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class UnitUpgradePossibleUpgradesCountTests {
        [TestCase("0", 1)]
        [TestCase("1 000", 21)]
        [TestCase("1 000 000", 67)]
        [TestCase("1 000 000 000", 114)]
        [TestCase("1 000 000 000 000", 160)]
        [TestCase("1 000 000 000 000 000", 207)]
        [TestCase("1 000 000 000 000 000 000", 253)]
        [TestCase("1 000 000 000 000 000 000 000", 300)]
        [TestCase("1 000 000 000 000 000 000 000 000", 346)]
        [TestCase("1 000 000 000 000 000 000 000 000 000", 393)]
        [TestCase("1 000 000 000 000 000 000 000 000 000 000", 439)]
        public void Warehouse_Possible_Upgrades_Count_Is_Correct(string currencyAmountString, int possibleUpgrades) {
            var upgrade = new WarehouseUpgrade();
            var currencyAmount = new BigInteger(currencyAmountString.Replace(" ", string.Empty));
            Debug.Log(currencyAmount.FormatHugeNumber());
            var upgradesCount = upgrade.GetPossibleUpgradesCount(currencyAmount);
            Assert.AreEqual(possibleUpgrades, upgradesCount);
        }
    }
}
