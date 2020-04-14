using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TruckUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CapacityUpgradeData Capacity { get; }

        public TruckUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Truck]) {
            WorkCycleTime = (WorkCycleTimeUpgradeData) Constants.TRUCK_UPGRADE_DATA[UpgradeType.WorkCycleTime];
            Capacity = (CapacityUpgradeData) Constants.TRUCK_UPGRADE_DATA[UpgradeType.Capacity];
        }

        public override object GetUpgradeValue(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime.GetUpgradeValue(UpgradeLevel);
                case UpgradeType.Capacity:
                    return Capacity.GetUpgradeValue(UpgradeLevel);
                default:
                    Debug.LogError("Elevator doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
