using IdleTransport.GameCore.Models;
using UnityEngine;

namespace IdleTransport.GameCore.Views
{
    public class LoadingRampView : MonoBehaviour {
        [SerializeField] private TruckView _truckView;
        [SerializeField] private LoaderView _loaderView;
        [SerializeField] private Transform _elevatorLandingPoint;
        public void InitView(LoadingRampData loadingRampData, Vector3 position) {
            _truckView.Init(loadingRampData.TruckData);
            _loaderView.Init(loadingRampData.LoaderData);
            transform.position = position;
            gameObject.SetActive(true);
        }

        public Transform GetElevatorLandingPoint() {
            return _elevatorLandingPoint;
        }

    }
}
