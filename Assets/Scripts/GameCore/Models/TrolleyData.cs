using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class TrolleyData : WorkingUnitData {
        [ShowInInspector] public double WalkingSpeed { get; private set; }

        private readonly WarehouseData _warehouseData;

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

        public TrolleyData(WarehouseData warehouseData) : base(Constants.TROLLEY_BASE_CAPACITY,
            Constants.TROLLEY_BASE_WORK_CYCLE_TIME) {
            WalkingSpeed = Constants.TROLLEY_BASE_WALKING_SPEED;
            _warehouseData = warehouseData;
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
            var availableTrolleyCapacity = Capacity - CurrentCargoAmount;
            var cargoLoadedFromWarehouse = _warehouseData.DistributeCargo(availableTrolleyCapacity);
            if (cargoLoadedFromWarehouse > 0) {
                CurrentCargoAmount += cargoLoadedFromWarehouse;
                CheckIfCapacityIsFull();
            }
        }

        protected override void StopWork() {
            CurrentWorkingState = TrolleyWorkingState.TransportingToElevator;
        }
    }
}
