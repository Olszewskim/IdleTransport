using IdleTransport.JSON;
using IdleTransport.ScriptableObjectData;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class LoadingRampData {
        [ShowInInspector] public LoaderData LoaderData { get; }
        [ShowInInspector] public TruckData TruckData { get; }

        public LoadingRampData(UnitBaseParameters unitBaseParameters) {
            TruckData = new TruckData(unitBaseParameters.truckUpgradeData);
            LoaderData = new LoaderData(unitBaseParameters.loaderUpgradeData, TruckData);
        }

        public LoadingRampData(UnitBaseParameters unitBaseParameters, LoadingRampDataJSON loadingRampDataJson) {
            TruckData = new TruckData(unitBaseParameters.truckUpgradeData, loadingRampDataJson.truckDataJSON);
            LoaderData = new LoaderData(unitBaseParameters.loaderUpgradeData, TruckData, loadingRampDataJson.loaderDataJSON);
        }
    }
}
