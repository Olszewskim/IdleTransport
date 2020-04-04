
using DG.Tweening;
using IdleTransport.GameCore.Models;
using UnityEngine;

namespace IdleTransport.GameCore.Views
{
    public abstract class TransportingUnitView : WorkingUnitView {
        [SerializeField] private Transform _transportingDestinationTransform;
        private Vector3 _startPos;
        private TransportingUnitData _transportingUnitData;
        protected override void Init() {
             _transportingUnitData = (TransportingUnitData) unitData;
             _transportingUnitData.OnUnitStartTransporting += StartTransportingAnimation;
             _transportingUnitData.OnUnitStartReturning += StartReturningAnimation;
            _startPos = transform.position;
            base.Init();
        }

        private void StartTransportingAnimation() {
            var destinationPos = new Vector3(_transportingDestinationTransform.transform.position.x, transform.position.y,
                transform.position.z);
            transform.DOMove(destinationPos, (float) _transportingUnitData.WalkingSpeed).SetEase(Ease.Linear);
            unitSpriteRenderer.flipX = false;
        }

        private void StartReturningAnimation() {
            transform.DOMove(_startPos, (float) _transportingUnitData.WalkingSpeed).SetEase(Ease.Linear);
            unitSpriteRenderer.flipX = true;
        }

    }
}
