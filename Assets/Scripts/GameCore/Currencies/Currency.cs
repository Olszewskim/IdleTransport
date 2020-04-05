using System;
using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Currencies {
    public abstract class Currency {
        public event Action<BigInteger> OnCurrencyAmountChanged;
        public Enums.CurrencyType CurrencyType { get; protected set; }

        public BigInteger CurrencyAmount { get; private set; }

        public Currency(BigInteger currencyAmount) {
            CurrencyAmount = currencyAmount;
        }

        public virtual void AddCurrency(BigInteger amount) {
            CurrencyAmount += amount;
            if (amount != 0) {
                OnCurrencyAmountChanged?.Invoke(CurrencyAmount);
            }
        }

        public virtual bool SpendCurrency(BigInteger amount) {
            if (HasEnoughCurrency(amount)) {
                AddCurrency(-amount);
                return true;
            }

            return false;
        }

        public virtual bool HasEnoughCurrency(BigInteger amount) {
            return CurrencyAmount >= amount;
        }

        public static Currency operator +(Currency c1, Currency c2) {
            c1.CurrencyAmount += c2.CurrencyAmount;
            return c1;
        }
    }
}
