using System.Collections.Generic;
using IdleTransport.GameCore.Models;
using Sirenix.OdinInspector;

namespace IdleTransport.Managers
{
    public class LoadingRampsManager
    {
        [ShowInInspector] public List<LoadingRampData> LoadingRampDataList { get; }

        public LoadingRampsManager() {
            LoadingRampDataList = new List<LoadingRampData> {new LoadingRampData(), new LoadingRampData()};
        }

    }
}
