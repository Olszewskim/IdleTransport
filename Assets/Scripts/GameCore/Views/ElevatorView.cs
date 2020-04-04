using IdleTransport.GameCore.Models;
using IdleTransport.Managers;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public class ElevatorView : UnitView {
        private const int START_RAMP_INDEX = -1;
        [SerializeField] private LoadingRampsView _loadingRampsView;

        private ElevatorData _elevatorData;
        private int _currentDestinationIndex;
        private Vector3 _startPosition;
        private LoadingRampView _currentLoadingRampMovingTo;
        private Vector3 _destinationPosition;
        private Vector3 _lastDestinationPosition;

        protected override void Init() {
            unitData = PlayerManager.Instance.Player.ElevatorData;
            _elevatorData = (ElevatorData) unitData;
            _elevatorData.OnElevatorMove += MoveElevator;
            _startPosition = _destinationPosition = _lastDestinationPosition = transform.position;
            _currentDestinationIndex = START_RAMP_INDEX;
            base.Init();
        }

        private void Update() {
            _elevatorData.UpdateUnit(Time.deltaTime);
        }

        private void MoveElevator(int destinationIndex, double movingProgress) {
            if (destinationIndex != _currentDestinationIndex) {
                _currentDestinationIndex = destinationIndex;
                _destinationPosition = GetNewDestinationPoint();
            }

            transform.position = Vector3.Lerp(_lastDestinationPosition, _destinationPosition, (float)movingProgress);
        }

        private Vector3 GetNewDestinationPoint() {
            _lastDestinationPosition = _destinationPosition;
            if (_currentDestinationIndex == START_RAMP_INDEX) {
                return _startPosition;
            }

            var loadingRamp = _loadingRampsView.GetLoadingRampViewOfIndex(_currentDestinationIndex);
            return new Vector3(_startPosition.x, loadingRamp.transform.position.y, _startPosition.z);
        }
    }
}
