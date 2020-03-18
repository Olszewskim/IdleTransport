using System;

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
