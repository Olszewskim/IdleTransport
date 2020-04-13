using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class TruckData : TransportingUnitData {
        [ShowInInspector] private TruckWorkingState _currentWorkingState;

        private TruckWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnWorkingStateChanged();
                }
            }
        }

        public TruckData()
            : base(Constants.TRUCK_BASE_CAPACITY, Constants.TRUCK_BASE_WORK_CYCLE_TIME,
                Constants.TRUCK_BASE_WALKING_SPEED, UnitType.Truck, new TruckUpgrade()) {
        }

        protected override void StartWaiting() {
            CurrentWorkingState = TruckWorkingState.Waiting;
            base.StartWaiting();
        }

        public override bool IsWaiting() {
            return CurrentWorkingState == TruckWorkingState.Waiting;
        }

        public override bool IsWorking() {
            return CurrentWorkingState == TruckWorkingState.Working;
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = TruckWorkingState.Working;
        }

        protected override void FinishWorking() {
            base.FinishWorking();
            StopWork();
        }

        protected override void StopWork() {
            PlayerManager.Instance.AddCurrency(CurrencyType.Gold, CurrentCargoAmount);
            CurrentCargoAmount = 0;
            StartReturning();
        }

        protected override bool IsTransporting() {
            return CurrentWorkingState == TruckWorkingState.TransportingToMarket;
        }

        protected override void SetTransportingState() {
            CurrentWorkingState = TruckWorkingState.TransportingToMarket;
        }

        protected override void FinishTransporting() {
            StartWorking();
        }

        protected override bool IsReturning() {
            return CurrentWorkingState == TruckWorkingState.ReturningToGate;
        }

        protected override void SetReturningState() {
            CurrentWorkingState = TruckWorkingState.ReturningToGate;
        }

        protected override void FinishReturning() {
            StartWaiting();
        }

        public override void LoadCargo(BigInteger loaderCargoAmount, out BigInteger unloadedCargo) {
            unloadedCargo = 0;
            if (!IsWaiting()) {
                return;
            }

            base.LoadCargo(loaderCargoAmount, out unloadedCargo);
            if (IsFull()) {
                StartTransporting();
            }
        }

        public override List<StatInfo> GetUnitStats() {
            return new List<StatInfo> {
                new StatInfo(StatType.TruckTotalIncomePerSecond, "0", "0"),
                new StatInfo(StatType.TruckSellSpeed, WorkCycleTime.ToSecondsWithTwoDecimalPlaces(), "0"),
                new StatInfo(StatType.TruckCapacity, Capacity.FormatHugeNumber(), "0")
            };
        }
    }
}
