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
        public const string UNIT_BASE_PARAMETERS_PATH = "Data/UnitBaseParameters";

        public const ulong SECONDS_IN_HOUR = 3600;
        public const float ENABLED_GROUP_ALPHA = 1f;
        public const float DISABLED_GROUP_ALPHA = 0.65f;

        #region Buildings initial data

        public const double TRUCK_BASE_WALKING_SPEED = 1.0;

        #endregion

        #region Player

        public static readonly Dictionary<CurrencyType, int> CURRENCIES_START_AMOUNT = new Dictionary<CurrencyType, int> {
            {CurrencyType.Gold, 10}
        };

        #endregion
    }
}
