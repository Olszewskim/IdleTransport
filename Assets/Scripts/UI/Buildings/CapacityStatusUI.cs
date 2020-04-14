using IdleTransport.GameCore.Models;
using IdleTransport.Utilities;
using TMPro;
using UnityEngine;

namespace IdleTransport.UI.Buildings {
    public class CapacityStatusUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _capacityMeterText;

        public void Init(UnitData unitData) {
            unitData.OnCapacityStatusChanged += RefreshCapacityMeter;
            RefreshCapacityMeter(unitData.CurrentCargoAmount, unitData.Capacity);
        }

        private void RefreshCapacityMeter(BigInteger currentCapacity, BigInteger maxCapacity) {
            _capacityMeterText.text = $"{currentCapacity.FormatHugeNumber()}/{maxCapacity.FormatHugeNumber()}";
        }
    }
}
