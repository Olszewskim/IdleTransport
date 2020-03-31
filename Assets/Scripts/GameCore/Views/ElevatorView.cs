using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views {
    public class ElevatorView : UnitView {
        protected override void Init() {
            unitData = PlayerManager.Instance.Player.ElevatorData;
            base.Init();
        }
    }
}
