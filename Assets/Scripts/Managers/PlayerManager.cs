using System;
using IdleTransport.GameCore.Currencies;
using IdleTransport.GameCore.Models;
using IdleTransport.Utilities;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.Managers {
    public class PlayerManager : Singleton<PlayerManager> {
        public static event Action OnPlayerLoaded;

        private readonly string _strPassword = "6yw>BuTD5p]zLhR{";
        private readonly string _strSalt = "zb]W!ep($7EAwcJ#";

        [ShowInInspector] private Player _player;

        public Player Player {
            get {
                if (_player == null) {
                    LoadSavedPlayer();
                }

                return _player;
            }
        }

        protected override void Awake() {
            base.Awake();
            if (Instance == this) {
                ZPlayerPrefs.Initialize(_strPassword, _strSalt);
                Application.targetFrameRate = 60;
                LoadSavedPlayer();
                SetupEvents();
            }
        }

        private void LoadSavedPlayer() {
            _player = null;
            if (ZPlayerPrefs.HasKey(Constants.SAVE_PLAYER_KEY)) {
                var playerJSON = JsonConvert.DeserializeObject<PlayerJSON>(
                    ZPlayerPrefs.GetString(Constants.SAVE_PLAYER_KEY),
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                _player = new Player(playerJSON);
            } else {
                _player = new Player();
            }

            OnPlayerLoaded?.Invoke();
        }

        private void SetupEvents() {
            //TODO: Load player's events
        }

        #region Currency handling

        public void AddCurrency(Currency currency) {
            AddCurrency(currency.CurrencyType, currency.CurrencyAmount);
        }

        public void AddCurrency(CurrencyType currencyType, BigInteger amount) {
            _player?.AddCurrency(currencyType, amount);
        }

        public bool SpendCurrency(CurrencyType currencyType, BigInteger amount) {
            return _player?.SpendCurrency(currencyType, amount) ?? false;
        }

        public bool HasPlayerSufficientCurrency(Enums.CurrencyType currencyType, BigInteger amount) {
            var currency = _player.GetCurrencyType(currencyType);
            return currency.HasEnoughCurrency(amount);
        }

        #endregion Currency handling
    }
}
