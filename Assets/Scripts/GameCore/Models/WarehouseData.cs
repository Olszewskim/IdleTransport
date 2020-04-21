using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.JSON;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class WarehouseData : WorkingUnitData {
        [ShowInInspector, DisplayAsString]
        public BigInteger CargoPerCycle => GetUpgradeValue<BigInteger>(UpgradeType.CargoPerCycle);

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

        public WarehouseData(WarehouseUpgradeData warehouseUpgradeData)
            : base(UnitType.Warehouse, new WarehouseUpgrade(warehouseUpgradeData)) {
            InitWarehouse();
        }

        public WarehouseData(WarehouseUpgradeData warehouseUpgradeData, UnitDataJSON warehouseDataJson) : base(
            UnitType.Warehouse, new WarehouseUpgrade(warehouseUpgradeData),warehouseDataJson) {
            InitWarehouse();
        }

        private void InitWarehouse() {
            if (!IsFull()) {
                StartWorking();
            } else {
                StopWork();
            }
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

        public override List<StatInfo> GetUnitStats(int levelsToUpgrade) {
            var levelAfterUpgrade = UnitUpgrade.UpgradeLevel + levelsToUpgrade;
            return new List<StatInfo> {
                new StatInfo(StatType.WarehouseTotalProductionPerSecond, GetTotalProductionDesc(),
                    GetTotalProductionAfterUpgradeBonus(levelAfterUpgrade)),

                new StatInfo(StatType.WarehouseProductionSpeed, GetUpgradeValueDesc(UpgradeType.WorkCycleTime),
                    GetAfterUpgradeBonus(UpgradeType.WorkCycleTime, levelAfterUpgrade)),

                new StatInfo(StatType.WarehouseProductionAmountPerCycle, GetUpgradeValueDesc(UpgradeType.CargoPerCycle),
                    GetAfterUpgradeBonus(UpgradeType.CargoPerCycle, levelAfterUpgrade)),

                new StatInfo(StatType.WarehouseCapacity, GetUpgradeValueDesc(UpgradeType.Capacity),
                    GetAfterUpgradeBonus(UpgradeType.Capacity, levelAfterUpgrade))
            };
        }

        protected override BigInteger GetTotalProduction(int level) {
            var workCycleValueAtLevel = GetUpgradeValue<double>(UpgradeType.WorkCycleTime, level);
            var cargoPerCycleValueAtLevel = GetUpgradeValue<BigInteger>(UpgradeType.CargoPerCycle, level);
            return cargoPerCycleValueAtLevel.MultipleByDouble(1 / workCycleValueAtLevel);
        }
    }
}
