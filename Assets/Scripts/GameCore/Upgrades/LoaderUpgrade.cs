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
                    Debug.LogError("Loader doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
