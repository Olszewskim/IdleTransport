using IdleTransport.Databases;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class LoaderUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public NumberOfUnitsUpgradeData NumberOfUnits { get; }
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public LoaderUpgrade() : base(GameResourcesDatabase.GetUnitBaseParameters().loaderUpgradeCost) {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            WorkCycleTime = unitBaseParameters.loaderWorkCycleTime;
            NumberOfUnits = unitBaseParameters.loaderNumberOfUnits;
            MovementSpeed = unitBaseParameters.loaderMovementSpeed;
            Capacity = unitBaseParameters.loaderCapacity;
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
                    Debug.LogError("Loader doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
