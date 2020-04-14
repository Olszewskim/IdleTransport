using System;
using System.Collections.Generic;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public abstract class UnitData {
        public event Action OnSwitchedToWaitingState;
        public event Action<BigInteger, BigInteger> OnCapacityStatusChanged;
        [ShowInInspector] public UnitType UnitType { get; }

        [ShowInInspector] public UnitUpgrade UnitUpgrade { get; }

        [ShowInInspector, DisplayAsString]
        public BigInteger Capacity => (BigInteger) UnitUpgrade.GetUpgradeValue(UpgradeType.Capacity);

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

        protected UnitData(UnitType unitType, UnitUpgrade unitUpgrade) {
            CurrentCargoAmount = 0;
            UnitType = unitType;
            UnitUpgrade = unitUpgrade;
            UnitUpgrade.OnUpgradeLevelUp += RefreshStatsAfterUpgrade;
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
            UnitUpgrade.IncreaseUpgradeLevel(levels);
        }

        protected virtual void RefreshStatsAfterUpgrade() {
            OnCapacityStatusChanged?.Invoke(CurrentCargoAmount, Capacity);
        }

        public abstract List<StatInfo> GetUnitStats();
    }
}
