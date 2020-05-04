using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IdleTransport.GameCore.Models;
using IdleTransport.JSON;
using IdleTransport.ScriptableObjectData;
using Sirenix.OdinInspector;

namespace IdleTransport.Managers {
    public class LoadingRampsManager {
        [ShowInInspector] public List<LoadingRampData> LoadingRampDataList { get; }
        public int LoadingRampsCount => LoadingRampDataList.Count;

        public LoadingRampsManager(UnitBaseParameters unitBaseParameters, int numberOfFloors = 1) {
            LoadingRampDataList = Enumerable.Repeat(new LoadingRampData(unitBaseParameters), numberOfFloors).ToList();
        }

        public LoadingRampsManager(UnitBaseParameters unitBaseParameters,
            LoadingRampsManagerJSON loadingRampsManagerJson) {
            LoadingRampDataList = new List<LoadingRampData>();
            for (int i = 0; i < loadingRampsManagerJson.loadingRampDataJSONList.Count; i++) {
                LoadingRampDataList.Add(new LoadingRampData(unitBaseParameters,
                    loadingRampsManagerJson.loadingRampDataJSONList[i]));
            }
        }

        public LoadingRampData GetNextLoadingRampData(int currentRampIndex) {
            var nextIndex = currentRampIndex + 1;
            if (nextIndex >= LoadingRampDataList.Count) {
                return null;
            }

            return LoadingRampDataList[nextIndex];
        }

        public LoadingRampData GetPreviousLoadingRampData(int currentRampIndex) {
            var prevIndex = currentRampIndex - 1;
            if (prevIndex < 0) {
                return null;
            }

            return LoadingRampDataList[prevIndex];
        }
    }
}
