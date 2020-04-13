using IdleTransport.Utilities;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class ElevatorUpgrade : UnitUpgrade {
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public ElevatorUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Elevator]) {
            MovementSpeed = (MovementSpeedUpgradeData) Constants.ELEVATOR_UPGRADE_DATA[UpgradeType.MovementSpeed];
            Capacity = (CapacityUpgradeData) Constants.ELEVATOR_UPGRADE_DATA[UpgradeType.Capacity];
        }
    }
}
