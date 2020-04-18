using IdleTransport.Databases;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class WarehouseUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CargoPerCycleUpgradeData CargoPerCycle { get; }
        public CapacityUpgradeData Capacity { get; }

        public WarehouseUpgrade() : base(GameResourcesDatabase.GetUnitBaseParameters().warehouseUpgradeCost) {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            WorkCycleTime = unitBaseParameters.warehohouseWorkCycleTime;
            CargoPerCycle = unitBaseParameters.warehohouseCargoPerCycle;
            Capacity = unitBaseParameters.warehohouseCapacity;
        }

        public override object GetUpgradeValue(UpgradeType upgradeType, int level) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime.GetUpgradeValue(level);
                case UpgradeType.Capacity:
                    return Capacity.GetUpgradeValue(level);
                case UpgradeType.CargoPerCycle:
                    return CargoPerCycle.GetUpgradeValue(level);
                default:
                    Debug.LogError("Warehouse doesn't have upgrade " + upgradeType);
                    return null;
            }
        }
    }
}
