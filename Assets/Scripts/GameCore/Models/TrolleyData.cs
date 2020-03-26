using System;
using IdleTransport.GameCore.Models;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace GameCore.Models {
    public class TrolleyData {
        [ShowInInspector, DisplayAsString] public BigInteger Capacity { get; private set; }
        [ShowInInspector] public double CargoLoadingTime { get; private set; }
        [ShowInInspector] public double WalkingSpeed { get; private set; }

        [ShowInInspector, DisplayAsString] public BigInteger CurrentCargoAmount { get; private set; }

        private WarehouseData _warehouseData;
        [ShowInInspector] private TrolleyWorkingState _currentWorkingState;
        [ShowInInspector] private double _currentProductionCycle;

        public TrolleyData(WarehouseData warehouseData) {
            Capacity = Constants.TROLLEY_BASE_CAPACITY;
            CargoLoadingTime = Constants.TROLLEY_CARGO_LOADING_TIME;
            WalkingSpeed = Constants.TROLLEY_WALKING_SPEED;
            _warehouseData = warehouseData;
            CurrentCargoAmount = 0;
            StartLoading();
        }

        private void StartLoading() {
            _currentProductionCycle = 0;
            _currentWorkingState = TrolleyWorkingState.Loading;
        }

        public void UpdateTrolley(float deltaTime) {
            if (IsLoading())
                Load(deltaTime);
        }

        private bool IsLoading() {
            return _currentWorkingState == TrolleyWorkingState.Loading;
        }

        private void Load(float deltaTime) {
            _currentProductionCycle += deltaTime;
            if (IsLoadingFinished()) {
                _currentProductionCycle = 0;
                var availableTrolleyCapacity = Capacity - CurrentCargoAmount;
                var cargoLoadedFromWarehouse = _warehouseData.DistributeCargo(availableTrolleyCapacity);
                if (cargoLoadedFromWarehouse > 0) {
                    CurrentCargoAmount += cargoLoadedFromWarehouse;
                    if (IsFull()) {
                        CurrentCargoAmount = Capacity;
                        StopLoading();
                    }
                }
            }
        }

        private void StopLoading() {
            _currentWorkingState = TrolleyWorkingState.TransportingToElevator;
        }

        private bool IsFull() {
            return CurrentCargoAmount >= Capacity;
        }

        private bool IsLoadingFinished() {
            return _currentProductionCycle >= CargoLoadingTime;
        }
    }
}
