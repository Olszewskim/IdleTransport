using System;
using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTransport.GameCore.Upgrades {
    public abstract class UpgradeData {
        [SerializeField][PropertyOrder(1)] protected double upgradeMultiplier;
    }

    [Serializable]
    public abstract class BigIntegerUpgradeData : UpgradeData {
        [SerializeField][PropertyOrder(0)] private string _baseUpgradeValueString;
        private BigInteger _baseUpgradeValue;

        public virtual BigInteger GetUpgradeValue(int upgradeLevel) {
            if (_baseUpgradeValue == null) {
                _baseUpgradeValue = GetBaseUpgradeValue();
            }

            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return GetUpgradeValue(upgradeLevel - 1).MultipleByDouble(upgradeMultiplier);
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
    }

    [Serializable]
    public abstract class DoubleUpgradeData : UpgradeData {
        [SerializeField][PropertyOrder(0)] private double _baseUpgradeValue;

        public double GetUpgradeValue(int upgradeLevel) {
            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return GetUpgradeValue(upgradeLevel - 1) + upgradeMultiplier;
        }
    }

    [Serializable]
    public class WorkCycleTimeUpgradeData : DoubleUpgradeData {
    }

    [Serializable]
    public class CargoPerCycleUpgradeData : BigIntegerUpgradeData {
    }

    [Serializable]
    public class CapacityUpgradeData : BigIntegerUpgradeData {
    }

    [Serializable]
    public class NumberOfUnitsUpgradeData : BigIntegerUpgradeData {
        public override BigInteger GetUpgradeValue(int upgradeLevel) {
            if (upgradeLevel < 10) {
                return 1;
            }

            if (upgradeLevel < 50) {
                return 2;
            }

            if (upgradeLevel < 100) {
                return 3;
            }

            if (upgradeLevel < 200) {
                return 4;
            }

            if (upgradeLevel < 400) {
                return 5;
            }

            return 6;
        }
    }

    [Serializable]
    public class MovementSpeedUpgradeData : DoubleUpgradeData {
    }
}
