using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
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
            Constants.TROLLEY_BASE_WORK_CYCLE_TIME, Constants.TROLLEY_BASE_WALKING_SPEED, UnitType.Trolley) {
            _warehouseData = warehouseData;
            _elevatorData = elevatorData;
            _elevatorData.OnSwitchedToWaitingState += TryToLoadElevator;
            StartWorking();
        }

        protected override void StartWaiting() {
            CurrentWorkingState = TrolleyWorkingState.Waiting;
        }

        public override bool IsWaiting() {
            return CurrentWorkingState == TrolleyWorkingState.Waiting;
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = TrolleyWorkingState.Working;
        }

        public override bool IsWorking() {
            return CurrentWorkingState == TrolleyWorkingState.Working;
        }

        protected override void FinishWorking() {
            base.FinishWorking();
            _warehouseData.DistributeCargo(AvailableCapacity, out var cargoLoadedFromWarehouse);
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

        private void TryToLoadElevator() {
            if (IsLoadingElevator()) {
                LoadElevator();
            }
        }

        private void LoadElevator() {
            CurrentWorkingState = TrolleyWorkingState.LoadingElevator;
            if (!_elevatorData.IsWaiting()) {
                return;
            }

            _elevatorData.LoadCargo(CurrentCargoAmount, out var loadedCargo);
            if (loadedCargo > 0) {
                CurrentCargoAmount -= loadedCargo;
                StartReturning();
            }
        }

        private bool IsLoadingElevator() {
            return CurrentWorkingState == TrolleyWorkingState.LoadingElevator;
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

        public override List<StatInfo> GetUnitStats() {
            return new List<StatInfo> {
                new StatInfo(StatType.TrolleyTotalTransportationPerSecond, "0", "0"),
                new StatInfo(StatType.TrolleyLoadingSpeed, WorkCycleTime.ToSecondsWithTwoDecimalPlaces(), "0"),
                new StatInfo(StatType.TrolleyAmount, "1", "0"),
                new StatInfo(StatType.TrolleyWalkingSpeed, WalkingSpeed.ToTimePerSecond(), "0"),
                new StatInfo(StatType.TrolleyCapacity, Capacity.FormatHugeNumber(), "0")
            };
        }
    }
}
