using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views {
    public class WarehouseView : WorkingUnitView {
        protected override void Init() {
            unitData = PlayerManager.Instance.Player.FactoryData.WarehouseData;
            base.Init();
        }
    }
}
