using System;
using IdleTransport.GameCore.Upgrades;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public abstract class TransportingUnitData : WorkingUnitData {
        public event Action OnUnitStartTransporting;
        public event Action<double> OnUnitTransporting;
        public event Action OnUnitFinishTransporting;
        public event Action OnUnitStartReturning;
        public event Action<double> OnUnitReturning;
        public event Action OnUnitFinishReturning;

        [ShowInInspector]
        public virtual double WalkingSpeed => (double) UnitUpgrade.GetUpgradeValue(UpgradeType.MovementSpeed);

        [ShowInInspector] private double _currentWalkingTime;
        private double _currentTraveledDistanceProgress => _currentWalkingTime / WalkingSpeed;

        protected TransportingUnitData(UnitType unitType, UnitUpgrade unitUpgrade)
            : base(unitType, unitUpgrade) {
        }

        public override void UpdateUnit(float deltaTime) {
            base.UpdateUnit(deltaTime);

            if (IsTransporting()) {
                Transport(deltaTime);
            }

            if (IsReturning()) {
                Return(deltaTime);
            }
        }

        protected abstract bool IsTransporting();

        protected void StartTransporting() {
            SetTransportingState();
            _currentWalkingTime = 0;
            OnUnitStartTransporting?.Invoke();
        }

        protected abstract void SetTransportingState();

        private void Transport(float deltaTime) {
            _currentWalkingTime += deltaTime;
            OnUnitTransporting?.Invoke(_currentTraveledDistanceProgress);
            if (_currentWalkingTime >= WalkingSpeed) {
                FinishTransporting();
            }
        }

        protected virtual void FinishTransporting() {
            OnUnitFinishTransporting?.Invoke();
        }

        protected abstract bool IsReturning();

        protected void StartReturning() {
            SetReturningState();
            _currentWalkingTime = 0;
            OnUnitStartReturning?.Invoke();
        }

        protected abstract void SetReturningState();

        private void Return(float deltaTime) {
            _currentWalkingTime += deltaTime;
            OnUnitReturning?.Invoke(_currentTraveledDistanceProgress);
            if (HasReachedTarget()) {
                FinishReturning();
            }
        }

        protected virtual void FinishReturning() {
            OnUnitFinishReturning?.Invoke();
        }

        private bool HasReachedTarget() {
            return _currentWalkingTime >= WalkingSpeed;
        }
    }
}
