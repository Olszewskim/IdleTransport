using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class WarehouseUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CargoPerCycleUpgradeData CargoPerCycle { get; }
        public CapacityUpgradeData Capacity { get; }

        public WarehouseUpgrade(WarehouseUpgradeData warehouseUpgradeData) : base(warehouseUpgradeData.upgradeCost,
            warehouseUpgradeData.maxUpgradeLevel) {
            WorkCycleTime = warehouseUpgradeData.workCycleTime;
            CargoPerCycle = warehouseUpgradeData.cargoPerCycle;
            Capacity = warehouseUpgradeData.capacity;
        }

        protected override UpgradeData GetUpgradeData(UpgradeType upgradeType) {
            switch (upgradeType) {
                case UpgradeType.WorkCycleTime:
                    return WorkCycleTime;
                case UpgradeType.Capacity:
                    return Capacity;
                case UpgradeType.CargoPerCycle:
                    return CargoPerCycle;
                default:
                    Debug.LogError("Warehouse doesn't have upgrade " + upgradeType);
                    return null;
            }
        }

        public override List<UpgradeType> GetUpgradesTypes() {
            return new List<UpgradeType> {
                UpgradeType.WorkCycleTime,
                UpgradeType.CargoPerCycle,
                UpgradeType.Capacity
            };
        }

        public override BigInteger GetTotalProduction(int level) {
            var workCycleValueAtLevel = (double)WorkCycleTime.GetUpgradeValue(level);
            var cargoPerCycleValueAtLevel = (BigInteger)CargoPerCycle.GetUpgradeValue(level);
            return cargoPerCycleValueAtLevel.MultipleByDouble(1 / workCycleValueAtLevel);
        }
    }
}
