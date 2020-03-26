using System;
using IdleTransport.GameCore.Models;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace GameCore.Models {
    public class TrolleyData {
        [ShowInInspector, DisplayAsString] public BigInteger Capacity { get; private set; }
        [ShowInInspector] public double CargoLoadingTime { get; private set; }
        [ShowInInspector] public double WalkingSpeed { get; private set; }

        [ShowInInspector, DisplayAsString] public BigInteger CurrentCargoAmount { get; private set; }

        private WarehouseData _warehouseData;
        [ShowInInspector] private TrolleyWorkingState _currentWorkingState;
        [ShowInInspector] private double _currentProductionCycle;
            Capacity = Constants.TROLLEY_BASE_CAPACITY;
            CargoLoadingTime = Constants.TROLLEY_CARGO_LOADING_TIME;
            WalkingSpeed = Constants.TROLLEY_WALKING_SPEED;
        }
    }
}
