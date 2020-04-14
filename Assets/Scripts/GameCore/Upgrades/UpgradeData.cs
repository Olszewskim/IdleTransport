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
            _baseUpgradeValue = baseUpgradeValue;
        }

        public virtual BigInteger GetUpgradeValue(int upgradeLevel) {
            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return GetUpgradeValue(upgradeLevel - 1).MultipleByDouble(upgradeMultiplier);
        }
    }

    public abstract class DoubleUpgradeData : UpgradeData {
        private double _baseUpgradeValue;

        protected DoubleUpgradeData(double baseUpgradeValue, double upgradeMultiplier) : base(upgradeMultiplier) {
            _baseUpgradeValue = baseUpgradeValue;
        }

        public double GetUpgradeValue(int upgradeLevel) {
            if (upgradeLevel == 1) {
                return _baseUpgradeValue;
            }

            return GetUpgradeValue(upgradeLevel - 1) + upgradeMultiplier;
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

    public class MovementSpeedUpgradeData : DoubleUpgradeData {
        public MovementSpeedUpgradeData(double baseUpgradeValue, double upgradeMultiplier) : base(baseUpgradeValue,
            upgradeMultiplier) {
        }
    }
}
