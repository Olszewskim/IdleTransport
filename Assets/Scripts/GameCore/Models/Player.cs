using System;
using System.Collections.Generic;
using IdleTransport.GameCore.Currencies;
using IdleTransport.JSON;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public Dictionary<CurrencyType, Currency> CurrenciesDictionary { get; }
        [ShowInInspector] public FactoryData FactoryData { get; }
        public DateTime QuitDate { get; private set; }

        public Player() {
            CurrenciesDictionary = new Dictionary<CurrencyType, Currency>();
            InitCurrencyDictionary();
            FactoryData = new FactoryData();
            SetupEvents();
        }

        public Player(PlayerJSON playerJson) {
            CurrenciesDictionary = new Dictionary<CurrencyType, Currency>();
            foreach (var currency in playerJson.currenciesDictionary) {
                CurrenciesDictionary.Add(currency.Key, CurrencyFactory.CreateCurrencyInstance(currency.Value));
            }

            FactoryData = new FactoryData(playerJson.factoryDataJSON);
            QuitDate = playerJson.quitDate;
            SetupEvents();
        }

        private void SetupEvents() {
            foreach (var currency in CurrenciesDictionary) {
                currency.Value.OnCurrencyAmountChanged += _ => Save();
            }
        }

        #region Currencies

        private void InitCurrencyDictionary() {
            var currencyTypes = (CurrencyType[]) Enum.GetValues(typeof(CurrencyType));
            for (int i = 0; i < currencyTypes.Length; i++) {
                CurrenciesDictionary.Add(currencyTypes[i],
                    CurrencyFactory.CreateCurrencyInstance(Constants.CURRENCIES_START_AMOUNT[currencyTypes[i]],
                        currencyTypes[i]));
            }
        }

        public Currency GetCurrencyType(CurrencyType currencyType) {
            if (CurrenciesDictionary.ContainsKey(currencyType)) {
                return CurrenciesDictionary[currencyType];
            }

            return null;
        }

        public void AddCurrency(CurrencyType currencyType, BigInteger amount) {
            var currency = GetCurrencyType(currencyType);
            currency?.AddCurrency(amount);
        }

        public bool SpendCurrency(CurrencyType currencyType, BigInteger amount) {
            var currency = GetCurrencyType(currencyType);
            return currency?.SpendCurrency(amount) ?? false;
        }

        #endregion

        public void Save() {
            QuitDate = DateTime.UtcNow;
            var playerJSON = new PlayerJSON(this);
            var save = JsonConvert.SerializeObject(playerJSON,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            ZPlayerPrefs.SetString(Constants.SAVE_PLAYER_KEY, save);
        }

#if UNITY_EDITOR
        [OnInspectorGUI]
        private void OnInspectorGUI() {
            Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
        }
#endif
    }
}
