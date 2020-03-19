using IdleTransport.GameCore.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BigInteger = IdleTransport.Utilities.BigInteger;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.UI.Buildings {
    public class BuildingProductionUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _capacityMeterText;
        [SerializeField] private Slider _productionMeterUISlider;

        public void Init(WarehouseData warehouseData) {
            InitProductionMeterUISlider();
            warehouseData.OnProductionCompleted += RefreshCapacityMeter;
            warehouseData.OnProgressUpdated += RefreshProductionMeterUISlider;
            warehouseData.OnBuildingWorkingStateChanged += ChangeProductionMeterUISliderVisibility;
            RefreshCapacityMeter(warehouseData.CurrentCargoAmount, warehouseData.Capacity);
            RefreshProductionMeterUISlider(warehouseData.CurrentProductionProgress);
            ChangeProductionMeterUISliderVisibility(warehouseData.CurrentWorkingState);
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

        private void ChangeProductionMeterUISliderVisibility(BuildingWorkingState buildingWorkingState) {
            _productionMeterUISlider.gameObject.SetActive(buildingWorkingState == BuildingWorkingState.Working);
        }
    }
}
