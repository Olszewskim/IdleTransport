using System.Collections.Generic;
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

        public const int WAREHOUSE_BASE_CAPACITY = 5;
        public const double WAREHOUSE_BASE_WORK_CYCLE_SPEED = 7.0;
        public const int WAREHOUSE_BASE_CARGO_AMOUNT_IN_PACKAGE = 1;

        public const int TROLLEY_BASE_CAPACITY = 5;
        public const double TROLLEY_BASE_WORK_CYCLE_TIME = 4.0;
        public const double TROLLEY_BASE_WALKING_SPEED = 3.0;

        public const int ELEVATOR_BASE_CAPACITY = 5;
        public const double ELEVATOR_TRAVEL_SPEED_PER_FLOOR = 1.0;

        public const int LOADER_BASE_CAPACITY = 5;
        public const double LOADER_BASE_WORK_CYCLE_TIME = 4.0;
        public const double LOADER_BASE_WALKING_SPEED = 2.0;

        public const int TRUCK_BASE_CAPACITY = 10;
        public const double TRUCK_BASE_WORK_CYCLE_TIME = 5.0;
        public const double TRUCK_BASE_WALKING_SPEED = 1.0;

        #endregion

        #region Player

        public static readonly Dictionary<CurrencyType, int> CURRENCIES_START_AMOUNT = new Dictionary<CurrencyType, int> {
            {CurrencyType.Gold, 10}
        };

        #endregion
    }
}
