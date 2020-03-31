using IdleTransport.GameCore.Models;
using IdleTransport.UI.Buildings;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public abstract class UnitView : MonoBehaviour {
        [SerializeField] protected SpriteRenderer unitSpriteRenderer;
        [SerializeField] private CapacityStatusUI _capacityStatusUI;

        protected UnitData unitData;

        private void Start() {
            Init();
        }

        protected virtual void Init() {
            _capacityStatusUI.Init(unitData);
        }
    }
}
