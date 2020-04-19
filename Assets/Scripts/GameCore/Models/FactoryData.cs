using IdleTransport.JSON;
using IdleTransport.Managers;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class FactoryData {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public ElevatorData ElevatorData { get; }
        [ShowInInspector] public LoadingRampsManager LoadingRampsManager { get; }

        public FactoryData() {
            LoadingRampsManager = new LoadingRampsManager();
            WarehouseData = new WarehouseData();
            ElevatorData = new ElevatorData(LoadingRampsManager);
            TrolleyData = new TrolleyData(WarehouseData, ElevatorData);
        }

        public FactoryData(FactoryDataJSON factoryDataJson) {
            LoadingRampsManager = new LoadingRampsManager(factoryDataJson.loadingRampsManagerJSON);
            WarehouseData = new WarehouseData(factoryDataJson.warehouseDataJSON);
            ElevatorData = new ElevatorData(LoadingRampsManager, factoryDataJson.elevatorDataJSON);
            TrolleyData = new TrolleyData(WarehouseData, ElevatorData, factoryDataJson.trolleyDataJSON);
        }
    }
}
