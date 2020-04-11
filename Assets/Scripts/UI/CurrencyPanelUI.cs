using System;
using DG.Tweening;
using IdleTransport.GameCore.Currencies;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.UI {
    [Serializable]
    public class CurrencyViewData {
        public CurrencyType CurrencyType;
        public TextMeshProUGUI CurrencyValueText;
        public Image CurrencyIcon;

        private BigInteger _currentCurrencyAmount;

        public void SetCurrencyIcon() {
            if (CurrencyIcon) {
                CurrencyIcon.sprite = SpritesLoaderManager.LoadIcon(GetEnumName(CurrencyType));
            }
        }

        public void SetCurrencyValue(BigInteger currencyAmount) {
            _currentCurrencyAmount = currencyAmount;
            RefreshCurrencyStatusText();
        }

        private void RefreshCurrencyStatusText() {
            if (CurrencyValueText) {
                var currencyIcon = CurrencyIcon != null ? "" : Sprites.GetCurrencySprite(CurrencyType);
                CurrencyValueText.text = $"{currencyIcon} {_currentCurrencyAmount.FormatHugeNumber()}";
            }
        }
    }

    public class CurrencyPanelUI : MonoBehaviour {
        [SerializeField] private CurrencyViewData _currencyViewData;
        private Currency _currency;
        private bool _isPulsing;
        private float _pulsingAnimationTime = 0.2f;

        private void Start() {
            _currencyViewData.SetCurrencyIcon();
            InitCallback();
        }

        private void InitCallback() {
            _currency = PlayerManager.Instance.Player.GetCurrencyType(_currencyViewData.CurrencyType);
            if (_currency != null) {
                _currency.OnCurrencyAmountChanged += OnCurrencyChanged;
                _currencyViewData.SetCurrencyValue(_currency.CurrencyAmount);
            }
        }

        private void OnCurrencyChanged(BigInteger currencyAmount) {
            _currencyViewData.SetCurrencyValue(currencyAmount);
            PlayPulseIconAnimation();
        }

        private void PlayPulseIconAnimation() {
            if (!_isPulsing) {
                _isPulsing = true;
                var currencyIcon = _currencyViewData.CurrencyIcon;
                if (currencyIcon != null) {
                    DOTween.Sequence().Append(currencyIcon.transform.DOScale(1.1f, _pulsingAnimationTime * 0.4f))
                        .Append(currencyIcon.transform.DOScale(0.9f, _pulsingAnimationTime * 0.3f))
                        .Append(currencyIcon.transform.DOScale(1.05f, _pulsingAnimationTime * 0.2f))
                        .Append(currencyIcon.transform.DOScale(1, _pulsingAnimationTime * 0.1f))
                        .AppendCallback(() => _isPulsing = false);
                }
            }
        }

        private void OnDestroy() {
            _currency.OnCurrencyAmountChanged -= OnCurrencyChanged;
        }
    }
}
