using System;

namespace IdleTransport.Utilities {
    public static class Enums {
        public enum UnitType {
            Warehouse,
            Trolley,
            Elevator,
            Loader,
            Truck
        }

        #region WorkingStates

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

        #endregion

        public enum CurrencyType {
            Gold
        }

        public enum UpgradeMultiplierMode {
            x1,
            x10,
            x50,
            Max
        }

        public enum StatType {
            //Not Upgradable
            WarehouseTotalProductionPerSecond,
            TrolleyTotalTransportationPerSecond,
            ElevatorTotalTransportationPerSecond,
            LoaderTotalTransportationPerSecond,
            TruckTotalIncomePerSecond,

            //Upgradable
            WarehouseProductionSpeed,
            WarehouseProductionAmountPerCycle,
            WarehouseCapacity,
            TrolleyLoadingSpeed,
            TrolleyAmount,
            TrolleyWalkingSpeed,
            TrolleyCapacity,
            ElevatorMovementSpeed,
            ElevatorCapacity,
            LoaderLoadingSpeed,
            LoaderAmount,
            LoaderWalkingSpeed,
            LoaderCapacity,
            TruckSellSpeed,
            TruckCapacity
        }

        public enum UpgradeType{
            WorkCycleTime,
            CargoPerCycle,
            Capacity,
            NumberOfUnits,
            MovementSpeed
        }

        public static string GetEnumName<T>(T element) {
            return Enum.GetName(typeof(T), element);
        }
    }
}
