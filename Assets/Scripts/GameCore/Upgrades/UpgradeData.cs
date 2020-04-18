using System;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Upgrades {
    public abstract class UpgradeData {
        [SerializeField] [PropertyOrder(1)] protected double upgradeMultiplier;

        public abstract object GetUpgradeValue(int upgradeLevel);
        public abstract string GetAfterUpgradeBonus(int currentUpgradeLevel, int afterUpgradeLevel);
        public abstract string GetUpgradeValueDesc(int upgradeLevel);
    }

    [Serializable]
    public abstract class BigIntegerUpgradeData : UpgradeData {
        [SerializeField] [PropertyOrder(0)] private string _baseUpgradeValueString;
        private BigInteger _baseUpgradeValue;

        public override object GetUpgradeValue(int upgradeLevel) {
            if (_baseUpgradeValue == null) {
                _baseUpgradeValue = GetBaseUpgradeValue();
            }

            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return ((BigInteger) GetUpgradeValue(upgradeLevel - 1)).MultipleByDouble(upgradeMultiplier);
        }

        private BigInteger GetBaseUpgradeValue() {
            try {
                var baseUpgradeValue = new BigInteger(_baseUpgradeValueString);
                return baseUpgradeValue;
            } catch (Exception e) {
                Debug.LogError(e);
                return new BigInteger();
            }
        }

        public override string GetAfterUpgradeBonus(int currentUpgradeLevel, int afterUpgradeLevel) {
            var currentUpgradeValue = (BigInteger) GetUpgradeValue(currentUpgradeLevel);
            var afterUpgradeValue = (BigInteger) GetUpgradeValue(afterUpgradeLevel);
            var difference = afterUpgradeValue - currentUpgradeValue;
            return $"+{difference.FormatHugeNumber()}";
        }

        public override string GetUpgradeValueDesc(int upgradeLevel) {
            var upgradeValue = (BigInteger) GetUpgradeValue(upgradeLevel);
            return upgradeValue.FormatHugeNumber();
        }
    }

    [Serializable]
    public abstract class DoubleUpgradeData : UpgradeData {
        [SerializeField] [PropertyOrder(0)] private double _baseUpgradeValue;
        private DoubleFormatType _doubleFormatType;

        public DoubleUpgradeData(DoubleFormatType doubleFormatType) {
            _doubleFormatType = doubleFormatType;
        }

        public override object GetUpgradeValue(int upgradeLevel) {
            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return (double) GetUpgradeValue(upgradeLevel - 1) + upgradeMultiplier;
        }

        public override string GetAfterUpgradeBonus(int currentUpgradeLevel, int afterUpgradeLevel) {
            var currentUpgradeValue = (double) GetUpgradeValue(currentUpgradeLevel);
            var afterUpgradeValue = (double) GetUpgradeValue(afterUpgradeLevel);
            if (_doubleFormatType == DoubleFormatType.ToSeconds) {
                var difference = afterUpgradeValue - currentUpgradeValue;
                return difference.ToSecondsWithTwoDecimalPlaces();
            } else {
                var difference = 1 / afterUpgradeValue - 1 / currentUpgradeValue;
                return $"+{difference.ToTimeWithThreeDecimalPlaces()}";
            }

        }

        public override string GetUpgradeValueDesc(int upgradeLevel) {
            var upgradeValue = (double) GetUpgradeValue(upgradeLevel);
            return _doubleFormatType == DoubleFormatType.ToSeconds
                ? upgradeValue.ToSecondsWithTwoDecimalPlaces()
                : upgradeValue.ToTimePerSecond();
        }
    }

    [Serializable]
    public class WorkCycleTimeUpgradeData : DoubleUpgradeData {
        public WorkCycleTimeUpgradeData() : base(DoubleFormatType.ToSeconds) {
        }
    }

    [Serializable]
    public class CargoPerCycleUpgradeData : BigIntegerUpgradeData {
    }

    [Serializable]
    public class CapacityUpgradeData : BigIntegerUpgradeData {
    }

    [Serializable]
    public class NumberOfUnitsUpgradeData : BigIntegerUpgradeData {
        public override object GetUpgradeValue(int upgradeLevel) {
            if (upgradeLevel < 10) {
                return new BigInteger(1);
            }

            if (upgradeLevel < 50) {
                return new BigInteger(2);
            }

            if (upgradeLevel < 100) {
                return new BigInteger(3);
            }

            if (upgradeLevel < 200) {
                return new BigInteger(4);
            }

            if (upgradeLevel < 400) {
                return new BigInteger(5);
            }

            return new BigInteger(6);
        }
    }

    [Serializable]
    public class MovementSpeedUpgradeData : DoubleUpgradeData {
        public MovementSpeedUpgradeData() : base(DoubleFormatType.ToTimePerSecond) {
        }
    }
}
