using IdleTransport.Databases;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TruckUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CapacityUpgradeData Capacity { get; }

        public TruckUpgrade() : base(GameResourcesDatabase.GetUnitBaseParameters().truckUpgradeCost) {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            WorkCycleTime = unitBaseParameters.truckWorkCycleTime;
            Capacity = unitBaseParameters.truckCapacity;
        }

        public override object GetUpgradeValue(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime.GetUpgradeValue(UpgradeLevel);
                case UpgradeType.Capacity:
                    return Capacity.GetUpgradeValue(UpgradeLevel);
                default:
                    Debug.LogError("Truck doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
