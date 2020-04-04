using System;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public abstract class UnitData {
        public event Action<BigInteger, BigInteger> OnCapacityStatusChanged;
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

        protected BigInteger AvailableCapacity => Capacity - CurrentCargoAmount;

        protected UnitData(BigInteger capacity) {
            Capacity = capacity;
            CurrentCargoAmount = 0;
        }

        protected abstract void StartWaiting();

        public abstract bool IsWaiting();

        protected bool IsFull() {
            return CurrentCargoAmount >= Capacity;
        }
    }
}
