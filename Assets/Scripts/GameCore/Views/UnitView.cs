using IdleTransport.ExtensionsMethods;
using IdleTransport.GameCore.Models;
using IdleTransport.UI;
using IdleTransport.UI.Buildings;
using UnityEngine;

namespace IdleTransport.GameCore.Views {
    public abstract class UnitView : MonoBehaviour {
        [SerializeField] protected SpriteRenderer unitSpriteRenderer;
        [SerializeField] private CapacityStatusUI _capacityStatusUI;
        [SerializeField] private UpgradeUnitButton _upgradeUnitButton;

        protected UnitData unitData;

        private void Start() {
            Init();
        }

        protected virtual void Init() {
            _capacityStatusUI.Init(unitData);
            _upgradeUnitButton.Button.RegisterButton(ShowUpgradeWindow);
            _upgradeUnitButton.InitButton(unitData);
        }

        private void ShowUpgradeWindow() {
            UpgradeUnitWindow.Instance.ShowWindow(unitData);
        }
    }
}
