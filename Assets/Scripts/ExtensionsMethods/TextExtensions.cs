using TMPro;

namespace IdleTransport.ExtensionsMethods
{
    public static class TextExtensions {


        public static bool IsNullOrEmpty(this string text) {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrEmpty(this TextMeshProUGUI text) {
            return string.IsNullOrEmpty(text.text);
        }
    }
}
