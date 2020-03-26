using System;
using GameCore.Models;
using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Models {
    [Serializable]
    public class Player {
        public WarehouseData WarehouseData { get; }
        public TrolleyData TrolleyData { get; }

        public Player() {
            WarehouseData = new WarehouseData();
            TrolleyData = new TrolleyData();
        }

        public Player(PlayerJSON playerJson) {
        }
    }
}
