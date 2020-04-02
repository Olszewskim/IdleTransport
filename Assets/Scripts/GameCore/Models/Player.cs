using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public ElevatorData ElevatorData { get; }
        [ShowInInspector] public LoaderData LoaderData { get; }

        public Player() {
            WarehouseData = new WarehouseData();
            ElevatorData = new ElevatorData();
            TrolleyData = new TrolleyData(WarehouseData, ElevatorData);
            LoaderData = new LoaderData();
        }

        public Player(PlayerJSON playerJson) {
        }
    }
}
