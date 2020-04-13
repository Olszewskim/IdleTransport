using IdleTransport.Utilities;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TrolleyUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public NumberOfUnitsUpgradeData NumberOfUnits { get; }
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public TrolleyUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Trolley]) {
            WorkCycleTime = (WorkCycleTimeUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.WorkCycleTime];
            NumberOfUnits = (NumberOfUnitsUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.NumberOfUnits];
            MovementSpeed = (MovementSpeedUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.MovementSpeed];
            Capacity = (CapacityUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.Capacity];
        }
    }
}
