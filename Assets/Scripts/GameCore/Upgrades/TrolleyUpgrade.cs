using IdleTransport.Databases;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TrolleyUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public NumberOfUnitsUpgradeData NumberOfUnits { get; }
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public TrolleyUpgrade() : base(GameResourcesDatabase.GetUnitBaseParameters().trolleyUpgradeCost) {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            WorkCycleTime = unitBaseParameters.trolleyWorkCycleTime;
            NumberOfUnits = unitBaseParameters.trolleyNumberOfUnits;
            MovementSpeed = unitBaseParameters.trolleyMovementSpeed;
            Capacity = unitBaseParameters.trolleyCapacity;
        }

        protected override UpgradeData GetUpgradeData(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime;
                case UpgradeType.NumberOfUnits:
                    return NumberOfUnits;
                case UpgradeType.Capacity:
                    return Capacity;
                case UpgradeType.MovementSpeed:
                    return MovementSpeed;
                default:
                    Debug.LogError("Trolley doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
