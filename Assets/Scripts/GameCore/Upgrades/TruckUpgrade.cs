using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TruckUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CapacityUpgradeData Capacity { get; }

        public TruckUpgrade(TruckUpgradeData truckUpgradeData) : base(truckUpgradeData.upgradeCost) {
            WorkCycleTime = truckUpgradeData.workCycleTime;
            Capacity = truckUpgradeData.capacity;
        }

        protected override UpgradeData GetUpgradeData(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime;
                case UpgradeType.Capacity:
                    return Capacity;
                default:
                    Debug.LogError("Truck doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
