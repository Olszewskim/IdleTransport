using IdleTransport.GameCore.Models;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public abstract class TransportingUnitView : WorkingUnitView {
        [SerializeField] private Transform _transportingDestinationTransform;
        private Vector3 _startPos;
        private TransportingUnitData _transportingUnitData;
        private Vector3 _destinationPos;

        protected override void Init() {
            _transportingUnitData = (TransportingUnitData) unitData;
            _transportingUnitData.OnUnitStartTransporting += StartTransportAnimation;
            _transportingUnitData.OnUnitTransporting += TransportingAnimation;
            _transportingUnitData.OnUnitFinishTransporting += OnTransportingAnimationFinished;
            _transportingUnitData.OnUnitStartReturning += StartReturningAnimation;
            _transportingUnitData.OnUnitReturning += ReturningAnimation;
            _transportingUnitData.OnUnitFinishReturning += OnUnitReturningAnimationFinished;
            _startPos = transform.position;
            SetTransportingDestination();
            base.Init();
        }

        private void SetTransportingDestination() {
            _destinationPos = new Vector3(_transportingDestinationTransform.transform.position.x,
                transform.position.y,
                transform.position.z);
        }

        private void StartTransportAnimation() {
            unitSpriteRenderer.flipX = false;
        }

        private void StartReturningAnimation() {
            unitSpriteRenderer.flipX = true;
        }

        private void TransportingAnimation(double progress) {
            transform.position = Vector3.Lerp(_startPos, _destinationPos, (float) progress);
        }

        private void ReturningAnimation(double progress) {
            transform.position = Vector3.Lerp(_destinationPos, _startPos, (float) progress);
        }

        protected virtual void OnTransportingAnimationFinished() {
        }

        protected virtual void OnUnitReturningAnimationFinished() {
        }
    }
}
