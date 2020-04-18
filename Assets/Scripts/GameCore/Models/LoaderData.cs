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
            : base(UnitType.Loader, new LoaderUpgrade()) {
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
            base.FinishTransporting();
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
            base.FinishReturning();
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

        public override List<StatInfo> GetUnitStats(int levelsToUpgrade) {
            var levelAfterUpgrade = UnitUpgrade.UpgradeLevel + levelsToUpgrade;
            return new List<StatInfo> {
                new StatInfo(StatType.TrolleyTotalTransportationPerSecond, GetTotalProductionDesc(),
                    GetTotalProductionAfterUpgradeBonus(levelAfterUpgrade)),

                new StatInfo(StatType.TrolleyLoadingSpeed, GetUpgradeValueDesc(UpgradeType.WorkCycleTime),
                    GetAfterUpgradeBonus(UpgradeType.WorkCycleTime, levelAfterUpgrade)),

                new StatInfo(StatType.TrolleyAmount, GetUpgradeValueDesc(UpgradeType.NumberOfUnits),
                    GetAfterUpgradeBonus(UpgradeType.NumberOfUnits, levelAfterUpgrade)),

                new StatInfo(StatType.TrolleyWalkingSpeed, GetUpgradeValueDesc(UpgradeType.MovementSpeed),
                    GetAfterUpgradeBonus(UpgradeType.MovementSpeed, levelAfterUpgrade)),

                new StatInfo(StatType.TrolleyCapacity, GetUpgradeValueDesc(UpgradeType.Capacity),
                    GetAfterUpgradeBonus(UpgradeType.Capacity, levelAfterUpgrade))
            };
        }

        protected override BigInteger GetTotalProduction(int level) {
            var workCycleValueAtLevel = GetUpgradeValue<double>(UpgradeType.WorkCycleTime, level);
            var capacityValueAtLevel = GetUpgradeValue<BigInteger>(UpgradeType.Capacity, level);
            var movementSpeedAtLevel = GetUpgradeValue<double>(UpgradeType.MovementSpeed, level);
            var movementTime = workCycleValueAtLevel + 2 * movementSpeedAtLevel;
            return capacityValueAtLevel.MultipleByDouble(1 / movementTime);
        }
    }
}
