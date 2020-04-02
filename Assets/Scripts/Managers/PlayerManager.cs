using System;
using IdleTransport.GameCore.Models;
using IdleTransport.Utilities;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTransport.Managers {
    public class PlayerManager : Singleton<PlayerManager> {
        public static event Action OnPlayerLoaded;

        private readonly string _strPassword = "6yw>BuTD5p]zLhR{";
        private readonly string _strSalt = "zb]W!ep($7EAwcJ#";

       [ShowInInspector] private Player _player;

        public Player Player {
            get {
                if (_player == null) {
                    LoadSavedPlayer();
                }

                return _player;
            }
        }

        protected override void Awake() {
            base.Awake();
            if (Instance == this) {
                ZPlayerPrefs.Initialize(_strPassword, _strSalt);
                Application.targetFrameRate = 60;
                LoadSavedPlayer();
                SetupEvents();
            }
        }

        private void LoadSavedPlayer() {
            _player = null;
            if (ZPlayerPrefs.HasKey(Constants.SAVE_PLAYER_KEY)) {
                var playerJSON = JsonConvert.DeserializeObject<PlayerJSON>(
                    ZPlayerPrefs.GetString(Constants.SAVE_PLAYER_KEY),
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                _player = new Player(playerJSON);
            } else {
                _player = new Player();
            }
            OnPlayerLoaded?.Invoke();
        }

        private void SetupEvents() {
            //TODO: Load player's events
        }
    }
}
