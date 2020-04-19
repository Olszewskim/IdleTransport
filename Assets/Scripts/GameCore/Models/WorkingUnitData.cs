using System;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.JSON;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public abstract class WorkingUnitData : UnitData {
        public event Action<bool> OnUnitWorkingStateChanged;
        public event Action<double> OnProgressUpdated;

        [ShowInInspector] public double WorkCycleTime => GetUpgradeValue<double>(UpgradeType.WorkCycleTime);

        public double CurrentProductionProgress => _currentProductionCycle / WorkCycleTime;
        [ShowInInspector] private double _currentProductionCycle;

        protected WorkingUnitData(UnitType unitType, UnitUpgrade unitUpgrade)
            : base(unitType, unitUpgrade) {
        }

        protected WorkingUnitData(UnitType unitType, UnitUpgrade unitUpgrade, UnitDataJSON unitDataJson)
            : base(unitType, unitUpgrade, unitDataJson) {
        }

        protected void StartWorking() {
            _currentProductionCycle = 0;
            SetWorkingState();
        }

        public abstract bool IsWorking();

        protected abstract void SetWorkingState();

        public virtual void UpdateUnit(float deltaTime) {
            if (IsWorking()) {
                Work(deltaTime);
            }
        }

        private void Work(float deltaTime) {
            _currentProductionCycle += deltaTime;
            OnProgressUpdated?.Invoke(CurrentProductionProgress);
            if (IsWorkingFinished()) {
                FinishWorking();
            }
        }

        private bool IsWorkingFinished() {
            return _currentProductionCycle >= WorkCycleTime;
        }

        protected virtual void FinishWorking() {
            _currentProductionCycle = 0;
        }

        protected void CheckIfCapacityIsFull() {
            if (IsFull()) {
                CurrentCargoAmount = Capacity;
                StopWork();
            }
        }

        protected abstract void StopWork();

        protected void OnWorkingStateChanged() {
            OnUnitWorkingStateChanged?.Invoke(IsWorking());
        }
    }
}
