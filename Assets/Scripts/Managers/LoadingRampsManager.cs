using System.Collections.Generic;
using IdleTransport.GameCore.Models;
using Sirenix.OdinInspector;

namespace IdleTransport.Managers {
    public class LoadingRampsManager {
        [ShowInInspector] public List<LoadingRampData> LoadingRampDataList { get; }

        public LoadingRampsManager() {
            LoadingRampDataList = new List<LoadingRampData>
                {new LoadingRampData()};
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
