using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Currencies;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using UnityEngine;

namespace IdleTransport.UI {
    public class ButtonWithCurrencyCost : ButtonWithText {
        [SerializeField] private CurrencyViewData _currencyViewData;
        [SerializeField] private string _currencyCostString;

        private BigInteger _currencyCost;

        private Currency _currency;
        private bool _hasEnoughCurrencyToTakeAction;

        private void Start() {
            if (_currencyCostString.IsNullOrEmpty()) {
                _currencyCostString = "0";
            }

            _currencyCost = new BigInteger(_currencyCostString);
            Init();
        }

        private void Init() {
            RegisterCallbacks();
            SetNewCurrencyCost(_currencyCost);
        }

        public void Init(Currency currency) {
            _currencyCost = currency.CurrencyAmount;
            _currencyViewData.CurrencyType = currency.CurrencyType;
            Init();
        }

        private void RegisterCallbacks() {
            if (_currency != null) {
                _currency.OnCurrencyAmountChanged -= RefreshButtonState;
            }

            _currency = PlayerManager.Instance.Player?.GetCurrencyType(_currencyViewData.CurrencyType);
            if (_currency != null) {
                _currency.OnCurrencyAmountChanged += RefreshButtonState;
            }
        }

        public virtual void SetNewCurrencyCost(BigInteger currencyCost) {
            _currencyCost = currencyCost;
            _currencyViewData.SetCurrencyValue(currencyCost);
            if (_currency != null) {
                RefreshButtonState(_currency.CurrencyAmount);
            }
        }

        private void RefreshButtonState(BigInteger currencyAmount) {
            _hasEnoughCurrencyToTakeAction = currencyAmount >= _currencyCost;
            SetInteractivity();
        }

        public override bool? OnClick() {
            var currencyCost = _currencyCost;
            if (base.OnClick() ?? true) {
                SpendCurrency(currencyCost);
                return true;
            }

            return false;
        }

        protected void SetInteractivity() {
            RefreshButtonState(ShouldBeInteractive());
        }

        protected virtual bool ShouldBeInteractive() {
            return _hasEnoughCurrencyToTakeAction;
        }

        private void SpendCurrency(BigInteger currencyCost) {
            PlayerManager.Instance.SpendCurrency(_currencyViewData.CurrencyType, currencyCost);
        }

        private void OnDestroy() {
            if (_currency != null) {
                _currency.OnCurrencyAmountChanged -= RefreshButtonState;
            }
        }
    }
}
