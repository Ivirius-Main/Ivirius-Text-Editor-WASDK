using Microsoft.UI.Xaml;

namespace IviriusTextEditor.Core.Helpers
{
    public static class ResourceHelper
    {
        public static string ResourceString(string key)
        {
            return (string)Application.Current.Resources[key];
        }

        public static object Resource(string key)
        {
            return Application.Current.Resources[key];
        }
    }
}
