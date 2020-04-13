using IdleTransport.Utilities;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class WarehouseUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CargoPerCycleUpgradeData CargoPerCycle { get; }
        public CapacityUpgradeData Capacity { get; }

        public WarehouseUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Warehouse]) {
            WorkCycleTime = (WorkCycleTimeUpgradeData) Constants.WAREHOUSE_UPGRADE_DATA[UpgradeType.WorkCycleTime];
            CargoPerCycle = (CargoPerCycleUpgradeData) Constants.WAREHOUSE_UPGRADE_DATA[UpgradeType.CargoPerCycle];
            Capacity = (CapacityUpgradeData) Constants.WAREHOUSE_UPGRADE_DATA[UpgradeType.Capacity];
        }
    }
}
