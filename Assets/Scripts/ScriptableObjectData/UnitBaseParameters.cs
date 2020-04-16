using IdleTransport.GameCore.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTransport.ScriptableObjectData {
    [CreateAssetMenu(menuName = "Data/UnitBaseParameters", fileName = "UnitBaseParameters")]
    public class UnitBaseParameters : ScriptableObject {
        private const int SPACE = 25;
        private const string WAREHOUSE = "Warehouse";
        private const string TROLLEY = "Trolley";
        private const string ELEVATOR = "Elevator";
        private const string LOADER = "Loader";
        private const string TRUCK = "Truck";

        [FoldoutGroup(WAREHOUSE)] [Title(WAREHOUSE)] [InlineProperty]
        public UpgradeCost warehouseUpgradeCost;
        [FoldoutGroup(WAREHOUSE)] [Space(SPACE)] [InlineProperty]
        public WorkCycleTimeUpgradeData warehohouseWorkCycleTime;
        [FoldoutGroup(WAREHOUSE)] [Space(SPACE)] [InlineProperty]
        public CargoPerCycleUpgradeData warehohouseCargoPerCycle;
        [FoldoutGroup(WAREHOUSE)] [Space(SPACE)] [InlineProperty]
        public CapacityUpgradeData warehohouseCapacity;

        [FoldoutGroup(TROLLEY)] [Title(TROLLEY)] [InlineProperty]
        public UpgradeCost trolleyUpgradeCost;
        [FoldoutGroup(TROLLEY)] [Space(SPACE)] [InlineProperty]
        public WorkCycleTimeUpgradeData trolleyWorkCycleTime;
        [FoldoutGroup(TROLLEY)] [Space(SPACE)] [InlineProperty]
        public NumberOfUnitsUpgradeData trolleyNumberOfUnits;
        [FoldoutGroup(TROLLEY)] [Space(SPACE)] [InlineProperty]
        public MovementSpeedUpgradeData trolleyMovementSpeed;
        [FoldoutGroup(TROLLEY)] [Space(SPACE)] [InlineProperty]
        public CapacityUpgradeData trolleyCapacity;

        [FoldoutGroup(ELEVATOR)] [Title(ELEVATOR)] [InlineProperty]
        public UpgradeCost elevatorUpgradeCost;
        [FoldoutGroup(ELEVATOR)] [Space(SPACE)] [InlineProperty]
        public MovementSpeedUpgradeData elevatorMovementSpeed;
        [FoldoutGroup(ELEVATOR)] [Space(SPACE)] [InlineProperty]
        public CapacityUpgradeData elevatorCapacity;

        [FoldoutGroup(LOADER)] [Title(LOADER)] [InlineProperty]
        public UpgradeCost loaderUpgradeCost;
        [FoldoutGroup(LOADER)] [Space(SPACE)] [InlineProperty]
        public WorkCycleTimeUpgradeData loaderWorkCycleTime;
        [FoldoutGroup(LOADER)] [Space(SPACE)] [InlineProperty]
        public NumberOfUnitsUpgradeData loaderNumberOfUnits;
        [FoldoutGroup(LOADER)] [Space(SPACE)] [InlineProperty]
        public MovementSpeedUpgradeData loaderMovementSpeed;
        [FoldoutGroup(LOADER)] [Space(SPACE)] [InlineProperty]
        public CapacityUpgradeData loaderCapacity;

        [FoldoutGroup(TRUCK)] [Title(TRUCK)] [InlineProperty]
        public UpgradeCost truckUpgradeCost;
        [FoldoutGroup(TRUCK)] [Space(SPACE)] [InlineProperty]
        public WorkCycleTimeUpgradeData truckWorkCycleTime;
        [FoldoutGroup(TRUCK)] [Space(SPACE)] [InlineProperty]
        public CapacityUpgradeData truckCapacity;
    }
}
