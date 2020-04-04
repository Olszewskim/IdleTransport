using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models
{
    public class LoadingRampData
    {
        [ShowInInspector] public LoaderData LoaderData { get; }
        [ShowInInspector] public TruckData TruckData { get; }

        public LoadingRampData() {
            TruckData = new TruckData();
            LoaderData = new LoaderData(TruckData);
        }
    }
}
