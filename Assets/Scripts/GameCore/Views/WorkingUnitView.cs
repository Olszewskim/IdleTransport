using IdleTransport.GameCore.Models;
using IdleTransport.UI.Buildings;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public abstract class WorkingUnitView : MonoBehaviour {
        [SerializeField] private WorkingStatusUI workingStatusUi;
        protected WorkingUnitData workingUnitData;

        private void Start() {
            Init();
        }

        protected virtual void Init() {
            workingStatusUi.Init(workingUnitData);
        }

        private void Update() {
            workingUnitData.UpdateUnit(Time.deltaTime);
        }
    }
}
