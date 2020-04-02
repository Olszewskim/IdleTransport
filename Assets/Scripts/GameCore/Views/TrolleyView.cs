using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views {
    public class TrolleyView : TransportingUnitView {
        protected override void Init() {
            unitData = PlayerManager.Instance.Player.TrolleyData;
            base.Init();
        }
    }
}
