using IdleTransport.GameCore.Models;
using IdleTransport.UI.Buildings;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public abstract class WorkingUnitView : UnitView {
        [SerializeField] private WorkingStatusUI workingStatusUI;

        private WorkingUnitData _workingUnitData;

        protected override void Init() {
            _workingUnitData = unitData as WorkingUnitData;
            base.Init();
            workingStatusUI.Init(_workingUnitData);
        }

        private void Update() {
            _workingUnitData.UpdateUnit(Time.deltaTime);
        }
    }
}
