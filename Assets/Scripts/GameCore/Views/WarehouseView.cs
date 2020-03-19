using IdleTransport.GameCore.Models;
using IdleTransport.Managers;
using UnityEngine;

namespace IdleTransport.GameCore.Views
{
    public class WarehouseView : MonoBehaviour {
        private WarehouseData _warehouseData;

        private void Start() {
            _warehouseData = PlayerManager.Instance.Player.WarehouseData;
        }

        private void Update() {
            _warehouseData.UpdateWarehouse(Time.deltaTime);
        }
    }
}
