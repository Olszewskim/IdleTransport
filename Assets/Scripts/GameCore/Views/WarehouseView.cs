using IdleTransport.GameCore.Models;
using IdleTransport.Managers;
using IdleTransport.UI.Buildings;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public class WarehouseView : MonoBehaviour {
        [SerializeField] private BuildingProductionUI _buildingProductionUI;
        private WarehouseData _warehouseData;

        private void Start() {
            _warehouseData = PlayerManager.Instance.Player.WarehouseData;
            _buildingProductionUI.Init(_warehouseData);
        }

        private void Update() {
            _warehouseData.UpdateWarehouse(Time.deltaTime);
        }
    }
}
