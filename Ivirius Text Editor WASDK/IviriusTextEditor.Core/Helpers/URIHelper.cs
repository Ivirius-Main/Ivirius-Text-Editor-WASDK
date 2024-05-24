using System;
using Microsoft.Windows.System;
using Windows.System;

namespace IviriusTextEditor.Core.Helpers
{
    public static class URIHelper
    {
        public static async void LaunchURI(string destination)
        {
            await Launcher.LaunchUriAsync(new Uri(destination));
        }
    }
}
