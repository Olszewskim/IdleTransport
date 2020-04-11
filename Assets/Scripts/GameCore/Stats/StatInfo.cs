using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Stats {
    public class StatInfo {
        public StatType StatType { get; }
        public string StatValue { get; }
        public string StatValueAfterUpgradeBonus { get; }

        public StatInfo(StatType statType, string statValue, string statValueAfterUpgradeBonus) {
            StatType = statType;
            StatValue = statValue;
            StatValueAfterUpgradeBonus = statValueAfterUpgradeBonus;
        }
    }
}
