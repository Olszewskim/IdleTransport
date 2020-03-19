using System;
using IdleTransport.Utilities;

namespace IdleTransport.GameCore.Models
{
    [Serializable]
    public class Player
    {

        public WarehouseData WarehouseData { get; }

        public Player() {
            WarehouseData = new WarehouseData();
        }

        public Player(PlayerJSON playerJson) {

        }
    }
}
