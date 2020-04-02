using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views
{
    public class LoaderView : TransportingUnitView
    {
        protected override void Init() {
            unitData = PlayerManager.Instance.Player.LoaderData;
            base.Init();
        }
    }
}
