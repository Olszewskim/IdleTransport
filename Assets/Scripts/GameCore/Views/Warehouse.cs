using UnityEngine;

public class Warehouse : MonoBehaviour {
    private WarehouseData _warehouseData;

    private void Start() {
        _warehouseData = PlayerManager.Instance.Player.WarehouseData;
    }

    private void Update() {
        _warehouseData.UpdateWarehouse(Time.deltaTime);
    }
}
