using static IdleTransport.Utilities.Enums;

namespace IdleTransport {
    public static class GameTexts {
        public static string GetStatName(StatType statType) {
            switch (statType) {
                case StatType.WarehouseTotalProductionPerSecond:
                    return "Total Production";

                case StatType.TrolleyTotalTransportationPerSecond:
                case StatType.ElevatorTotalTransportationPerSecond:
                case StatType.LoaderTotalTransportationPerSecond:
                    return "Total Transportation";

                case StatType.TruckTotalIncomePerSecond:
                    return "Total Income";

                case StatType.WarehouseProductionSpeed:
                    return "Production Cycle Time";

                case StatType.WarehouseProductionAmountPerCycle:
                    return "Cargo Per Cycle";

                case StatType.TrolleyLoadingSpeed:
                case StatType.LoaderLoadingSpeed:
                    return "Loading Time";

                case StatType.TrolleyAmount:
                    return "Trolleys";

                case StatType.TrolleyWalkingSpeed:
                case StatType.LoaderWalkingSpeed:
                    return "Walking Speed";

                case StatType.ElevatorMovementSpeed:
                    return "Movement Speed";

                case StatType.LoaderAmount:
                    return "Loaders";

                case StatType.TruckSellSpeed:
                    return "Selling Time";

                case StatType.WarehouseCapacity:
                case StatType.TrolleyCapacity:
                case StatType.ElevatorCapacity:
                case StatType.LoaderCapacity:
                case StatType.TruckCapacity:
                    return "Capacity";
                default:
                    return "";
            }
        }

        public static string GetUnitName(UnitType unitType) {
            switch (unitType) {
                case UnitType.Warehouse:
                    return "Warehouse";
                case UnitType.Trolley:
                    return "Trolley";
                case UnitType.Elevator:
                    return "Elevator";
                case UnitType.Loader:
                    return "Loader";
                case UnitType.Truck:
                    return "Truck";
                default:
                    return "";
            }
        }

        public static string GetLevelText(int level) {
            return $"Level {level}";
        }

        public static string GetLevelUpMultiplierText(int numberOfUpgrades) {
            return $"Level Up x{numberOfUpgrades}";
        }
    }
}
