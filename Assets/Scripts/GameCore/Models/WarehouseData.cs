using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class WarehouseData : WorkingUnitData {
        [ShowInInspector, DisplayAsString]
        public BigInteger CargoPerCycle => (BigInteger) UnitUpgrade.GetUpgradeValue(UpgradeType.CargoPerCycle);

        [ShowInInspector] private BuildingWorkingState _currentWorkingState;

        private BuildingWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnWorkingStateChanged();
                }
            }
        }

        public WarehouseData()
            : base(UnitType.Warehouse, new WarehouseUpgrade()) {
            StartWorking();
        }

        protected override void StartWaiting() {
            CurrentWorkingState = BuildingWorkingState.Waiting;
        }

        public override bool IsWaiting() {
            return CurrentWorkingState == BuildingWorkingState.Waiting;
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = BuildingWorkingState.Working;
        }

        public override bool IsWorking() {
            return CurrentWorkingState == BuildingWorkingState.Working;
        }

        protected override void FinishWorking() {
            base.FinishWorking();
            CurrentCargoAmount += CargoPerCycle;
            CheckIfCapacityIsFull();
        }

        protected override void StopWork() {
            CurrentWorkingState = BuildingWorkingState.Full;
        }

        public void DistributeCargo(BigInteger availableTrolleyCapacity, out BigInteger distributedCargo) {
            distributedCargo = BigInteger.Min(CurrentCargoAmount, availableTrolleyCapacity);
            CurrentCargoAmount -= distributedCargo;
            if (IsProductionStopped()) {
                StartWorking();
            }
        }

        private bool IsProductionStopped() {
            return CurrentWorkingState == BuildingWorkingState.Full;
        }

        public override List<StatInfo> GetUnitStats() {
            return new List<StatInfo> {
                new StatInfo(StatType.WarehouseTotalProductionPerSecond, "0", "0"),
                new StatInfo(StatType.WarehouseProductionSpeed, WorkCycleTime.ToSecondsWithTwoDecimalPlaces(), "0"),
                new StatInfo(StatType.WarehouseProductionAmountPerCycle, CargoPerCycle.FormatHugeNumber(),
                    "0"),
                new StatInfo(StatType.WarehouseCapacity, Capacity.FormatHugeNumber(), "0")
            };
        }
    }
}
