using IdleTransport.GameCore.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTransport.ScriptableObjectData {
    [CreateAssetMenu(menuName = "Data/UnitBaseParameters", fileName = "UnitBaseParameters")]
    public class UnitBaseParameters : ScriptableObject {
        [Header("Warehouse")]
        [InlineProperty] public UpgradeCost warehouseUpgradeCost;

        [Header("Trolley")]
        [InlineProperty] public UpgradeCost trolleyUpgradeCost;

        [Header("Elevator")]
        [InlineProperty] public UpgradeCost elevatorUpgradeCost;

        [Header("Loader")]
        [InlineProperty] public UpgradeCost loaderUpgradeCost;

        [Header("Truck")]
        [InlineProperty] public UpgradeCost truckUpgradeCost;
    }
}
