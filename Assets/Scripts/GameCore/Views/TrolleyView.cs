using GameCore.Models;
using IdleTransport.Managers;
using UnityEngine;

namespace GameCore.Views
{
    public class TrolleyView : MonoBehaviour
    {
        private TrolleyData _trolleyData;

        private void Start() {
            _trolleyData = PlayerManager.Instance.Player.TrolleyData;
        }

        private void Update() {
            _trolleyData.UpdateUnit(Time.deltaTime);
        }
    }
}
