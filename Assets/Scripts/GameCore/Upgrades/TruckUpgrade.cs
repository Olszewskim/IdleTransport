using IdleTransport.Utilities;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TruckUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CapacityUpgradeData Capacity { get; }

        public TruckUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Truck]) {
            WorkCycleTime = (WorkCycleTimeUpgradeData) Constants.TRUCK_UPGRADE_DATA[UpgradeType.WorkCycleTime];
            Capacity = (CapacityUpgradeData) Constants.TRUCK_UPGRADE_DATA[UpgradeType.Capacity];
        }
    }
}
