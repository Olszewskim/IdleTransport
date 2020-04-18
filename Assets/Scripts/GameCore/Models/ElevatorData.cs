using System;
using System.Collections.Generic;
using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Stats;
using IdleTransport.GameCore.Upgrades;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.GameCore.Models {
    public class ElevatorData : UnitData {
        public event Action<int, double> OnElevatorMove;

        [ShowInInspector] public double TravelSpeedPerFloor => GetUpgradeValue<double>(UpgradeType.MovementSpeed);

        [ShowInInspector] private ElevatorWorkingState _currentWorkingState;

        private LoadingRampsManager _loadingRampsManager;
        [ShowInInspector] private int _currentFloorRampIndex = -1;
        private LoadingRampData _currentLoadingRampData;

        private double _currentTravelingTime;
        private double _currentTraveledDistanceProgress => _currentTravelingTime / TravelSpeedPerFloor;

        public ElevatorData(LoadingRampsManager loadingRampsManager, UnitType unitType) : base(unitType,
            new ElevatorUpgrade()) {
            _loadingRampsManager = loadingRampsManager;
            StartWaiting();
        }

        protected override void StartWaiting() {
            _currentWorkingState = ElevatorWorkingState.Waiting;
            _currentFloorRampIndex = -1;
            base.StartWaiting();
        }

        public override bool IsWaiting() {
            return _currentWorkingState == ElevatorWorkingState.Waiting;
        }

        public void UpdateUnit(float deltaTime) {
            if (IsDistributingDownwards() || IsDistributingUpwards()) {
                Move(deltaTime);
            }
        }

        public void LoadCargo(BigInteger cargo, out BigInteger loadedCargo) {
            base.LoadCargo(cargo, out loadedCargo);
            StartDistributingCargoDownwards();
        }

        private void Move(float deltaTime) {
            _currentTravelingTime += deltaTime;
            OnElevatorMove?.Invoke(_currentFloorRampIndex, _currentTraveledDistanceProgress);
            if (HasReachedTarget()) {
                TryUnloadCargo();
            }
        }

        private void StartDistributingCargoDownwards() {
            _currentWorkingState = ElevatorWorkingState.DistributingDownwards;
            SetupNextLoadingRampData();
        }

        private void SetupNextLoadingRampData() {
            _currentLoadingRampData = _loadingRampsManager.GetNextLoadingRampData(_currentFloorRampIndex);
            if (_currentLoadingRampData != null) {
                _currentTravelingTime = 0;
                _currentFloorRampIndex++;
            } else {
                StartDistributingCargoUpwards();
            }
        }

        private bool IsDistributingDownwards() {
            return _currentWorkingState == ElevatorWorkingState.DistributingDownwards;
        }

        private void TryUnloadCargo() {
            if (_currentLoadingRampData != null && !IsEmpty()) {
                _currentLoadingRampData.LoaderData.LoadCargo(CurrentCargoAmount, out var unloadedCargo);
                CurrentCargoAmount -= unloadedCargo;
            }

            if (IsEmptyAndGoingDown()) {
                StartDistributingCargoUpwards();
                return;
            }

            if (IsDistributingDownwards()) {
                SetupNextLoadingRampData();
            } else if (IsDistributingUpwards()) {
                SetupPrevLoadingRampData();
            }
        }

        private bool IsEmptyAndGoingDown() {
            return IsEmpty() && IsDistributingDownwards();
        }

        private bool IsEmpty() {
            return CurrentCargoAmount == 0;
        }

        private void StartDistributingCargoUpwards() {
            _currentWorkingState = ElevatorWorkingState.DistributingUpwards;
            SetupPrevLoadingRampData();
        }

        private void SetupPrevLoadingRampData() {
            _currentLoadingRampData = _loadingRampsManager.GetPreviousLoadingRampData(_currentFloorRampIndex);
            if (_currentLoadingRampData != null || _currentFloorRampIndex == 0) {
                _currentFloorRampIndex--;
                _currentTravelingTime = 0;
            } else {
                if (IsFull()) {
                    StartDistributingCargoDownwards();
                    return;
                }

                StartWaiting();
            }
        }

        private bool IsDistributingUpwards() {
            return _currentWorkingState == ElevatorWorkingState.DistributingUpwards;
        }

        private bool HasReachedTarget() {
            return _currentTravelingTime >= TravelSpeedPerFloor;
        }

        public override List<StatInfo> GetUnitStats() {
            return new List<StatInfo> {
                new StatInfo(StatType.ElevatorTotalTransportationPerSecond, GetTotalProductionStat(), "0"),
                new StatInfo(StatType.ElevatorMovementSpeed, TravelSpeedPerFloor.ToTimePerSecond(), "0"),
                new StatInfo(StatType.ElevatorCapacity, Capacity.FormatHugeNumber(), "0")
            };
        }

        protected override BigInteger GetTotalProduction() {
            var movementTime = TravelSpeedPerFloor * _loadingRampsManager.LoadingRampsCount;
            return Capacity.MultipleByDouble(1 / movementTime);
        }
    }
}
