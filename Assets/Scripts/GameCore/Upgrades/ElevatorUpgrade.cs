using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class ElevatorUpgrade : UnitUpgrade {
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public ElevatorUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Elevator]) {
            MovementSpeed = (MovementSpeedUpgradeData) Constants.ELEVATOR_UPGRADE_DATA[UpgradeType.MovementSpeed];
            Capacity = (CapacityUpgradeData) Constants.ELEVATOR_UPGRADE_DATA[UpgradeType.Capacity];
        }

        public override object GetUpgradeValue(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.Capacity:
                    return Capacity.GetUpgradeValue(UpgradeLevel);
                case UpgradeType.MovementSpeed:
                    return MovementSpeed.GetUpgradeValue(UpgradeLevel);
                default:
                    Debug.LogError("Elevator doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
