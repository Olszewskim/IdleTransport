using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views
{
    public class TruckView : TransportingUnitView
    {
        protected override void Init() {
            unitData = PlayerManager.Instance.Player.TruckData;
            base.Init();
        }
    }
}
