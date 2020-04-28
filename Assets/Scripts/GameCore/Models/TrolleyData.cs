using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.JSON;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class TrolleyData : TransportingUnitData {
        private WarehouseData _warehouseData;
        private ElevatorData _elevatorData;

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

        public TrolleyData(TrolleyUpgradeData trolleyUpgradeData, WarehouseData warehouseData,
            ElevatorData elevatorData)
            : base(UnitType.Trolley, new TrolleyUpgrade(trolleyUpgradeData)) {
            InitTrolley(warehouseData, elevatorData);
        }

        public TrolleyData(TrolleyUpgradeData trolleyUpgradeData, WarehouseData warehouseData,
            ElevatorData elevatorData, UnitDataJSON unitDataJson)
            : base(UnitType.Trolley, new TrolleyUpgrade(trolleyUpgradeData), unitDataJson) {
            InitTrolley(warehouseData, elevatorData);
        }

        private void InitTrolley(WarehouseData warehouseData, ElevatorData elevatorData) {
            _warehouseData = warehouseData;
            _elevatorData = elevatorData;
            _elevatorData.OnSwitchedToWaitingState += TryToLoadElevator;
            if (!IsFull()) {
                StartWorking();
            } else {
                StopWork();
            }
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
            base.FinishTransporting();
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
            base.FinishReturning();
            StartWorking();
        }

        public override List<StatInfo> GetUnitStats(int levelsToUpgrade) {
            var levelAfterUpgrade = UnitUpgrade.UpgradeLevel + levelsToUpgrade;
            return new List<StatInfo> {
                new StatInfo(StatType.TrolleyTotalTransportationPerSecond, UnitUpgrade.GetTotalProductionDesc(),
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
    }
}
