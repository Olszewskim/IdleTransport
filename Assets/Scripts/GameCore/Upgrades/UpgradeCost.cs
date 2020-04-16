using System;
using IdleTransport.Utilities;
using UnityEngine;

namespace IdleTransport.GameCore.Upgrades {
    [Serializable]
    public class UpgradeCost {
        [SerializeField] private string _baseCostString;
        public double costMultiplier;
        private BigInteger _baseCost;

        public BigInteger GetBaseCost() {
            if (_baseCost != null) {
                return _baseCost;
            }

            try {
                _baseCost = new BigInteger(_baseCostString);
                return _baseCost;
            } catch (Exception e) {
                Debug.LogError(e);
                _baseCost = new BigInteger();
                return _baseCost;
            }
        }
    }
}
