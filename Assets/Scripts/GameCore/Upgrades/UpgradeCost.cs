using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Upgrades {
    public struct UpgradeCost {
        public BigInteger BaseCost { get; }
        public double CostMultiplier { get; }

        public UpgradeCost(BigInteger baseCost, double costMultiplier) {
            BaseCost = baseCost;
            CostMultiplier = costMultiplier;
        }
    }
}
