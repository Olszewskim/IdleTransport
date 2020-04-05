using IdleTransport.Utilities;
using UnityEngine;

namespace IdleTransport.GameCore.Currencies {
    public static class CurrencyFactory {
        public static Currencies.Currency CreateCurrencyInstance(CurrencyJSON currencyJSON) {
            return CreateCurrencyInstance(new BigInteger(currencyJSON.currencyAmount), currencyJSON.currencyType);
        }

        public static Currencies.Currency CreateCurrencyInstance(BigInteger currencyAmount, Enums.CurrencyType currencyType) {
            switch (currencyType) {
                case Enums.CurrencyType.Gold:
                    return new Gold(currencyAmount);
            }

            Debug.LogError("Wrong currency type!");
            return null;
        }
    }
}
