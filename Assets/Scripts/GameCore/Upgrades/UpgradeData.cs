using IdleTransport.ExtensionsMethods;
using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Upgrades {
    public abstract class UpgradeData {
        protected double upgradeMultiplier;

        protected UpgradeData(double upgradeMultiplier) {
            this.upgradeMultiplier = upgradeMultiplier;
        }
    }

    public abstract class BigIntegerUpgradeData : UpgradeData {
        private BigInteger _baseUpgradeValue;

        protected BigIntegerUpgradeData(BigInteger baseUpgradeValue, double upgradeMultiplier) :
            base(upgradeMultiplier) {
        }

        public BigInteger GetUpgradeLevelValue(int upgradeLevel) {
            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return GetUpgradeLevelValue(upgradeLevel - 1).MultipleByDouble(upgradeMultiplier);
        }
    }

    public abstract class DoubleUpgradeData : UpgradeData {
        private double _baseUpgradeValue;

        protected DoubleUpgradeData(double baseUpgradeValue, double upgradeMultiplier) : base(upgradeMultiplier) {
            _baseUpgradeValue = baseUpgradeValue;
        }

        public double GetUpgradeLevelValue(int upgradeLevel) {
            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return GetUpgradeLevelValue(upgradeLevel - 1) + upgradeMultiplier;
        }
    }

    public class WorkCycleTimeUpgradeData : DoubleUpgradeData {
        public WorkCycleTimeUpgradeData(double baseUpgradeValue, double upgradeMultiplier) : base(baseUpgradeValue,
            upgradeMultiplier) {
        }
    }

    public class CargoPerCycleUpgradeData : BigIntegerUpgradeData {
        public CargoPerCycleUpgradeData(BigInteger baseUpgradeValue, double upgradeMultiplier) : base(baseUpgradeValue,
            upgradeMultiplier) {
        }
    }

    public class CapacityUpgradeData : BigIntegerUpgradeData {
        public CapacityUpgradeData(BigInteger baseUpgradeValue, double upgradeMultiplier) : base(baseUpgradeValue,
            upgradeMultiplier) {
        }
    }

    public class NumberOfUnitsUpgradeData : BigIntegerUpgradeData {
        public NumberOfUnitsUpgradeData(BigInteger baseUpgradeValue, double upgradeMultiplier) : base(baseUpgradeValue,
            upgradeMultiplier) {
        }
    }

    public class MovementSpeedUpgradeData : DoubleUpgradeData {
        public MovementSpeedUpgradeData(double baseUpgradeValue, double upgradeMultiplier) : base(baseUpgradeValue,
            upgradeMultiplier) {
        }
    }
}
