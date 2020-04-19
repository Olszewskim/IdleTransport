using IdleTransport.Managers;
using UnityEngine;

public class EditorPlayModeQuitWatcher : MonoBehaviour {
    private void Awake() {
#if !UNITY_EDITOR
        Destroy(gameObject);
#endif
    }

    void OnApplicationQuit() {
        PlayerManager.Instance.SavePlayerData();
    }
}
