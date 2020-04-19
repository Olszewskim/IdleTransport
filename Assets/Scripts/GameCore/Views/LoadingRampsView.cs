using System.Collections.Generic;
using IdleTransport.Managers;
using UnityEngine;

namespace IdleTransport.GameCore.Views
{
    public class LoadingRampsView : MonoBehaviour {
        private const float SPACE_BETWEEN_RAMP_VIEWS = -1.7f;
        [SerializeField] private LoadingRampView _loadingRampViewPrefab;

        private readonly List<LoadingRampView> _loadingRampViewsList = new List<LoadingRampView>();

        private void Awake() {
            _loadingRampViewPrefab.gameObject.SetActive(false);
        }

        private void Start() {
            InitView();
        }

        private void InitView() {
            var loadingRampsData = PlayerManager.Instance.Player.FactoryData.LoadingRampsManager.LoadingRampDataList;
            for (int i = 0; i < loadingRampsData.Count; i++) {
                if (i >= _loadingRampViewsList.Count) {
                    _loadingRampViewsList.Add(Instantiate(_loadingRampViewPrefab, transform));
                }

                _loadingRampViewsList[i].InitView(loadingRampsData[i], GetNextRampPosition(i));
            }
        }

        private Vector3 GetNextRampPosition(int rampIndex) {
            return _loadingRampViewPrefab.transform.position + rampIndex * Vector3.up * SPACE_BETWEEN_RAMP_VIEWS;
        }

        private void OnDestroy() {
            PlayerManager.OnPlayerLoaded -= InitView;
        }

        public Transform GetLoadingRampElevatorLandingPoint(int currentDestinationIndex) {
            return _loadingRampViewsList[currentDestinationIndex].GetElevatorLandingPoint();
        }
    }
}
