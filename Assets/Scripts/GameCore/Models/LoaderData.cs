using System;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class LoaderData : WorkingUnitData {
        public event Action OnLoaderStartTransportingToTruck;
        public event Action OnLoaderStartReturningToElevator;

        [ShowInInspector] public double WalkingSpeed { get; private set; }

        [ShowInInspector] private LoaderWorkingState _currentWorkingState;

        private LoaderWorkingState CurrentWorkingState {
            get => _currentWorkingState;
            set {
                if (_currentWorkingState != value) {
                    _currentWorkingState = value;
                    OnWorkingStateChanged();
                }
            }
        }

        private double _currentWalkingTime;

        public LoaderData() : base(Constants.LOADER_BASE_CAPACITY, Constants.LOADER_BASE_WORK_CYCLE_TIME) {
            WalkingSpeed = Constants.LOADER_BASE_WALKING_SPEED;
        }

        protected override void SetWorkingState() {
            CurrentWorkingState = LoaderWorkingState.Working;
        }

        public override bool IsWorking() {
            return CurrentWorkingState == LoaderWorkingState.Working;
        }

        protected override void StopWork() {
        }
    }
}
