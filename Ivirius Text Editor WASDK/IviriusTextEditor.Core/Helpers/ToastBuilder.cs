using CommunityToolkit.WinUI.Notifications;

namespace IviriusTextEditor.Core.Helpers
{
    static class ToastBuilder
    {
        public static void BuildToastForFileSave(string textFilePath)
        {
            new ToastContentBuilder()
            .SetToastScenario(ToastScenario.Reminder)
            .AddText($"Your file has been succesfully saved at {textFilePath}")
            .AddButton(new ToastButton()
            .SetDismissActivation().SetContent("Close"))
            .Show();
        }
    }
}
