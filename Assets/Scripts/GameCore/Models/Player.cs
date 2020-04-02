using System.Collections.Generic;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public ElevatorData ElevatorData { get; }

        [ShowInInspector] public List<LoadingRampData> LoadingRampDataList { get; }

        public Player() {
            WarehouseData = new WarehouseData();
            ElevatorData = new ElevatorData();
            TrolleyData = new TrolleyData(WarehouseData, ElevatorData);
            LoadingRampDataList = new List<LoadingRampData> {new LoadingRampData(), new LoadingRampData()};
        }

        public Player(PlayerJSON playerJson) {
        }
    }
}
