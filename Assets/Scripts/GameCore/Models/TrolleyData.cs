using IdleTransport.Utilities;

namespace GameCore.Models {
    public class TrolleyData {
        public BigInteger Capacity { get; private set; }
        public double CargoLoadingTime { get; private set; }
        public double WalkingSpeed { get; private set; }

        public TrolleyData() {
            Capacity = Constants.TROLLEY_BASE_CAPACITY;
            CargoLoadingTime = Constants.TROLLEY_CARGO_LOADING_TIME;
            WalkingSpeed = Constants.TROLLEY_WALKING_SPEED;
        }
    }
}
