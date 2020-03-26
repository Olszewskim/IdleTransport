using GameCore.Views;
using IdleTransport.Managers;

namespace IdleTransport.GameCore.Views {
    public class WarehouseView : WorkingUnitView {
        protected override void Init() {
            workingUnitData = PlayerManager.Instance.Player.WarehouseData;
            base.Init();
        }
    }
}
