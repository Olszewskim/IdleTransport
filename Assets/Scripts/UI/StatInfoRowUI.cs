using IdleTransport.Databases;
using IdleTransport.GameCore.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTransport.UI {
    public class StatInfoRowUI : MonoBehaviour {
        [SerializeField] private Image _statIconImage;
        [SerializeField] private TextMeshProUGUI _statNameText;
        [SerializeField] private TextMeshProUGUI _statValueText;
        [SerializeField] private TextMeshProUGUI _statUpgradeBonusText;

        public void InitView(StatInfo statInfo) {
            _statIconImage.sprite = GameResourcesDatabase.GetStatIcon(statInfo.StatType);
            _statNameText.text = GameTexts.GetStatName(statInfo.StatType);
            _statValueText.text = statInfo.StatValue;
            _statUpgradeBonusText.text = statInfo.StatValueAfterUpgradeBonus;
            gameObject.SetActive(true);
        }
    }
}
