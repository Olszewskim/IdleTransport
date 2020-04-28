using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public class TruckUpgrade : UnitUpgrade {
        public WorkCycleTimeUpgradeData WorkCycleTime { get; }
        public CapacityUpgradeData Capacity { get; }

        private double _walkingSpeed;

        public TruckUpgrade(TruckUpgradeData truckUpgradeData) : base(truckUpgradeData.upgradeCost,
            truckUpgradeData.maxUpgradeLevel) {
            WorkCycleTime = truckUpgradeData.workCycleTime;
            Capacity = truckUpgradeData.capacity;
            _walkingSpeed = Constants.TRUCK_BASE_WALKING_SPEED;
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

        public override List<UpgradeType> GetUpgradesTypes() {
            return new List<UpgradeType> {
                UpgradeType.WorkCycleTime,
                UpgradeType.Capacity
            };
        }

        public override BigInteger GetTotalProduction(int level) {
            var workCycleValueAtLevel = (double) WorkCycleTime.GetUpgradeValue(level);
            var capacityValueAtLevel = (BigInteger) Capacity.GetUpgradeValue(level);
            var movementTime = workCycleValueAtLevel + 2 * _walkingSpeed;
            return capacityValueAtLevel.MultipleByDouble(1 / movementTime);
        }
    }
}
