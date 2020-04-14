using IdleTransport.GameCore.Models;

namespace IdleTransport.GameCore.Views {
    public class TruckView : TransportingUnitView {
        public void Init(TruckData truckData) {
            unitData = truckData;
            base.Init();
        }

        protected override void OnUnitReturningAnimationFinished() {
            base.OnUnitReturningAnimationFinished();
            unitSpriteRenderer.flipX = false;
        }
    }
}
