using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class ElevatorData : UnitData {
        [ShowInInspector] public double TravelSpeedPerFloor { get; private set; }

        public ElevatorData() : base(Constants.ELEVATOR_BASE_CAPACITY) {
            TravelSpeedPerFloor = Constants.ELEVATOR_TRAVEL_SPEED_PER_FLOOR;
        }

        public void LoadCargo(BigInteger cargo, out BigInteger loadedCargo) {
            loadedCargo = BigInteger.Min(AvailableCapacity, cargo);
            CurrentCargoAmount += loadedCargo;
        }
    }
}
