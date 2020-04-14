using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class LoaderData : TransportingUnitData {
        [ShowInInspector] private LoaderWorkingState _currentWorkingState;
        private TruckData _truckData;

        private LoaderWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnWorkingStateChanged();
                }
            }
        }

        public LoaderData(TruckData truckData)
            : base(Constants.LOADER_BASE_WALKING_SPEED, UnitType.Loader, new LoaderUpgrade()) {
            _truckData = truckData;
            _truckData.OnSwitchedToWaitingState += TryToLoadTruck;
            StartWaiting();
        }

        protected override void StartWaiting() {
            _currentWorkingState = LoaderWorkingState.Waiting;
        }

        public override bool IsWaiting() {
            return CurrentWorkingState == LoaderWorkingState.Waiting;
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = LoaderWorkingState.Working;
        }

        public override bool IsWorking() {
            return CurrentWorkingState == LoaderWorkingState.Working;
        }

        protected override void FinishWorking() {
            base.FinishWorking();
            _truckData.LoadCargo(CurrentCargoAmount, out var unloadedCargo);
            if (unloadedCargo > 0) {
                CurrentCargoAmount -= unloadedCargo;
                StopWork();
            } else {
                CurrentWorkingState = LoaderWorkingState.WaitingForTruck;
            }
        }

        protected override void StopWork() {
            StartReturning();
        }

        private void TryToLoadTruck() {
            if (IsWaitingForTruck()) {
                StartWorking();
            }
        }

        private bool IsWaitingForTruck() {
            return CurrentWorkingState == LoaderWorkingState.WaitingForTruck;
        }

        protected override bool IsTransporting() {
            return CurrentWorkingState == LoaderWorkingState.TransportingToTruck;
        }

        protected override void SetTransportingState() {
            CurrentWorkingState = LoaderWorkingState.TransportingToTruck;
        }

        protected override void FinishTransporting() {
            if (!_truckData.IsWaiting()) {
                if (!IsFull()) {
                    StartReturning();
                    return;
                }

                CurrentWorkingState = LoaderWorkingState.WaitingForTruck;
                return;
            }

            StartWorking();
        }

        protected override bool IsReturning() {
            return CurrentWorkingState == LoaderWorkingState.ReturningToElevator;
        }

        protected override void SetReturningState() {
            CurrentWorkingState = LoaderWorkingState.ReturningToElevator;
        }

        protected override void FinishReturning() {
            StartWaiting();
        }

        public override void LoadCargo(BigInteger elevatorCargoAmount, out BigInteger unloadedCargo) {
            unloadedCargo = 0;
            if (!IsWaiting()) {
                return;
            }

            base.LoadCargo(elevatorCargoAmount, out unloadedCargo);
            StartTransporting();
        }

        public override List<StatInfo> GetUnitStats() {
            return new List<StatInfo> {
                new StatInfo(StatType.LoaderTotalTransportationPerSecond, "0", "0"),
                new StatInfo(StatType.LoaderLoadingSpeed, WorkCycleTime.ToSecondsWithTwoDecimalPlaces(), "0"),
                new StatInfo(StatType.LoaderAmount, "1", "0"),
                new StatInfo(StatType.LoaderWalkingSpeed, WalkingSpeed.ToTimePerSecond(), "0"),
                new StatInfo(StatType.LoaderCapacity, Capacity.FormatHugeNumber(), "0")
            };
        }
    }
}
