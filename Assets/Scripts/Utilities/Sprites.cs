using static IdleTransport.Utilities.Enums;

namespace IdleTransport.Utilities {
    public static class Sprites {
        public static readonly string goldSprite = "<sprite name=\"Gold\">";

        public static string GetCurrencySprite(CurrencyType currencyType) {
            switch (currencyType) {
                case CurrencyType.Gold:
                    return goldSprite;
            }

            return "";
        }
    }
}
