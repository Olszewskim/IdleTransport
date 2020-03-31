using System;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public abstract class WorkingUnitData : UnitData {
        public event Action<bool> OnUnitWorkingStateChanged;
        public event Action<double> OnProgressUpdated;

        [ShowInInspector] public double WorkCycleTime { get; private set; }

        public double CurrentProductionProgress => _currentProductionCycle / WorkCycleTime;
        [ShowInInspector] private double _currentProductionCycle;

        protected WorkingUnitData(BigInteger capacity, double workCycleTime) : base(capacity) {
            WorkCycleTime = workCycleTime;
            CurrentCargoAmount = 0;
        }

        protected void StartWorking() {
            _currentProductionCycle = 0;
            SetWorkingState();
        }

        protected abstract void SetWorkingState();

        public void UpdateUnit(float deltaTime) {
            if (IsWorking()) {
                Work(deltaTime);
            }
        }

        public abstract bool IsWorking();

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
