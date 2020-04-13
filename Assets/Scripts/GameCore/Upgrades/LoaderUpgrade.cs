using IdleTransport.Utilities;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class LoaderUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public NumberOfUnitsUpgradeData NumberOfUnits { get; }
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public LoaderUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Loader]) {
            WorkCycleTime = (WorkCycleTimeUpgradeData) Constants.LOADER_UPGRADE_DATA[UpgradeType.WorkCycleTime];
            NumberOfUnits = (NumberOfUnitsUpgradeData) Constants.LOADER_UPGRADE_DATA[UpgradeType.NumberOfUnits];
            MovementSpeed = (MovementSpeedUpgradeData) Constants.LOADER_UPGRADE_DATA[UpgradeType.MovementSpeed];
            Capacity = (CapacityUpgradeData) Constants.LOADER_UPGRADE_DATA[UpgradeType.Capacity];
        }
    }
}
