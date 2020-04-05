using System;

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

        public enum ElevatorWorkingState {
            Waiting,
            DistributingDownwards,
            DistributingUpwards
        }

        public enum LoaderWorkingState {
            Waiting,
            GetCargoFromElevator,
            TransportingToTruck,
            WaitingForTruck,
            Working,
            ReturningToElevator
        }

        public enum TruckWorkingState {
            Waiting,
            TransportingToMarket,
            Working,
            ReturningToGate
        }

        public enum CurrencyType {
            Gold
        }

        public static string GetEnumName<T>(T element) {
            return Enum.GetName(typeof(T), element);
        }
    }
}
