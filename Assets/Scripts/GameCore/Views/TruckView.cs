using IdleTransport.GameCore.Models;

namespace IdleTransport.GameCore.Views {
    public class TruckView : TransportingUnitView {
        public void Init(TruckData truckData) {
            unitData = truckData;
            base.Init();
        }

        protected override void OnReturningAnimationFinished() {
            base.OnReturningAnimationFinished();
            unitSpriteRenderer.flipX = false;
        }
    }
}
