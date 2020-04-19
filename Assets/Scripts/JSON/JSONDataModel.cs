using System;
using System.Collections.Generic;
using IdleTransport.GameCore.Currencies;
using IdleTransport.GameCore.Models;
using IdleTransport.Managers;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.JSON {
    public class PlayerJSON {
        public Dictionary<CurrencyType, CurrencyJSON> currenciesDictionary =
            new Dictionary<CurrencyType, CurrencyJSON>();
        public FactoryDataJSON factoryDataJSON;
        public DateTime quitDate;

        public PlayerJSON() {
        }

        public PlayerJSON(Player player) {
            foreach (var currency in player.CurrenciesDictionary) {
                currenciesDictionary.Add(currency.Key, new CurrencyJSON(currency.Value));
            }

            factoryDataJSON = new FactoryDataJSON(player.FactoryData);
            quitDate = player.QuitDate;
        }
    }

    public class FactoryDataJSON {
        public UnitDataJSON warehouseDataJSON;
        public UnitDataJSON trolleyDataJSON;
        public UnitDataJSON elevatorDataJSON;
        public LoadingRampsManagerJSON loadingRampsManagerJSON;

        public FactoryDataJSON() {
        }

        public FactoryDataJSON(FactoryData factoryData) {
            warehouseDataJSON = new UnitDataJSON(factoryData.WarehouseData);
            trolleyDataJSON = new UnitDataJSON(factoryData.TrolleyData);
            elevatorDataJSON = new UnitDataJSON(factoryData.ElevatorData);
            loadingRampsManagerJSON = new LoadingRampsManagerJSON(factoryData.LoadingRampsManager);
        }
    }

    public class LoadingRampsManagerJSON {
        public List<LoadingRampDataJSON> loadingRampDataJSONList = new List<LoadingRampDataJSON>();

        public LoadingRampsManagerJSON() {
        }

        public LoadingRampsManagerJSON(LoadingRampsManager loadingRampsManager) {
            for (int i = 0; i < loadingRampsManager.LoadingRampDataList.Count; i++) {
                loadingRampDataJSONList.Add(new LoadingRampDataJSON(loadingRampsManager.LoadingRampDataList[i]));
            }
        }
    }

    public class LoadingRampDataJSON {
        public UnitDataJSON loaderDataJSON;
        public UnitDataJSON truckDataJSON;

        public LoadingRampDataJSON() {
        }

        public LoadingRampDataJSON(LoadingRampData loadingRampData) {
            loaderDataJSON = new UnitDataJSON(loadingRampData.LoaderData);
            truckDataJSON = new UnitDataJSON(loadingRampData.TruckData);
        }
    }

    public class UnitDataJSON {
        public int upgradeLevel;
        public string currentCargoAmount;

        public UnitDataJSON() {
        }

        public UnitDataJSON(UnitData unitData) {
            upgradeLevel = unitData.UnitUpgrade.UpgradeLevel;
            currentCargoAmount = unitData.CurrentCargoAmount.ToString();
        }
    }

    public class CurrencyJSON {
        public CurrencyType currencyType;
        public string currencyAmount;

        public CurrencyJSON() {
        }

        public CurrencyJSON(Currency currency) {
            currencyType = currency.CurrencyType;
            currencyAmount = currency.CurrencyAmount.ToString();
        }
    }
}
