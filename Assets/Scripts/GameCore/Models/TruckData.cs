using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.JSON;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class TruckData : TransportingUnitData {
        [ShowInInspector] private TruckWorkingState _currentWorkingState;
        [ShowInInspector] public override double WalkingSpeed => Constants.TRUCK_BASE_WALKING_SPEED;

        private TruckWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnWorkingStateChanged();
                }
            }
        }

        public TruckData(TruckUpgradeData truckUpgradeData)
            : base(UnitType.Truck, new TruckUpgrade(truckUpgradeData)) {
        }

        public TruckData(TruckUpgradeData truckUpgradeData, UnitDataJSON truckDataJson)
            : base(UnitType.Truck, new TruckUpgrade(truckUpgradeData), truckDataJson) {
            if (!IsFull()) {
                StartWaiting();
            } else {
                StartTransporting();
            }
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
            base.FinishTransporting();
            StartWorking();
        }

        protected override bool IsReturning() {
            return CurrentWorkingState == TruckWorkingState.ReturningToGate;
        }

        protected override void SetReturningState() {
            CurrentWorkingState = TruckWorkingState.ReturningToGate;
        }

        protected override void FinishReturning() {
            base.FinishReturning();
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

        public override List<StatInfo> GetUnitStats(int levelsToUpgrade) {
            var levelAfterUpgrade = UnitUpgrade.UpgradeLevel + levelsToUpgrade;
            return new List<StatInfo> {
                new StatInfo(StatType.TruckTotalTransportationPerSecond, GetTotalProductionDesc(),
                    GetTotalProductionAfterUpgradeBonus(levelAfterUpgrade)),

                new StatInfo(StatType.TruckSellSpeed, GetUpgradeValueDesc(UpgradeType.WorkCycleTime),
                    GetAfterUpgradeBonus(UpgradeType.WorkCycleTime, levelAfterUpgrade)),

                new StatInfo(StatType.TruckCapacity, GetUpgradeValueDesc(UpgradeType.Capacity),
                    GetAfterUpgradeBonus(UpgradeType.Capacity, levelAfterUpgrade))
            };
        }

        protected override BigInteger GetTotalProduction(int level) {
            var workCycleValueAtLevel = GetUpgradeValue<double>(UpgradeType.WorkCycleTime, level);
            var capacityValueAtLevel = GetUpgradeValue<BigInteger>(UpgradeType.Capacity, level);
            var movementTime = workCycleValueAtLevel + 2 * WalkingSpeed;
            return capacityValueAtLevel.MultipleByDouble(1 / movementTime);
        }
    }
}
