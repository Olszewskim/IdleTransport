namespace IdleTransport.Utilities {
    public static class Enums {
        public enum BuildingWorkingState {
            Waiting,
            Working,
            Full
        }

        public enum TrolleyWorkingState {
            Waiting,
            Working,
            TransportingToElevator,
            LoadingElevator,
            ReturningToWarehouse
        }

        public enum LoaderWorkingState {
            Waiting,
            GetCargoFromElevator,
            TransportingToTruck,
            Working,
            ReturningToElevator
        }
    }
}
