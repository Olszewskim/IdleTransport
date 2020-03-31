using IdleTransport.GameCore.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BigInteger = IdleTransport.Utilities.BigInteger;

namespace IdleTransport.UI.Buildings {
    public class WorkingStatusUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _capacityMeterText;
        [SerializeField] private Slider _productionMeterUISlider;

        public void Init(WorkingUnitData workingUnitData) {
            InitProductionMeterUISlider();
            workingUnitData.OnCapacityStatusChanged += RefreshCapacityMeter;
            workingUnitData.OnProgressUpdated += RefreshProductionMeterUISlider;
            workingUnitData.OnUnitWorkingStateChanged += ChangeProductionMeterUISliderVisibility;
            RefreshCapacityMeter(workingUnitData.CurrentCargoAmount, workingUnitData.Capacity);
            RefreshProductionMeterUISlider(workingUnitData.CurrentProductionProgress);
            ChangeProductionMeterUISliderVisibility(workingUnitData.IsWorking());
        }

        private void InitProductionMeterUISlider() {
            _productionMeterUISlider.minValue = _productionMeterUISlider.value = 0;
            _productionMeterUISlider.maxValue = 1;
        }

        private void RefreshCapacityMeter(BigInteger currentCapacity, BigInteger maxCapacity) {
            _capacityMeterText.text = $"{currentCapacity}/{maxCapacity}";
        }

        private void RefreshProductionMeterUISlider(double progress) {
            _productionMeterUISlider.value = (float) progress;
        }

        private void ChangeProductionMeterUISliderVisibility(bool isWorking) {
            _productionMeterUISlider.gameObject.SetActive(isWorking);
        }
    }
}
