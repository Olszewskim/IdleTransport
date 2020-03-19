using System;
using IdleTransport.Utilities;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    [Serializable]
    public class WarehouseData {
        public Action<BuildingWorkingState> OnBuildingWorkingStateChanged;
        public Action<BigInteger, BigInteger> OnProductionCompleted;
        public Action<double> OnProgressUpdated;

        public BigInteger Capacity { get; private set; }
        public double CargoPackageProducingSpeed { get; private set; }
        public BigInteger TotalCargoAmountInPackage { get; private set; }

        public BigInteger AverageCargoProducedPerSecond { get; private set; }

        public BigInteger CurrentCargoAmount { get; private set; }

        private BuildingWorkingState _currentWorkingState;

        public BuildingWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            private set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnBuildingWorkingStateChanged?.Invoke(_currentWorkingState);
                }
            }
        }

        public double CurrentProductionProgress => _currentProductionCycle / CargoPackageProducingSpeed;
        private double _currentProductionCycle;

        public WarehouseData() {
            Capacity = Constants.WAREHOUSE_BASE_CAPACITY;
            CargoPackageProducingSpeed = Constants.WAREHOUSE_BASE_CARGO_PACKAGE_PRODUCING_SPEED;
            TotalCargoAmountInPackage = Constants.WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE;
            CurrentCargoAmount = 0;
            _currentProductionCycle = 0;
            StartProduction();
        }

        private void StartProduction() {
            _currentProductionCycle = 0;
            CurrentWorkingState = BuildingWorkingState.Working;
        }

        public void UpdateWarehouse(float deltaTime) {
            if (IsWorking()) {
                _currentProductionCycle += deltaTime;
                OnProgressUpdated?.Invoke(CurrentProductionProgress);
                if (_currentProductionCycle >= CargoPackageProducingSpeed) {
                    FinishCargoPackageProduction();
                }
            }
        }

        private bool IsWorking() {
            return CurrentWorkingState == BuildingWorkingState.Working;
        }

        private void FinishCargoPackageProduction() {
            _currentProductionCycle = 0;
            CurrentCargoAmount += TotalCargoAmountInPackage;
            if (IsFull()) {
                CurrentCargoAmount = Capacity;
                StopProduction();
            }
            OnProductionCompleted?.Invoke(CurrentCargoAmount, Capacity);
        }

        private bool IsFull() {
            return CurrentCargoAmount >= Capacity;
        }

        private void StopProduction() {
            CurrentWorkingState = BuildingWorkingState.Full;
        }
    }
}
