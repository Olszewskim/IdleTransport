using System;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace GameCore.Models {
    public abstract class WorkingUnitData {
        public event Action<bool> OnUnitWorkingStateChanged;
        public event Action<BigInteger, BigInteger> OnCapacityStatusChanged;
        public event Action<double> OnProgressUpdated;

        private BigInteger _capacity;

        [ShowInInspector, DisplayAsString]
        public BigInteger Capacity {
            get => _capacity;
            private set {
                if (_capacity != value) {
                    _capacity = value;
                    OnCapacityStatusChanged?.Invoke(CurrentCargoAmount, Capacity);
                }
            }
        }

        [ShowInInspector] public double WorkCycleTime { get; private set; }

        private BigInteger _currentCargoAmount;

        [ShowInInspector, DisplayAsString]
        public BigInteger CurrentCargoAmount {
            get => _currentCargoAmount;
            protected set {
                if (_currentCargoAmount != value) {
                    _currentCargoAmount = value;
                    OnCapacityStatusChanged?.Invoke(CurrentCargoAmount, Capacity);
                }
            }
        }

        public double CurrentProductionProgress => _currentProductionCycle / WorkCycleTime;
        [ShowInInspector] private double _currentProductionCycle;

        protected WorkingUnitData(BigInteger capacity, double workCycleTime) {
            Capacity = capacity;
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

        private bool IsFull() {
            return CurrentCargoAmount >= Capacity;
        }

        protected abstract void StopWork();

        protected void OnWorkingStateChanged() {
            OnUnitWorkingStateChanged?.Invoke(IsWorking());
        }
    }
}
