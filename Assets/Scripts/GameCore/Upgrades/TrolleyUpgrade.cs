using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TrolleyUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public NumberOfUnitsUpgradeData NumberOfUnits { get; }
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public TrolleyUpgrade() : base(Constants.UNIT_UPGRADE_COST[UnitType.Trolley]) {
            WorkCycleTime = (WorkCycleTimeUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.WorkCycleTime];
            NumberOfUnits = (NumberOfUnitsUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.NumberOfUnits];
            MovementSpeed = (MovementSpeedUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.MovementSpeed];
            Capacity = (CapacityUpgradeData) Constants.TROLLEY_UPGRADE_DATA[UpgradeType.Capacity];
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
