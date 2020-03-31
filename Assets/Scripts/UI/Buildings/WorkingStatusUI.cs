using IdleTransport.GameCore.Models;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTransport.UI.Buildings {
    public class WorkingStatusUI : MonoBehaviour {
        [SerializeField] private Slider _productionMeterUISlider;

        public void Init(WorkingUnitData workingUnitData) {
            InitProductionMeterUISlider();
            workingUnitData.OnProgressUpdated += RefreshProductionMeterUISlider;
            workingUnitData.OnUnitWorkingStateChanged += ChangeProductionMeterUISliderVisibility;
            RefreshProductionMeterUISlider(workingUnitData.CurrentProductionProgress);
            ChangeProductionMeterUISliderVisibility(workingUnitData.IsWorking());
        }

        private void InitProductionMeterUISlider() {
            _productionMeterUISlider.minValue = _productionMeterUISlider.value = 0;
            _productionMeterUISlider.maxValue = 1;
        }

        private void RefreshProductionMeterUISlider(double progress) {
            _productionMeterUISlider.value = (float) progress;
        }

        private void ChangeProductionMeterUISliderVisibility(bool isWorking) {
            _productionMeterUISlider.gameObject.SetActive(isWorking);
        }
    }
}
