using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTransport.UI
{
    public class StatInfoRowUI : MonoBehaviour {
        [SerializeField] private Image _statIconImage;
        [SerializeField] private TextMeshProUGUI _statNameText;
        [SerializeField] private TextMeshProUGUI _statValueText;
        [SerializeField] private TextMeshProUGUI _statUpgradeBonusText;
    }
}
