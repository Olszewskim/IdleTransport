using GameCore.Models;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }

        public Player() {
            WarehouseData = new WarehouseData();
            TrolleyData = new TrolleyData(WarehouseData);
        }

        public Player(PlayerJSON playerJson) {
        }
    }
}
