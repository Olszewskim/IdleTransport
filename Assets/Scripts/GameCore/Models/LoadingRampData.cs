using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models
{
    public class LoadingRampData
    {
        [ShowInInspector] public LoaderData LoaderData { get; }
        [ShowInInspector] public TruckData TruckData { get; }

        public LoadingRampData() {
            LoaderData = new LoaderData();
            TruckData = new TruckData();
        }
    }
}
