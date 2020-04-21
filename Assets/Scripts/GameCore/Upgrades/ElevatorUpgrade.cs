using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class ElevatorUpgrade : UnitUpgrade {
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public ElevatorUpgrade(ElevatorUpgradeData elevatorUpgradeData) : base(elevatorUpgradeData.upgradeCost) {
            MovementSpeed = elevatorUpgradeData.movementSpeed;
            Capacity = elevatorUpgradeData.capacity;
        }

        protected override UpgradeData GetUpgradeData(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.Capacity:
                    return Capacity;
                case UpgradeType.MovementSpeed:
                    return MovementSpeed;
                default:
                    Debug.LogError("Elevator doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
