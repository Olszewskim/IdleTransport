using IdleTransport.GameCore.Currency;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.Utilities {
    public class PlayerJSON {
    }

    public class CurrencyJSON {
        public CurrencyType currencyType;
        public string currencyAmount;

        public CurrencyJSON(Currency currency) {
            currencyType = currency.CurrencyType;
            currencyAmount = currency.CurrencyAmount.ToString();
        }
    }
}
