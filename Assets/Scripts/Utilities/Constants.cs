using System.Collections.Generic;
using IdleTransport.GameCore.Upgrades;
using JetBrains.Annotations;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.Utilities {
    public static class Constants {
        public const string SAVE_PLAYER_KEY = "PlayerSave";
        public const string RESOURCES_ICONS_FOLDER_NAME = "Icons";
        public const string CURRENCY_ATLAS_NAME = "CurrencyAtlas";
        public const string SPRITES_FOLDER_NAME = "Sprites";

        public const ulong SECONDS_IN_HOUR = 3600;
        public const float ENABLED_GROUP_ALPHA = 1f;
        public const float DISABLED_GROUP_ALPHA = 0.65f;

        #region Buildings initial data

        public const int WAREHOUSE_BASE_CAPACITY = 60;
        public const double WAREHOUSE_BASE_WORK_CYCLE_SPEED = 7.0;
        public const int WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE = 20;

        public const int TROLLEY_BASE_CAPACITY = 25;
        public const double TROLLEY_BASE_WORK_CYCLE_TIME = 4.0;
        public const double TROLLEY_BASE_WALKING_SPEED = 3.0;

        public const int ELEVATOR_BASE_CAPACITY = 20;
        public const double ELEVATOR_TRAVEL_SPEED_PER_FLOOR = 2.5;

        public const int LOADER_BASE_CAPACITY = 25;
        public const double LOADER_BASE_WORK_CYCLE_TIME = 4.0;
        public const double LOADER_BASE_WALKING_SPEED = 4.0;

        public const int TRUCK_BASE_CAPACITY = 30;
        public const double TRUCK_BASE_WORK_CYCLE_TIME = 7.0;
        public const double TRUCK_BASE_WALKING_SPEED = 1.0;

        #endregion

        #region Upgrades

        public static readonly Dictionary<UnitType, UpgradeCost> UNIT_UPGRADE_COST =
            new Dictionary<UnitType, UpgradeCost> {
                {UnitType.Warehouse, new UpgradeCost(10, 1.16)},
                {UnitType.Trolley, new UpgradeCost(240, 1.2)},
                {UnitType.Elevator, new UpgradeCost(240, 1.2)},
                {UnitType.Loader, new UpgradeCost(240, 1.2)},
                {UnitType.Truck, new UpgradeCost(300, 1.2)}
            };

        public static readonly Dictionary<UpgradeType, UpgradeData> WAREHOUSE_UPGRADE_DATA =
            new Dictionary<UpgradeType, UpgradeData> {
                {UpgradeType.WorkCycleTime, new WorkCycleTimeUpgradeData(WAREHOUSE_BASE_WORK_CYCLE_SPEED, -0.05)},
                {UpgradeType.CargoPerCycle, new CargoPerCycleUpgradeData(WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE, 1.1)},
                {UpgradeType.Capacity, new CapacityUpgradeData(WAREHOUSE_BASE_CAPACITY, 1.3)}
            };

        public static readonly Dictionary<UpgradeType, UpgradeData> TROLLEY_UPGRADE_DATA =
            new Dictionary<UpgradeType, UpgradeData> {
                {UpgradeType.WorkCycleTime, new WorkCycleTimeUpgradeData(TROLLEY_BASE_WORK_CYCLE_TIME, -0.05)},
                {UpgradeType.NumberOfUnits, new NumberOfUnitsUpgradeData(1, 1)},
                {UpgradeType.MovementSpeed, new MovementSpeedUpgradeData(TROLLEY_BASE_WALKING_SPEED, -0.05)},
                {UpgradeType.Capacity, new CapacityUpgradeData(TROLLEY_BASE_CAPACITY, 1.3)}
            };

        public static readonly Dictionary<UpgradeType, UpgradeData> ELEVATOR_UPGRADE_DATA =
            new Dictionary<UpgradeType, UpgradeData> {
                {UpgradeType.MovementSpeed, new MovementSpeedUpgradeData(ELEVATOR_TRAVEL_SPEED_PER_FLOOR, -0.01)},
                {UpgradeType.Capacity, new CapacityUpgradeData(ELEVATOR_BASE_CAPACITY, 1.3)}
            };

        public static readonly Dictionary<UpgradeType, UpgradeData> LOADER_UPGRADE_DATA =
            new Dictionary<UpgradeType, UpgradeData> {
                {UpgradeType.WorkCycleTime, new WorkCycleTimeUpgradeData(LOADER_BASE_WORK_CYCLE_TIME, -0.05)},
                {UpgradeType.NumberOfUnits, new NumberOfUnitsUpgradeData(1, 1)},
                {UpgradeType.MovementSpeed, new MovementSpeedUpgradeData(LOADER_BASE_WALKING_SPEED, -0.05)},
                {UpgradeType.Capacity, new CapacityUpgradeData(LOADER_BASE_CAPACITY, 1.3)}
            };


        public static readonly Dictionary<UpgradeType, UpgradeData> TRUCK_UPGRADE_DATA =
            new Dictionary<UpgradeType, UpgradeData> {
                {UpgradeType.WorkCycleTime, new WorkCycleTimeUpgradeData(TRUCK_BASE_WORK_CYCLE_TIME, -0.05)},
                {UpgradeType.Capacity, new CapacityUpgradeData(TRUCK_BASE_CAPACITY, 1.3)}
            };

        #endregion

        #region Player

        public static readonly Dictionary<CurrencyType, int> CURRENCIES_START_AMOUNT = new Dictionary<CurrencyType, int> {
            {CurrencyType.Gold, 10}
        };

        #endregion
    }
}
