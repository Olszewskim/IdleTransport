using IdleTransport.Databases;
using IdleTransport.JSON;
using IdleTransport.Managers;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class FactoryData {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public ElevatorData ElevatorData { get; }
        [ShowInInspector] public LoadingRampsManager LoadingRampsManager { get; }

        public FactoryData() {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            LoadingRampsManager = new LoadingRampsManager(unitBaseParameters);
            WarehouseData = new WarehouseData(unitBaseParameters.warehouseUpgradeData);
            ElevatorData = new ElevatorData(unitBaseParameters.elevatorUpgradeData, LoadingRampsManager);
            TrolleyData = new TrolleyData(unitBaseParameters.trolleyUpgradeData, WarehouseData, ElevatorData);
        }

        public FactoryData(FactoryDataJSON factoryDataJson) {
            var unitBaseParameters = GameResourcesDatabase.GetUnitBaseParameters();
            LoadingRampsManager = new LoadingRampsManager(unitBaseParameters, factoryDataJson.loadingRampsManagerJSON);
            WarehouseData =
                new WarehouseData(unitBaseParameters.warehouseUpgradeData, factoryDataJson.warehouseDataJSON);
            ElevatorData = new ElevatorData(unitBaseParameters.elevatorUpgradeData, LoadingRampsManager,
                factoryDataJson.elevatorDataJSON);
            TrolleyData = new TrolleyData(unitBaseParameters.trolleyUpgradeData, WarehouseData, ElevatorData,
                factoryDataJson.trolleyDataJSON);
        }
    }
}
