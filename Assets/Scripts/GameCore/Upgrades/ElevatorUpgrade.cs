using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class ElevatorUpgrade : UnitUpgrade {
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        private LoadingRampsManager _loadingRampsManager;

        public ElevatorUpgrade(ElevatorUpgradeData elevatorUpgradeData, LoadingRampsManager loadingRampsManager) : base(
            elevatorUpgradeData.upgradeCost,
            elevatorUpgradeData.maxUpgradeLevel) {
            MovementSpeed = elevatorUpgradeData.movementSpeed;
            Capacity = elevatorUpgradeData.capacity;
            _loadingRampsManager = loadingRampsManager;
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

        public override List<UpgradeType> GetUpgradesTypes() {
            return new List<UpgradeType> {
                UpgradeType.MovementSpeed,
                UpgradeType.Capacity
            };
        }

        public override BigInteger GetTotalProduction(int level) {
            var capacityValueAtLevel = (BigInteger) Capacity.GetUpgradeValue(level);
            var movementSpeedAtLevel = (double) MovementSpeed.GetUpgradeValue(level);
            var movementTime = movementSpeedAtLevel * _loadingRampsManager.LoadingRampsCount;
            return capacityValueAtLevel.MultipleByDouble(1 / movementTime);
        }
    }
}
