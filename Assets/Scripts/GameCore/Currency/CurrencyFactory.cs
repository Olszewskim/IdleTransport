using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Currency {
    public static class CurrencyFactory {
        public static Currency CreateCurrencyInstance(CurrencyJSON currencyJSON) {
            return CreateCurrencyInstance(new BigInteger(currencyJSON.currencyAmount), currencyJSON.currencyType);
        }

        public static Currency CreateCurrencyInstance(BigInteger currencyAmount, CurrencyType currencyType) {
            switch (currencyType) {
                case CurrencyType.Gold:
                    return new Gold(currencyAmount);
            }

            Debug.LogError("Wrong currency type!");
            return null;
        }
    }
}
