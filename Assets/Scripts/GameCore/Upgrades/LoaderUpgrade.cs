using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class LoaderUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public NumberOfUnitsUpgradeData NumberOfUnits { get; }
        public MovementSpeedUpgradeData MovementSpeed { get; }
        public CapacityUpgradeData Capacity { get; }

        public LoaderUpgrade(LoaderUpgradeData loaderUpgradeData) : base(loaderUpgradeData.upgradeCost,
            loaderUpgradeData.maxUpgradeLevel) {
            WorkCycleTime = loaderUpgradeData.workCycleTime;
            NumberOfUnits = loaderUpgradeData.numberOfUnits;
            MovementSpeed = loaderUpgradeData.movementSpeed;
            Capacity = loaderUpgradeData.capacity;
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

        public override List<UpgradeType> GetUpgradesTypes() {
            return new List<UpgradeType> {
                UpgradeType.WorkCycleTime,
                UpgradeType.NumberOfUnits,
                UpgradeType.MovementSpeed,
                UpgradeType.Capacity
            };
        }

        public override BigInteger GetTotalProduction(int level) {
            var workCycleValueAtLevel = (double) WorkCycleTime.GetUpgradeValue(level);
            var capacityValueAtLevel = (BigInteger) Capacity.GetUpgradeValue(level);
            var movementSpeedAtLevel = (double) MovementSpeed.GetUpgradeValue(level);
            var movementTime = workCycleValueAtLevel + 2 * movementSpeedAtLevel;
            var numberOfUnits = (BigInteger) NumberOfUnits.GetUpgradeValue(level);
            return capacityValueAtLevel.MultipleByDouble(1 / movementTime) * numberOfUnits;
        }
    }
}
