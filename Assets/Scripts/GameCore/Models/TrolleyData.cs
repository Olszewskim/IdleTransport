using System;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class TrolleyData : WorkingUnitData {
        public event Action OnTrolleyStartTransportingToElevator;
        public event Action OnTrolleyStartReturningToWarehouse;
        [ShowInInspector] public double WalkingSpeed { get; private set; }

        private readonly WarehouseData _warehouseData;
        private readonly ElevatorData _elevatorData;

        [ShowInInspector] private TrolleyWorkingState _currentWorkingState;

        private TrolleyWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnWorkingStateChanged();
                }
            }
        }

        private double _currentWalkingTime;

        public TrolleyData(WarehouseData warehouseData, ElevatorData elevatorData) : base(
            Constants.TROLLEY_BASE_CAPACITY,
            Constants.TROLLEY_BASE_WORK_CYCLE_TIME) {
            WalkingSpeed = Constants.TROLLEY_BASE_WALKING_SPEED;
            _warehouseData = warehouseData;
            _elevatorData = elevatorData;
            StartWorking();
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = TrolleyWorkingState.Working;
        }

        public override void UpdateUnit(float deltaTime) {
            base.UpdateUnit(deltaTime);

            if (IsTransportingToElevator()) {
                TransportToElevator(deltaTime);
            }

            if (IsReturningToElevator()) {
                ReturningToElevator(deltaTime);
            }
        }

        public override bool IsWorking() {
            return CurrentWorkingState == TrolleyWorkingState.Working;
        }

        protected override void FinishWorking() {
            base.FinishWorking();
            var cargoLoadedFromWarehouse = _warehouseData.DistributeCargo(AvailableCapacity);
            if (cargoLoadedFromWarehouse > 0) {
                CurrentCargoAmount += cargoLoadedFromWarehouse;
                StopWork();
            }
        }

        protected override void StopWork() {
            StartTransportingToElevator();
        }

        private void StartTransportingToElevator() {
            CurrentWorkingState = TrolleyWorkingState.TransportingToElevator;
            _currentWalkingTime = 0;
            OnTrolleyStartTransportingToElevator?.Invoke();
        }

        private bool IsTransportingToElevator() {
            return CurrentWorkingState == TrolleyWorkingState.TransportingToElevator;
        }

        private void TransportToElevator(float deltaTime) {
            _currentWalkingTime += deltaTime;
            //TODO: Adjust Walking Speed ​after upgrade on the ​next cycle
            if (_currentWalkingTime >= WalkingSpeed) {
                LoadElevator();
            }
        }

        private void LoadElevator() {
            CurrentWorkingState = TrolleyWorkingState.LoadingElevator;
            _elevatorData.LoadCargo(CurrentCargoAmount, out var loadedCargo);
            if (loadedCargo > 0) {
                CurrentCargoAmount -= loadedCargo;
                StartReturningToWarehouse();
            }
        }

        private void StartReturningToWarehouse() {
            CurrentWorkingState = TrolleyWorkingState.ReturningToWarehouse;
            _currentWalkingTime = 0;
            OnTrolleyStartReturningToWarehouse?.Invoke();
        }

        private bool IsReturningToElevator() {
            return CurrentWorkingState == TrolleyWorkingState.ReturningToWarehouse;
        }

        private void ReturningToElevator(float deltaTime) {
            _currentWalkingTime += deltaTime;
            if (_currentWalkingTime >= WalkingSpeed) {
                StartWorking();
            }
        }
    }
}
