using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class TrolleyData : TransportingUnitData {
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

        public TrolleyData(WarehouseData warehouseData, ElevatorData elevatorData) : base(
            Constants.TROLLEY_BASE_CAPACITY,
            Constants.TROLLEY_BASE_WORK_CYCLE_TIME, Constants.TROLLEY_BASE_WALKING_SPEED) {
            _warehouseData = warehouseData;
            _elevatorData = elevatorData;
            StartWorking();
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = TrolleyWorkingState.Working;
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
            StartTransporting();
        }

        protected override bool IsTransporting() {
            return CurrentWorkingState == TrolleyWorkingState.TransportingToElevator;
        }

        protected override void SetTransportingState() {
            CurrentWorkingState = TrolleyWorkingState.TransportingToElevator;
        }

        protected override void FinishTransporting() {
            LoadElevator();
        }

        private void LoadElevator() {
            CurrentWorkingState = TrolleyWorkingState.LoadingElevator;
            _elevatorData.LoadCargo(CurrentCargoAmount, out var loadedCargo);
            if (loadedCargo > 0) {
                CurrentCargoAmount -= loadedCargo;
                StartReturning();
            }
        }

        protected override bool IsReturning() {
            return CurrentWorkingState == TrolleyWorkingState.ReturningToWarehouse;
        }

        protected override void SetReturningState() {
            CurrentWorkingState = TrolleyWorkingState.ReturningToWarehouse;
        }

        protected override void FinishReturning() {
            StartWorking();
        }
    }
}
