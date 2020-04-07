namespace IdleTransport.UI
{
    public class PopupsCanvas : InitChildrenAtStart {

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
