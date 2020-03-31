using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public  ElevatorData ElevatorData { get; }

        public Player() {
            WarehouseData = new WarehouseData();
            TrolleyData = new TrolleyData(WarehouseData);
            ElevatorData = new ElevatorData();
        }

        public Player(PlayerJSON playerJson) {
        }
    }
}
