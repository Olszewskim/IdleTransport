using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views {
    public class TrolleyView : WorkingUnitView {
        protected override void Init() {
            workingUnitData = PlayerManager.Instance.Player.TrolleyData;
            base.Init();
        }
    }
}
