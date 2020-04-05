using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Currency
{
    public class Gold : Currency
    {
        public Gold(BigInteger currencyAmount) : base(currencyAmount) {
            CurrencyType = Enums.CurrencyType.Gold;
        }
    }
}
