using System;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class WarehouseData {
        public event Action<BuildingWorkingState> OnBuildingWorkingStateChanged;
        public event Action<BigInteger, BigInteger> OnProductionCompleted;
        public event Action<double> OnProgressUpdated;

        [ShowInInspector, DisplayAsString] public BigInteger Capacity { get; private set; }
        [ShowInInspector] public double CargoPackageProducingSpeed { get; private set; }
        [ShowInInspector, DisplayAsString] public BigInteger TotalCargoAmountInPackage { get; private set; }

        public BigInteger AverageCargoProducedPerSecond { get; private set; }

        [ShowInInspector, DisplayAsString] public BigInteger CurrentCargoAmount { get; private set; }

        [ShowInInspector] private BuildingWorkingState _currentWorkingState;

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
        [ShowInInspector] private double _currentProductionCycle;

        public WarehouseData() {
            Capacity = Constants.WAREHOUSE_BASE_CAPACITY;
            CargoPackageProducingSpeed = Constants.WAREHOUSE_BASE_CARGO_PACKAGE_PRODUCING_SPEED;
            TotalCargoAmountInPackage = Constants.WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE;
            CurrentCargoAmount = 0;
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

        public BigInteger DistributeCargo(BigInteger availableTrolleyCapacity) {
            var cargoToDistribute = BigInteger.Min(CurrentCargoAmount, availableTrolleyCapacity);
            CurrentCargoAmount -= cargoToDistribute;
            OnProductionCompleted?.Invoke(CurrentCargoAmount, Capacity);
            if (IsProductionStopped()) {
                StartProduction();
            }

            return cargoToDistribute;
        }

        private bool IsProductionStopped() {
            return CurrentWorkingState == BuildingWorkingState.Full;
        }
    }
}
