using System;
using System.Collections.Generic;
using IdleTransport.GameCore.Stats;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public abstract class UnitData {
        public event Action OnSwitchedToWaitingState;
        public event Action<BigInteger, BigInteger> OnCapacityStatusChanged;
        public event Action OnUnitUpgraded;
        [ShowInInspector] public UnitType UnitType { get; }
        [ShowInInspector] public int UpgradeLevel { get; private set; }

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

        protected UnitData(BigInteger capacity, UnitType unitType) {
            UpgradeLevel = 1;
            Capacity = capacity;
            CurrentCargoAmount = 0;
            UnitType = unitType;
        }

        protected virtual void StartWaiting() {
            OnSwitchedToWaitingState?.Invoke();
        }

        public abstract bool IsWaiting();

        public bool IsFull() {
            return CurrentCargoAmount >= Capacity;
        }

        public virtual void LoadCargo(BigInteger cargo, out BigInteger loadedCargo) {
            loadedCargo = BigInteger.Min(AvailableCapacity, cargo);
            CurrentCargoAmount += loadedCargo;
        }

        public void UpgradeUnit(int levels) {
            UpgradeLevel += levels;
            OnUnitUpgraded?.Invoke();
        }

        public abstract List<StatInfo> GetUnitStats();


    }
}
