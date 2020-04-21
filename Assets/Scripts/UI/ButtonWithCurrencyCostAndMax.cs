using IdleTransport.Utilities;
using UnityEngine;

namespace IdleTransport.UI {
    public class ButtonWithCurrencyCostAndMax : ButtonWithCurrencyCost {
        private bool _isMaxedOut;

        [SerializeField] private GameObject _maxText;
        [SerializeField] private GameObject[] _elementsToDisableWhenMaxedOut;

        public void MaxOut() {
            _isMaxedOut = true;
            RefreshVisibility();
            SetInteractivity();
        }

        protected override bool ShouldBeInteractive() {
            return !_isMaxedOut && base.ShouldBeInteractive();
        }

        public override void SetNewCurrencyCost(BigInteger currencyCost) {
            _isMaxedOut = false;
            RefreshVisibility();
            base.SetNewCurrencyCost(currencyCost);
        }

        private void RefreshVisibility() {
            _maxText.SetActive(_isMaxedOut);
            for (int i = 0; i < _elementsToDisableWhenMaxedOut.Length; i++) {
                _elementsToDisableWhenMaxedOut[i].SetActive(!_isMaxedOut);
            }
        }
    }
}
