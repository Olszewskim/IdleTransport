using DG.Tweening;
using IdleTransport.GameCore.Models;
using IdleTransport.Managers;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public class TrolleyView : WorkingUnitView {
        [SerializeField] private ElevatorView _elevatorView;

        private Vector3 _startPos;

        private TrolleyData _trolleyData;

        protected override void Init() {
            unitData = PlayerManager.Instance.Player.TrolleyData;
            _trolleyData = unitData as TrolleyData;
            _trolleyData.OnTrolleyStartTransportingToElevator += StartTransportingToElevatorAnimation;
            _startPos = transform.position;
            base.Init();
        }

        private void StartTransportingToElevatorAnimation() {
            var destinationPos = new Vector3(_elevatorView.transform.position.x, transform.position.y,
                transform.position.z);
            transform.DOMove(destinationPos, (float) _trolleyData.WalkingSpeed);
            unitSpriteRenderer.flipX = false;
        }
    }
}
