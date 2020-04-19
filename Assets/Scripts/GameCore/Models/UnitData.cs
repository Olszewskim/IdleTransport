using System;
using System.Collections.Generic;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.JSON;
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
        public BigInteger Capacity => GetUpgradeValue<BigInteger>(UpgradeType.Capacity);

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

        protected UnitData(UnitType unitType, UnitUpgrade unitUpgrade, UnitDataJSON unitDataJson) : this(unitType,
            unitUpgrade) {
            CurrentCargoAmount = new BigInteger(unitDataJson.currentCargoAmount);
            UnitUpgrade.SetLevel(unitDataJson.upgradeLevel);
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

        private void RefreshStatsAfterUpgrade() {
            OnCapacityStatusChanged?.Invoke(CurrentCargoAmount, Capacity);
        }

        protected T GetUpgradeValue<T>(UpgradeType upgradeType) {
            return GetUpgradeValue<T>(upgradeType, UnitUpgrade.UpgradeLevel);
        }

        protected T GetUpgradeValue<T>(UpgradeType upgradeType, int level) {
            return (T) UnitUpgrade.GetUpgradeValue(upgradeType, level);
        }

        public abstract List<StatInfo> GetUnitStats(int levelsToUpgrade);

        protected abstract BigInteger GetTotalProduction(int level);

        private BigInteger GetTotalProduction() {
            return GetTotalProduction(UnitUpgrade.UpgradeLevel);
        }

        protected string GetTotalProductionDesc() {
            return $"{Sprites.goldSprite} {GetTotalProduction().FormatHugeNumber()}/s";
        }

        protected string GetTotalProductionAfterUpgradeBonus(int afterUpgradeLevel) {
            var difference = GetTotalProduction(afterUpgradeLevel) - GetTotalProduction();
            return $"+{Sprites.goldSprite} {difference.FormatHugeNumber()}";
        }

        protected string GetUpgradeValueDesc(UpgradeType upgradeType) {
            return UnitUpgrade.GetUpgradeValueDesc(upgradeType);
        }

        protected string GetAfterUpgradeBonus(UpgradeType upgradeType, int afterUpgradeLevel) {
            return UnitUpgrade.GetAfterUpgradeBonus(upgradeType, afterUpgradeLevel);
        }
    }
}
