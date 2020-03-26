namespace IdleTransport.Utilities {
    public static class Enums {
        public enum BuildingWorkingState {
            Waiting,
            Working,
            Full
        }

        public enum TrolleyWorkingState {
            Waiting,
            Loading,
            TransportingToElevator,
            LoadingElevator,
            ReturningToWarehouse
        }
    }
}
