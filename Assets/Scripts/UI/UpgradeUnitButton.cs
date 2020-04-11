using IdleTransport.GameCore.Models;

namespace IdleTransport.UI {
    public class UpgradeUnitButton : ButtonWithText {
        private UnitData _unitData;

        public void InitButton(UnitData unitData) {
            _unitData = unitData;
            _unitData.OnUnitUpgraded += RefreshButtonText;
            RefreshButtonText();
        }

        private void RefreshButtonText() {
            SetButtonText(_unitData.UpgradeLevel.ToString());
        }
    }
}
