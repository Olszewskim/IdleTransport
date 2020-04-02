using IdleTransport.GameCore.Models;
using UnityEngine;

namespace IdleTransport.GameCore.Views
{
    public class LoadingRampView : MonoBehaviour {
        [SerializeField] private TruckView _truckView;
        [SerializeField] private LoaderView _loaderView;

        public void InitView(LoadingRampData loadingRampData, Vector3 position) {
            _truckView.Init(loadingRampData.TruckData);
            _loaderView.Init(loadingRampData.LoaderData);
            transform.position = position;
            gameObject.SetActive(true);
        }

    }
}
