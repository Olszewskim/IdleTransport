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

        public override object GetUpgradeValue(UpgradeType upgradeType, int level) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime.GetUpgradeValue(level);
                case UpgradeType.NumberOfUnits:
                    return NumberOfUnits.GetUpgradeValue(level);
                case UpgradeType.Capacity:
                    return Capacity.GetUpgradeValue(level);
                case UpgradeType.MovementSpeed:
                    return MovementSpeed.GetUpgradeValue(level);
                default:
                    Debug.LogError("Trolley doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
