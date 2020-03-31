using IdleTransport.GameCore.Models;
using IdleTransport.UI.Buildings;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public abstract class UnitView : MonoBehaviour {
        [SerializeField] private CapacityStatusUI capacityStatusUI;
        protected UnitData unitData;

        private void Start() {
            Init();
        }

        protected virtual void Init() {
            capacityStatusUI.Init(unitData);
        }
    }
}
