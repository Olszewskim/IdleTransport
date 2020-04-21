using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTransport.GameCore.Upgrades {
    [Serializable]
    public class UnitUpgradeData {
        protected const int SPACE = 25;

        public int maxLevel;
        [InlineProperty] public UpgradeCost upgradeCost;
        [InlineProperty] [Space(SPACE)][PropertyOrder(5)] public CapacityUpgradeData capacity;
    }

    [Serializable]
    public class WarehouseUpgradeData : UnitUpgradeData {
        [InlineProperty] [Space(SPACE)] public WorkCycleTimeUpgradeData workCycleTime;
        [InlineProperty] [Space(SPACE)] public CargoPerCycleUpgradeData cargoPerCycle;
    }

    [Serializable]
    public class TrolleyUpgradeData : UnitUpgradeData {
        [InlineProperty] [Space(SPACE)] public WorkCycleTimeUpgradeData workCycleTime;
        [InlineProperty] [Space(SPACE)] public NumberOfUnitsUpgradeData numberOfUnits;
        [InlineProperty] [Space(SPACE)] public MovementSpeedUpgradeData movementSpeed;
    }

    [Serializable]
    public class ElevatorUpgradeData : UnitUpgradeData {
        [InlineProperty] [Space(SPACE)] public MovementSpeedUpgradeData movementSpeed;
    }

    [Serializable]
    public class LoaderUpgradeData : UnitUpgradeData {
        [InlineProperty] [Space(SPACE)] public WorkCycleTimeUpgradeData workCycleTime;
        [InlineProperty] [Space(SPACE)] public NumberOfUnitsUpgradeData numberOfUnits;
        [InlineProperty] [Space(SPACE)] public MovementSpeedUpgradeData movementSpeed;
    }

    [Serializable]
    public class TruckUpgradeData : UnitUpgradeData {
        [InlineProperty] [Space(SPACE)] public WorkCycleTimeUpgradeData workCycleTime;
    }
}
