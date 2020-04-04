using IdleTransport.Managers;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;

namespace IdleTransport.GameCore.Models {
    public class Player {
        [ShowInInspector] public WarehouseData WarehouseData { get; }
        [ShowInInspector] public TrolleyData TrolleyData { get; }
        [ShowInInspector] public ElevatorData ElevatorData { get; }
        [ShowInInspector] public LoadingRampsManager LoadingRampsManager { get; }

        public Player() {
            WarehouseData = new WarehouseData();
            ElevatorData = new ElevatorData();
            TrolleyData = new TrolleyData(WarehouseData, ElevatorData);
            LoadingRampsManager = new LoadingRampsManager();
        }

        public Player(PlayerJSON playerJson) {
        }

#if UNITY_EDITOR
        [OnInspectorGUI]
        private void OnInspectorGUI() {
            Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
        }
#endif
    }
}
