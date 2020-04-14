using System;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public abstract class TransportingUnitData : WorkingUnitData {
        public event Action OnUnitStartTransporting;
        public event Action OnUnitStartReturning;

        [ShowInInspector] public double WalkingSpeed { get; private set; }
        [ShowInInspector] private double _currentWalkingTime;

        protected TransportingUnitData(double workCycleTime, double walkingSpeed,
            UnitType unitType, UnitUpgrade unitUpgrade)
            : base(workCycleTime, unitType, unitUpgrade) {
            WalkingSpeed = walkingSpeed;
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
            //TODO: Adjust Walking Speed ​after upgrade on the ​next cycle
            if (_currentWalkingTime >= WalkingSpeed) {
                FinishTransporting();
            }
        }

        protected abstract void FinishTransporting();

        protected abstract bool IsReturning();

        protected void StartReturning() {
            SetReturningState();
            _currentWalkingTime = 0;
            OnUnitStartReturning?.Invoke();
        }

        protected abstract void SetReturningState();

        private void Return(float deltaTime) {
            _currentWalkingTime += deltaTime;
            if (HasReachedTarget()) {
                FinishReturning();
            }
        }

        protected abstract void FinishReturning();

        private bool HasReachedTarget() {
            return _currentWalkingTime >= WalkingSpeed;
        }
    }
}
