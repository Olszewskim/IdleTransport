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

        public override object GetUpgradeValue(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime.GetUpgradeValue(UpgradeLevel);
                case UpgradeType.NumberOfUnits:
                    return NumberOfUnits.GetUpgradeValue(UpgradeLevel);
                case UpgradeType.Capacity:
                    return Capacity.GetUpgradeValue(UpgradeLevel);
                case UpgradeType.MovementSpeed:
                    return MovementSpeed.GetUpgradeValue(UpgradeLevel);
                default:
                    Debug.LogError("Trolley doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
