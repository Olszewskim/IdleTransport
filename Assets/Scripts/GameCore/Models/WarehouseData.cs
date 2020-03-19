using System;
using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Models
{
    [Serializable]
    public class WarehouseData {
        public BigInteger Capacity { get; private set; }
        public double CargoPackageProducingSpeed { get; private set; }
        public BigInteger TotalCargoAmountInPackage { get; private set; }

        public BigInteger AverageCargoProducedPerSecond { get; private set; }

        public BigInteger CurrentCargoAmount { get; private set; }

        private Enums.BuildingWorkingState _currentWorkingState;
        private double _currentProductionCycle;

        public WarehouseData() {
            Capacity = Constants.WAREHOUSE_BASE_CAPACITY;
            CargoPackageProducingSpeed = Constants.WAREHOUSE_BASE_CARGO_PACKAGE_PRODUCING_SPEED;
            TotalCargoAmountInPackage = Constants.WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE;
            _currentProductionCycle = 0;
            StartProduction();
        }

        private void StartProduction() {
            _currentProductionCycle = 0;
            _currentWorkingState = Enums.BuildingWorkingState.Working;
        }

        public void UpdateWarehouse(float deltaTime) {
            if (IsWorking()) {
                _currentProductionCycle += deltaTime;
                if (_currentProductionCycle >= CargoPackageProducingSpeed) {
                    FinishCargoPackageProduction();
                }
            }
        }

        private bool IsWorking() {
            return _currentWorkingState == Enums.BuildingWorkingState.Working;
        }

        private void FinishCargoPackageProduction() {
            _currentProductionCycle = 0;
            CurrentCargoAmount += TotalCargoAmountInPackage;
            if (IsFull()) {
                CurrentCargoAmount = Capacity;
                StopProduction();
            }
        }

        private bool IsFull() {
            return CurrentCargoAmount >= Capacity;
        }

        private void StopProduction() {
            _currentWorkingState = Enums.BuildingWorkingState.Full;
        }
    }
}
