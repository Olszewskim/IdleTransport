namespace IdleTransport.UI
{
    public class OKPopupWindow : PopupWindow<OKPopupWindow> {

        protected override void ForceCloseWindow() {
            ConfirmAndCloseWindow();
        }
    }
}
