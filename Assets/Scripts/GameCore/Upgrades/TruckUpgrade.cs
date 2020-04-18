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

        protected override UpgradeData GetUpgradeData(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime;
                case UpgradeType.Capacity:
                    return Capacity;
                default:
                    Debug.LogError("Truck doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
