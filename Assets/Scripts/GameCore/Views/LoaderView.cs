using IdleTransport.GameCore.Models;

namespace IdleTransport.GameCore.Views {
    public class LoaderView : TransportingUnitView {
        public void Init(LoaderData loaderData) {
            unitData = loaderData;
            base.Init();
        }
    }
}
