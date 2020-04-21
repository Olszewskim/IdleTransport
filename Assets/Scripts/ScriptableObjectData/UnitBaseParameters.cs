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

        [FoldoutGroup(WAREHOUSE)] [Title(WAREHOUSE)] [InlineProperty] [HideLabel]
        public WarehouseUpgradeData warehouseUpgradeData;
        [FoldoutGroup(TROLLEY)] [Title(TROLLEY)] [InlineProperty] [HideLabel]
        public TrolleyUpgradeData trolleyUpgradeData;
        [FoldoutGroup(ELEVATOR)] [Title(ELEVATOR)] [InlineProperty] [HideLabel]
        public ElevatorUpgradeData elevatorUpgradeData;
        [FoldoutGroup(LOADER)] [Title(LOADER)] [InlineProperty] [HideLabel]
        public LoaderUpgradeData loaderUpgradeData;
        [FoldoutGroup(TRUCK)] [Title(TRUCK)] [InlineProperty] [HideLabel]
        public TruckUpgradeData truckUpgradeData;
    }
}
