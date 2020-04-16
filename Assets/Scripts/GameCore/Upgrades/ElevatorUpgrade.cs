using IdleTransport.Databases;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class ElevatorUpgrade : UnitUpgrade {
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public ElevatorUpgrade() : base(GameResourcesDatabase.GetUnitBaseParameters().elevatorUpgradeCost) {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            MovementSpeed = unitBaseParameters.elevatorMovementSpeed;
            Capacity = unitBaseParameters.elevatorCapacity;
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
