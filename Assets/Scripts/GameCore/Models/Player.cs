using System;
using System.Collections.Generic;
using IdleTransport.GameCore.Currencies;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public Dictionary<CurrencyType, Currency> CurrenciesDictionary { get; }

        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public ElevatorData ElevatorData { get; }
        [ShowInInspector] public LoadingRampsManager LoadingRampsManager { get; }

        public Player() {
            CurrenciesDictionary = new Dictionary<CurrencyType, Currency>();
            InitCurrencyDictionary();
            LoadingRampsManager = new LoadingRampsManager();
            WarehouseData = new WarehouseData();
            ElevatorData = new ElevatorData(LoadingRampsManager);
            TrolleyData = new TrolleyData(WarehouseData, ElevatorData);
        }

        public Player(PlayerJSON playerJson) {
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

        #endregion

#if UNITY_EDITOR
        [OnInspectorGUI]
        private void OnInspectorGUI() {
            Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
        }
#endif
    }
}
