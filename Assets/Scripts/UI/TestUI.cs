using IdleTransport.GameCore.Currencies;
using IdleTransport.Managers;
using IdleTransport.Utilities;
using TMPro;
using UnityConstants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdleTransport.UI {
    public class TestUI : Singleton<TestUI> {
        private bool _isVisible;
        [SerializeField] private TextMeshProUGUI _switchBtnTxt;
        [SerializeField] private GameObject _testPanel;

        public void AddGold(string amount) {
            var gold = new Gold(new BigInteger(amount));
            PlayerManager.Instance.AddCurrency(gold);
        }

        public void SetTimeScale(int scale) {
            Time.timeScale = scale;
        }

        public void ChangeVisibilityStatus() {
            _isVisible = !_isVisible;
            _testPanel.SetActive(_isVisible);
            _switchBtnTxt.text = _isVisible ? "TestPanel OFF" : "TestPanel ON";
        }

        public void DeleteSave() {
            PlayerPrefs.DeleteKey(Constants.SAVE_PLAYER_KEY);
            PlayerManager.Instance.LoadSavedPlayer();
            ChangeVisibilityStatus();
            SceneManager.LoadSceneAsync(Scenes.Main);
        }

        protected override void Awake() {
            base.Awake();
            _testPanel.SetActive(false);
        }
    }
}
