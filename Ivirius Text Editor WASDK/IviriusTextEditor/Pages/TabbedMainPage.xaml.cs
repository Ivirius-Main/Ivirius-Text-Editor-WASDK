//MicaForUWP
using IviriusTextEditor.Core.Helpers;
using IviriusTextEditor.Languages;
//using MicaForUWP.Media;
//Microsoft toolkit usings
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
//using Microsoft.Graphics.Canvas.Text;
using CommunityToolkit.WinUI.Helpers;
using CommunityToolkit.WinUI.Notifications;
using CommunityToolkit.WinUI.UI.Helpers;

//WinUI namespaces
using Microsoft.UI.Xaml.Controls;
//System data usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//Windows components usings
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Devices.Input;
using Windows.Media.SpeechSynthesis;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Microsoft.Windows.System;
using Windows.UI;
using Windows.UI.Input.Preview.Injection;
using Microsoft.UI.Text;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
//Clarification namespaces and classes
using Color = Windows.UI.Color;
using ColorChangedEventArgs = Microsoft.UI.Xaml.Controls.ColorChangedEventArgs;
//using ColorHelper = CommunityToolkit.WinUI.Helpers.ColorHelper;
//using Frame = Microsoft.UI.Xaml.Controls.Frame;
using Run = Microsoft.UI.Xaml.Documents.Run;
using Microsoft.Graphics.Canvas.Text;
using Ivirius_Text_Editor;
using Windows.System;

namespace IviriusTextEditor.Pages
{
    public sealed partial class TabbedMainPage : Page
    {
        readonly DispatcherTimer DT = new();
        readonly DispatcherTimer DTSave = new();
        public bool IsCloseRequestComplete = false;
        string RestartArgs;

        #region Page

        public void LoadApp()
        {
            var DTSize = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, (int)1.2)
            };
            DTSize.Tick += DTSize_Tick;
            DTSize.Start();
            void DTSize_Tick(object Sender, object E)
            {
                DTSize.Stop();
                LoadingScreen.Visibility = Visibility.Collapsed;
                REBOpenAnimation.Begin();
            }
        }

        public TabbedMainPage()
        {
            #region Miscellaneous

            StringTable.ReadLanguage();

            //Component initialization
            InitializeComponent();

            //Settings
            RequestedTheme = ElementTheme.Light;
            DTSave.Interval = new TimeSpan(0, 0, (int)1.5);
            DTSave.Tick += DTSave_Tick;
            AppCenter.Start("Debugging", typeof(Crashes));

            //Special methods
            TriggerCrashDetection();
            RunEditConfig();

            LoadApp();

            //Variables
            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            //ApplicationViewTitleBar AppTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            //CoreApplicationViewTitleBar AppCoreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            #endregion Miscellaneous

            this.SizeChanged += WindowSizeChanged;

            #region Settings

            //Theme settings
            if (!(LocalSettings.Values["Theme"] == null))
            {
                //Custom theme
                if ((string)LocalSettings.Values["Theme"] == "Custom")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Light");
                }

                //Light theme
                if ((string)LocalSettings.Values["Theme"] == "Light")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Light");
                }

                //Dark theme
                if ((string)LocalSettings.Values["Theme"] == "Dark")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Dark");
                }

                //Acrylic theme
                if ((string)LocalSettings.Values["Theme"] == "Nostalgic Windows")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Light");
                }
                if ((string)LocalSettings.Values["Theme"] == "Acrylic")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Light");
                }
                if ((string)LocalSettings.Values["Theme"] == "Luna")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Light");
                }
                if ((string)LocalSettings.Values["Theme"] == "Old")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Light");
                }
                if ((string)LocalSettings.Values["Theme"] == "Full Dark")
                {
                    SettingsHelper.SetSetting("Theme", "Mica Dark");
                }

                //Mica Light theme
                if ((string)LocalSettings.Values["Theme"] == "Mica Light")
                {
                    //Final settings
                    RequestedTheme = ElementTheme.Light;

                    StatusBar.Background = null;

                    //Flat UI settings
                    if (!(LocalSettings.Values["FlatUI"] == null))
                    {
                        if ((string)LocalSettings.Values["FlatUI"] == "On")
                        {
                            REBPanel.Background = new SolidColorBrush()
                            {
                                Color = Color.FromArgb(255, 230, 230, 230)
                            };
                        }
                        if ((string)LocalSettings.Values["FlatUI"] == "Off")
                        {

                        }
                    }
                    else
                    {
                        LocalSettings.Values["FlatUI"] = "Off";
                    }
                }

                if ((string)LocalSettings.Values["Theme"] == "Mica Dark")
                {
                    RequestedTheme = ElementTheme.Dark;
                    AeroBlue.Visibility = Visibility.Collapsed;

                    StatusBar.Background = null;

                    //Flat UI settings
                    if (!(LocalSettings.Values["FlatUI"] == null))
                    {
                        if ((string)LocalSettings.Values["FlatUI"] == "On")
                        {
                            REBPanel.Background = new SolidColorBrush()
                            {
                                Color = Color.FromArgb(255, 30, 30, 30)
                            };
                        }
                        if ((string)LocalSettings.Values["FlatUI"] == "Off")
                        {

                        }
                    }
                    else
                    {
                        LocalSettings.Values["FlatUI"] = "Off";
                    }
                }
            }
            else
            {
                var TL = new CommunityToolkit.WinUI.Helpers.ThemeListener();
                if (TL.CurrentTheme == ApplicationTheme.Light)
                {
                    LocalSettings.Values["Theme"] = "Mica Light";
                    RequestedTheme = ElementTheme.Light;
                    AeroBlue.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LocalSettings.Values["Theme"] = "Mica Dark";
                    RequestedTheme = ElementTheme.Dark;
                    AeroBlue.Visibility = Visibility.Collapsed;
                }
            }

            //Accent border settings
            if (!(LocalSettings.Values["Autosave"] == null))
            {
                if ((string)LocalSettings.Values["Autosave"] == "On")
                {
                    AutoSaveSwitch.IsOn = true;
                }
                if ((string)LocalSettings.Values["Autosave"] == "Off")
                {
                    AutoSaveSwitch.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Autosave"] = "On";
                AutoSaveSwitch.IsOn = true;
            }

            //Dev settings
            if (!(LocalSettings.Values["DEV"] == null))
            {
                if ((string)LocalSettings.Values["DEV"] == "On")
                {

                }
                if ((string)LocalSettings.Values["DEV"] == "Off")
                {

                }
            }
            else
            {
                LocalSettings.Values["DEV"] = "Off";
            }

            //Changelog settings
            if (!(LocalSettings.Values["Changelog"] == null))
            {
                if ((string)LocalSettings.Values["Changelog"] == "On")
                {
                    ChangelogButton.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Changelog"] == "Off")
                {
                    ChangelogButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Changelog"] = "On";
                ChangelogButton.Visibility = Visibility.Visible;
            }

            //Cut settings
            if (!(LocalSettings.Values["Cut"] == null))
            {
                if ((string)LocalSettings.Values["Cut"] == "On")
                {
                    CTBBar.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Cut"] == "Off")
                {
                    CTBBar.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Cut"] = "On";
                CTBBar.Visibility = Visibility.Visible;
            }

            //Copy settings
            if (!(LocalSettings.Values["Copy"] == null))
            {
                if ((string)LocalSettings.Values["Copy"] == "On")
                {
                    CBBar.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Copy"] == "Off")
                {
                    CBBar.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Copy"] = "On";
                CBBar.Visibility = Visibility.Visible;
            }

            //Paste settings
            if (!(LocalSettings.Values["Paste"] == null))
            {
                if ((string)LocalSettings.Values["Paste"] == "On")
                {
                    PBBar.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Paste"] == "Off")
                {
                    PBBar.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Paste"] = "On";
                PBBar.Visibility = Visibility.Visible;
            }

            //New settings
            if (LocalSettings.Values["New"] != null)
            {
                if ((string)LocalSettings.Values["New"] == "On")
                {
                    TBNewB.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["New"] == "Off")
                {
                    TBNewB.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["New"] = "Off";
                TBNewB.Visibility = Visibility.Collapsed;
            }

            //Open settings
            if (LocalSettings.Values["Open"] != null)
            {
                if ((string)LocalSettings.Values["Open"] == "On")
                {
                    TBOpenB.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Open"] == "Off")
                {
                    TBOpenB.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Open"] = "Off";
                TBOpenB.Visibility = Visibility.Collapsed;
            }

            //Print settings
            if (LocalSettings.Values["Print"] != null)
            {
                if ((string)LocalSettings.Values["Print"] == "On")
                {
                    TBPrintB.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Print"] == "Off")
                {
                    TBPrintB.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Print"] = "Off";
                TBPrintB.Visibility = Visibility.Collapsed;
            }

            //Delete settings
            if (!(LocalSettings.Values["Delete"] == null))
            {
                if ((string)LocalSettings.Values["Delete"] == "On")
                {
                    DelBBar.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Delete"] == "Off")
                {
                    DelBBar.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Delete"] = "On";
                DelBBar.Visibility = Visibility.Visible;
            }

            //Home page settings
            if (!(LocalSettings.Values["HomePage"] == null))
            {
                if ((string)LocalSettings.Values["HomePage"] == "On")
                {
                    HomePage.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["HomePage"] == "Off")
                {
                    HomePage.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["HomePage"] = "Off";
                HomePage.Visibility = Visibility.Collapsed;
            }

            //Ruler settings
            if (!(LocalSettings.Values["Ruler"] == null))
            {
                if ((string)LocalSettings.Values["Ruler"] == "On")
                {
                    SCR3.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["Ruler"] == "Off")
                {
                    SCR3.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["Ruler"] = "On";
                SCR3.Visibility = Visibility.Visible;
            }

            //Toolbar settings
            if (!(LocalSettings.Values["Toolbar"] == null))
            {
                if ((string)LocalSettings.Values["Toolbar"] == "On")
                {
                    Bor.Opacity = 100;
                }
                if ((string)LocalSettings.Values["Toolbar"] == "Off")
                {
                    Bor.Opacity = 0;
                }
            }
            else
            {
                LocalSettings.Values["Toolbar"] = "On";
                Bor.Opacity = 100;
            }

            //Status bar settings
            if (!(LocalSettings.Values["StatusBar"] == null))
            {
                if ((string)LocalSettings.Values["StatusBar"] == "On")
                {
                    StatusBar.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["StatusBar"] == "Off")
                {
                    StatusBar.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["StatusBar"] = "On";
                StatusBar.Visibility = Visibility.Visible;
            }

            //News settings
            if (!(LocalSettings.Values["News"] == null))
            {
                if ((string)LocalSettings.Values["News"] == "On")
                {
                    NewsItem.Visibility = Visibility.Visible;
                    StoreItem.Visibility = Visibility.Visible;
                    WebsiteItem.Visibility = Visibility.Visible;
                    BugItem.Visibility = Visibility.Visible;
                    YoutubeItem.Visibility = Visibility.Visible;
                }
                if ((string)LocalSettings.Values["News"] == "Off")
                {
                    NewsItem.Visibility = Visibility.Collapsed;
                    StoreItem.Visibility = Visibility.Collapsed;
                    WebsiteItem.Visibility = Visibility.Collapsed;
                    BugItem.Visibility = Visibility.Collapsed;
                    YoutubeItem.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                LocalSettings.Values["News"] = "On";
                NewsItem.Visibility = Visibility.Visible;
                StoreItem.Visibility = Visibility.Visible;
                WebsiteItem.Visibility = Visibility.Visible;
                BugItem.Visibility = Visibility.Visible;
                YoutubeItem.Visibility = Visibility.Visible;
            }

            //Flat UI settings
            if (!(LocalSettings.Values["FlatUI"] == null))
            {
                if ((string)LocalSettings.Values["FlatUI"] == "On")
                {
                    //REB.ClearValue(RequestedThemeProperty);
                    //RTB.ClearValue(RequestedThemeProperty);
                    //EmptyRTB.ClearValue(RequestedThemeProperty);
                    //REB.Document.Selection.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.White;
                    //RTB.Document.Selection.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.White;
                    //EmptyRTB.Document.Selection.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.White;
                }
                if ((string)LocalSettings.Values["FlatUI"] == "Off")
                {
                    //REB.Background = new SolidColorBrush(Microsoft.UI.Colors.White);
                    //RTB.Background = new SolidColorBrush(Microsoft.UI.Colors.White);
                    //EmptyRTB.Background = new SolidColorBrush(Microsoft.UI.Colors.White);
                }
            }
            else
            {


            }

            //Spell check settings
            if ((string)LocalSettings.Values["SCheck"] == "On")
            {
                REB.IsSpellCheckEnabled = true;
                string Y = GetText(REB);
                //if (T != Y)
                //{
                TextSetOptions Options = TextSetOptions.FormatRtf | TextSetOptions.ApplyRtfDocumentDefaults;
                REB.Document.GetText(TextGetOptions.FormatRtf, out _);
                //REB.Document.SetText(Options, T);
                REB.Document.SetText(Options, string.Empty);
                REB.Document.SetText(Options, Y);
                //}
                //else
                //{

                //}
            }
            else
            {
                REB.IsSpellCheckEnabled = false;
                string Y = GetText(REB);
                //if (T != Y)
                //{
                TextSetOptions Options = TextSetOptions.FormatRtf | TextSetOptions.ApplyRtfDocumentDefaults;
                REB.Document.GetText(TextGetOptions.FormatRtf, out _);
                //REB.Document.SetText(Options, T);
                REB.Document.SetText(Options, string.Empty);
                REB.Document.SetText(Options, Y);
                //}
                //else
                //{

                //}
            }

            #endregion Settings

            #region Components

            //Document config
            REB.Document.Selection.CharacterFormat.Size = (float)10.5;
            RTB.Document.Selection.CharacterFormat.Size = (float)10.5;
            BackPicker.Color = Color.FromArgb(0, 255, 255, 255);

            //Title bar config
            //AppCoreTitleBar.ExtendViewIntoTitleBar = true;

            //Fonts config
            var Fonts = CanvasTextFormat.GetSystemFontFamilies().OrderBy(Font => Font).ToList();
            FontBox.ItemsSource = Fonts;

            //Size config
            SizeBox.Items.Add("A4");
            SizeBox.Items.Add("Letter");
            SizeBox.Items.Add("Tabloid");
            SizeBox.SelectedItem = "A4";

            #endregion Components
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if ((App.Window.Content).ActualWidth < 900)
            //{
            //    PlusButton.Visibility = Visibility.Collapsed;
            //} else
            //{
            //    PlusButton.Visibility = Visibility.Visible;
            //}
        }

        #region Crashes

        public async void TriggerCrashDetection()
        {
            bool Crash = await Crashes.HasCrashedInLastSessionAsync();
            if (Crash == true)
            {
                try
                {
                    ErrorReport Report = await Crashes.GetLastSessionCrashReportAsync();
                    if (Report != null && Report.StackTrace != null)
                    {
                        new ToastContentBuilder()
                        .SetToastScenario(ToastScenario.Reminder)
                        .AddText("It seems like Ivirius Text Editor has crashed.")
                        .AddText("These are the details:")
                        .AddText($"{Report.StackTrace}")
                        .AddButton(new ToastButton()
                            .SetDismissActivation().SetContent("Close"))
                        .AddButton(new ToastButton()
                        .SetProtocolActivation(new Uri("https://ivirius.vercel.app/contact/")).SetContent("Send bug report"))
                        .Show();
                    }
                    else
                    {
                        new ToastContentBuilder()
                        .SetToastScenario(ToastScenario.Reminder)
                        .AddText("It seems like Ivirius Text Editor has crashed.")
                        .AddText("We can't show any information right now")
                        .AddButton(new ToastButton()
                            .SetDismissActivation().SetContent("Close"))
                        .AddButton(new ToastButton()
                        .SetProtocolActivation(new Uri("https://ivirius.vercel.app/contact/")).SetContent("Send bug report"))
                        .Show();
                    }
                }
                catch
                {
                    try
                    {
                        new ToastContentBuilder()
                        .SetToastScenario(ToastScenario.Reminder)
                        .AddText("It seems like Ivirius Text Editor has crashed.")
                        .AddText("We can't show any information right now")
                        .AddButton(new ToastButton()
                            .SetDismissActivation().SetContent("Close"))
                        .AddButton(new ToastButton()
                        .SetProtocolActivation(new Uri("https://ivirius.vercel.app/contact/")).SetContent("Send bug report"))
                        .Show();
                    }
                    catch
                    {

                    }
                }
            }
            else
            {

            }
        }

        #endregion Crashes

        #endregion Page

        #region File

        public StorageFile TXTFile;

        public IRandomAccessStream RAS;

        private readonly PrintHelperOptions PP = new PrintHelperOptions();

        public bool IsRTF;

        #region Buttons

        private async void SB2_Click(object sender, RoutedEventArgs e)
        {
            if (TXTFile != null)
            {
                if (TXTFile == null)
                {
                    FileNotSavedInfoBar.Title = "There is no file associated with this document";
                    FileNotSavedInfoBar.IsOpen = true;
                }
                else if (!(TXTFile == null))
                {
                    try
                    {
                        IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite);
                        REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                        RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                        RAS.Dispose();
                        FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                        if (Stats == FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            new ToastContentBuilder()
                            .SetToastScenario(ToastScenario.Reminder)
                            .AddText($"Your file has been succesfully saved at {TXTFile.Path}")
                            .AddButton(new ToastButton()
                                .SetDismissActivation().SetContent("Close"))
                            .Show();
                            CheckForSaving();
                        }
                        if (Stats != FileUpdateStatus.Complete)
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "File couldn't be saved";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        FileNotSavedInfoBar.Title = "The file is currently in use either by either the autosave option or another app";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
            }
            else
            {
                ActionErrorMessage.Text = "There is no file associated with this document";
            }
        }

        private void AutoSaveSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer LS = ApplicationData.Current.LocalSettings;
            if (AutoSaveSwitch != null)
            {
                if (AutoSaveSwitch.IsOn == true && AutoSaveSwitch.IsEnabled == true)
                {
                    LS.Values["Autosave"] = "On";
                }
                if (AutoSaveSwitch.IsOn == false && AutoSaveSwitch.IsEnabled == true)
                {
                    LS.Values["Autosave"] = "Off";
                }
            }
            CheckForSaving();
        }

        private async void MenuFlyoutItem_Click_24(object sender, RoutedEventArgs e)
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);
            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
            //File dialog configuration
            FSP.FileTypeChoices.Add("Plain Ivirius Text", new List<string>() { ".ivrtxt" });
            FSP.FileTypeChoices.Add("Universal Plain Text", new List<string>() { ".txt" });
            if (!(LocalSettings.Values["DEV"] == null))
            {
                if ((string)LocalSettings.Values["DEV"] == "On")
                {
                    FSP.FileTypeChoices.Add("HTML", new List<string>() { ".html" });
                    FSP.FileTypeChoices.Add("XML", new List<string>() { ".xml" });
                    FSP.FileTypeChoices.Add("CSS", new List<string>() { ".css" });
                }
                if ((string)LocalSettings.Values["DEV"] == "Off")
                {
                }
            }
            else
            {
                LocalSettings.Values["DEV"] = "Off";
            }
            FSP.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            FSP.SuggestedFileName = "New Plain Text File";
            //Set file content
            TXTFile = await FSP.PickSaveFileAsync();
            if (!(TXTFile == null))
            {
                try
                {
                    IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite);
                    REB.Document.SaveToStream(TextGetOptions.None, RAS);
                    RTB.Document.LoadFromStream(TextSetOptions.None, RAS);
                    RAS.Dispose();
                    FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                    if (Stats == FileUpdateStatus.Complete)
                    {
                        //Confirm file saving without close
                        new ToastContentBuilder()
                        .SetToastScenario(ToastScenario.Reminder)
                        .AddText($"Your file has been succesfully saved at {TXTFile.Path}")
                        .AddButton(new ToastButton()
                            .SetDismissActivation().SetContent("Close"))
                        .Show();
                        CheckForSaving();
                    }
                    if (!(Stats == FileUpdateStatus.Complete))
                    {
                        //File failed to save message
                        FileNotSavedInfoBar.Title = "File couldn't be saved";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
                catch
                {
                    FileNotSavedInfoBar.Title = "The file is currently in use either by the autosave option or another app";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                }
            }
        }

        private static FileSavePicker InitializeWithWindow(FileSavePicker obj, IntPtr windowHandle)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(obj, windowHandle);
            return obj;
        }

        private async void SNFYesToFull_Click(object Sender, RoutedEventArgs EvArgs)
        {
            await SaveFile(false, false, false, false);
        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {

        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://learn.microsoft.com/en-us/windows/apps/winui/winui2/"));
        }

        private async void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://learn.microsoft.com/en-us/windows/communitytoolkit/"));
        }

        private async void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://learn.microsoft.com/en-us/windows/uwp/get-started/universal-application-platform-guide"));
        }

        private async void HyperlinkButton_Click_3(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/Humanizr/Humanizer/blob/main/LICENSE"));
        }

        public string overrideFile = "";

        public async Task NewFile()
        {
            if (TXTFile == null)
            {
                if (FileNotSaved.Visibility == Visibility.Visible)
                {
                    NewFileBox.Open();
                }
                else
                {
                    await EraseFile();
                }
            }
            if (TXTFile != null)
            {
                if (FileNotSaved.Visibility == Visibility.Visible)
                {
                    NewFileBox.Open();
                }
                else
                {
                    await EraseFile();
                }
            }
        }

        public async Task EraseFile()
        {
            TXTFile = null;
            var TempTXT = await StorageFile.GetFileFromApplicationUriAsync(new Uri(overrideFile));
            var RAStream = await TempTXT.OpenAsync(FileAccessMode.Read);
            REB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAStream);
        }

        private async void NewFile_Click(object Sender, RoutedEventArgs EvArgs)
        {
            overrideFile = "";
            await NewFile();
        }

        private void DelB_Click(object sender, RoutedEventArgs e)
        {
            if (TXTFile != null)
            {
                if (TXTFile.IsAvailable != true)
                {
                    FileNotSavedInfoBar.Title = "There is no file available";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                }
                ActionWarningBox.Open();
                ActionWarningMessage.Text = "Are you sure you want to delete this file?";
                ActionWarningBox.FirstButtonClick += ED2_PrimaryButtonClick;
                void ED2_PrimaryButtonClick(object Sender, RoutedEventArgs EvArgs)
                {
                    try
                    {
                        if (TXTFile != null)
                        {
                            _ = TXTFile.DeleteAsync();
                            TXTFile = null;
                            CheckForSaving();
                            ActionWarningBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                        }
                        else
                        {
                            CheckForSaving();
                            new ToastContentBuilder()
                            .SetToastScenario(ToastScenario.Reminder)
                            .AddText($"Your file could not be found on this computer or is currently in use by another application")
                            .AddButton(new ToastButton()
                                .SetDismissActivation().SetContent("Close"))
                            .Show();
                            ActionWarningBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                        }
                    }
                    catch (Exception Ex)
                    {
                        try
                        {
                            new ToastContentBuilder()
                            .SetToastScenario(ToastScenario.Reminder)
                            .AddText("It seems like Ivirius Text Editor has a bug.")
                            .AddText("These are the details:")
                            .AddText($"{Ex.Message}")
                            .AddButton(new ToastButton()
                                .SetDismissActivation().SetContent("Close"))
                            .AddButton(new ToastButton()
                            .SetProtocolActivation(new Uri("https://ivirius.vercel.app/contact/")).SetContent("Send bug report"))
                            .Show();
                            CheckForSaving();
                            ActionWarningBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                        }
                        catch
                        {
                            ActionWarningBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                        }
                        return;
                    }
                }
            }
            else
            {
                FileNotSavedInfoBar.Title = "There is no file available";
                FileNotSavedInfoBar.IsOpen = true;
                CheckForSaving();
            }
        }

        private async void OB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            var Value = GetText(REB);
            var SecValue = GetText(RTB);
            if (Value == SecValue)
            {
                await Open();
            }
            else
            {
                //Remember the user to save the file
                FileSaveBox.FirstButtonClick += ED2_PrimaryButtonClick;
                FileSaveBox.SecondButtonClick += ED2_SecondaryButtonClick;
                FileSaveBox.CancelButtonClick += ED2_CloseButtonClick;
                try { FileSaveBox.Open(); } catch { }
                async void ED2_PrimaryButtonClick(object SenderSec, RoutedEventArgs DialogEvArgs)
                {
                    if (TXTFile != null) await SaveFile(true, false, false, false); else await SaveFile(false, false, false, false);
                    FileSaveBox.Close();
                    FileSaveBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                    FileSaveBox.SecondButtonClick -= ED2_SecondaryButtonClick;
                    FileSaveBox.CancelButtonClick -= ED2_CloseButtonClick;
                }
                async void ED2_SecondaryButtonClick(object SenderSec, RoutedEventArgs DialogEvArgs)
                {
                    await Open();
                    FileSaveBox.Close();
                    FileSaveBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                    FileSaveBox.SecondButtonClick -= ED2_SecondaryButtonClick;
                    FileSaveBox.CancelButtonClick -= ED2_CloseButtonClick;
                }
                void ED2_CloseButtonClick(object SenderSec, RoutedEventArgs DialogEvArgs)
                {
                    FileSaveBox.Close();
                    FileSaveBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                    FileSaveBox.SecondButtonClick -= ED2_SecondaryButtonClick;
                    FileSaveBox.CancelButtonClick -= ED2_CloseButtonClick;
                }
            }
        }

        public async void Print()
        {
            int workingComponents = 0;

            try
            {
                StorageFolder desktop = await StorageFolder.GetFolderFromPathAsync(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                StorageFile TempFile;
                try
                {
                    TempFile = await desktop.CreateFileAsync("IviriusTextEditorPrinterCachedDocument.ivrprintingservice");
                    CachedFileManager.DeferUpdates(TempFile);
                    using (IRandomAccessStream RAS = await TempFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                        if (await CachedFileManager.CompleteUpdatesAsync(TempFile) is FileUpdateStatus.Complete)
                        {
                            workingComponents++;
                        }
                        else
                        {
                            //File failed to check for saving
                            FileNotSavedInfoBar.Title = "An unexpected error occured. Please manually delete the cached printer file from your desktop and try again.";
                            FileNotSavedInfoBar.IsOpen = true;
                        }
                    }
                    if (await CachedFileManager.CompleteUpdatesAsync(TempFile) is FileUpdateStatus.Complete)
                    {
                        //Confirm file saving without close
                        CheckForSaving();
                    }
                    else
                    {
                        //File failed to save message
                        FileNotSavedInfoBar.Title = "Printer file couldn't be saved.";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }

                }
                catch
                {
                    try
                    {
                        TempFile = await desktop.GetFileAsync("IviriusTextEditorPrinterCachedDocument.ivrprintingservice");
                        await TempFile.DeleteAsync();
                        TempFile = await desktop.CreateFileAsync("IviriusTextEditorPrinterCachedDocument.ivrprintingservice");
                        CachedFileManager.DeferUpdates(TempFile);
                        using (IRandomAccessStream RAS = await TempFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TempFile) is FileUpdateStatus.Complete)
                            {
                                workingComponents++;
                            }
                            else
                            {
                                //File failed to check for saving
                                FileNotSavedInfoBar.Title = "An unexpected error occured. Please manually delete the cached printer file from your desktop and try again.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                        }
                        if (await CachedFileManager.CompleteUpdatesAsync(TempFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "Printer file couldn't be saved.";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        ActionWarningMessage.Text = "The printing file cannot be deleted. Please manually delete it from yuor desktop and try again.";
                        ActionWarningBox.Open();
                    }
                }
            }
            catch
            {
                PrintingInfoBox.Open();
                CheckForSaving();
            }

            if (workingComponents == 1)
            {
                await Launcher.LaunchUriAsync(new Uri("ivirrtf2x:///"));
            }

            else
            {
                PrintingInfoBox.Open();
            }
        }

        private void PntB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            Print();
        }

        private async void SAsB_Click(object Sender, RoutedEventArgs EvArgs) => await SaveFile(false, false, false, false);

        private async void SB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            if (TXTFile != null) await SaveFile(true, false, false, false);
            else await SaveFile(false, false, false, false);
        }

        #endregion Buttons

        #region Actions

        public async Task AutosaveWritingToFile()
        {
            try
            {
                FileSaveBox.Close();
                var RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite);
                REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                try
                {
                    RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                }
                catch (AccessViolationException)
                {
                    ActionErrorMessage.Text = "The file you are trying to edit cannot be checked for saving";
                    ActionErrorBox.Open();
                    isFileLinked = false;
                }
                RAS.Dispose();
                var Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                if (Stats == FileUpdateStatus.Complete)
                {
                    //Confirm file saving without close
                    CheckForSaving();
                }
                if (Stats != FileUpdateStatus.Complete)
                {
                    //File failed to save message
                    FileNotSavedInfoBar.Title = "File couldn't be saved";
                    FileNotSavedInfoBar.IsOpen = true;
                    isFileLinked = false;
                    CheckForSaving();
                }
                isFileLinked = true;
            }
            catch
            {
                return;
            }
        }

        private bool AutosaveDT = false;

        private async void DTSave_Tick(object sender, object e)
        {

        }

        public async Task TriggerAutosave()
        {
            if (AutoSaveSwitch.IsOn == true && TXTFile != null)
            {
                try
                {
                    await SaveFile(true, false, false, false, true);
                }
                catch (Exception EX)
                {
                    new ToastContentBuilder()
                    .SetToastScenario(ToastScenario.Reminder)
                    .AddText($"Ivirius Text Editor has a bug. These are the details:")
                    .AddText($"Debugger Output:")
                    .AddText($"Message:")
                    .AddText($"{EX.Message}")
                    .AddText($"Data:")
                    .AddText($"{EX.Data}")
                    .AddButton(new ToastButton()
                        .SetDismissActivation().SetContent("Close"))
                    .AddButton(new ToastButton()
                    .SetProtocolActivation(new Uri("https://ivirius.vercel.app/contact/")).SetContent("Send bug report"))
                    .Show();
                }
            }
            else
            {
                DTSave.Stop();
                isFileLinked = false;
            }
        }

        public async Task SaveAsLog()
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);
            //File dialog configuration
            FSP.FileTypeChoices.Add("Log File", new List<string>() { ".log" });
            FSP.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            FSP.SuggestedFileName = "New Log File";
            //Set file content
            TXTFile = await FSP.PickSaveFileAsync();
            if (!(TXTFile == null))
            {
                try
                {
                    IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite);
                    REB.Document.SaveToStream(TextGetOptions.None, RAS);
                    RTB.Document.LoadFromStream(TextSetOptions.None, RAS);
                    RAS.Dispose();
                    FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                    if (Stats == FileUpdateStatus.Complete)
                    {
                        //Confirm file saving without close
                        CheckForSaving();
                        new ToastContentBuilder()
                        .SetToastScenario(ToastScenario.Reminder)
                        .AddText($"Your file has been succesfully saved at {TXTFile.Path}")
                        .AddButton(new ToastButton()
                            .SetDismissActivation().SetContent("Close"))
                        .Show();
                    }
                    if (!(Stats == FileUpdateStatus.Complete))
                    {
                        //File failed to save message
                        FileNotSavedInfoBar.Title = "File couldn't be saved";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
                catch
                {
                    FileNotSavedInfoBar.Title = "The file is currently in use either by the autosave option or another app";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                }
            }
        }

        public void RequestClose()
        {
            var Value = GetText(REB);
            var SecValue = GetText(RTB);
            if (Value == SecValue)
            {
                IsCloseRequestComplete = true;
            }
            else
            {
                //Remember the user to save the file
                FileSaveBox.FirstButtonClick += ED2_PrimaryButtonClick;
                FileSaveBox.SecondButtonClick += ED2_SecondaryButtonClick;
                FileSaveBox.CancelButtonClick += ED2_CloseButtonClick;
                try { FileSaveBox.Open(); } catch { }
                async void ED2_PrimaryButtonClick(object SenderSec, RoutedEventArgs DialogEvArgs)
                {
                    if (TXTFile != null) await SaveFile(true, true, false, false);
                    else await SaveFile(false, true, false, false);
                    HomePage.Visibility = Visibility.Collapsed;
                    FileSaveBox.Close();
                    FileSaveBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                    FileSaveBox.SecondButtonClick -= ED2_SecondaryButtonClick;
                    FileSaveBox.CancelButtonClick -= ED2_CloseButtonClick;
                }
                void ED2_SecondaryButtonClick(object SenderSec, RoutedEventArgs DialogEvArgs)
                {
                    IsCloseRequestComplete = true;
                    FileSaveBox.Close();
                    FileSaveBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                    FileSaveBox.SecondButtonClick -= ED2_SecondaryButtonClick;
                    FileSaveBox.CancelButtonClick -= ED2_CloseButtonClick;
                }
                void ED2_CloseButtonClick(object SenderSec, RoutedEventArgs DialogEvArgs)
                {
                    FileSaveBox.Close();
                    FileSaveBox.FirstButtonClick -= ED2_PrimaryButtonClick;
                    FileSaveBox.SecondButtonClick -= ED2_SecondaryButtonClick;
                    FileSaveBox.CancelButtonClick -= ED2_CloseButtonClick;
                }
            }
            return;
        }

        public bool isWorkSaved;

        public bool isFileLinked;

        public void CheckForSaving()
        {
            if (GetText(REB) == GetText(RTB) || GetText(REB) == string.Empty)
            {
                FileNotSaved.Visibility = Visibility.Collapsed;
                isWorkSaved = true;
            }
            else
            {
                if (GetText(REB) == GetText(RTB) && TXTFile != null && ((App.Window.Content as Frame).Content as MainPage).TabbedView.SelectedItem != null)
                {
                    FileNotSaved.Visibility = Visibility.Visible;
                    isWorkSaved = false;
                    FileNameTextBlock.Text = TXTFile.DisplayName;
                    (((App.Window.Content as Frame).Content as MainPage).TabbedView.SelectedItem as TabViewItem).Header = TXTFile.DisplayName;
                }
                if (GetText(REB) != GetText(RTB) || TXTFile == null)
                {
                    FileNotSaved.Visibility = Visibility.Visible;
                    isWorkSaved = false;
                }
            }
        }

        public async Task SaveFile(bool Background, bool Close, bool Erase, bool NoFile, bool triggeredByAutosave = false)
        {
            //----------Save Background----------

            if (Background == true && Close == false && Erase == false && NoFile == false)
            {
                if (TXTFile == null)
                {
                    //Set other scenario
                    await SaveFile(false, false, false, false);
                }
                else if (TXTFile != null)
                {
                    try
                    {
                        CachedFileManager.DeferUpdates(TXTFile);
                        using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                            {
                                RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                                AutosaveRing.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                //File failed to check for saving
                                if (triggeredByAutosave == false)
                                {
                                    FileNotSavedInfoBar.Title = "An unexpected error occured while syncing your file. Please reopen the file in a new tab to proceed, and close the current tab.";
                                    FileNotSavedInfoBar.IsOpen = true;
                                }
                                else AutosaveRing.Visibility = Visibility.Visible;
                            }
                        }
                        if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            if (triggeredByAutosave == false) ToastBuilder.BuildToastForFileSave(TXTFile.Path);
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            if (triggeredByAutosave == false)
                            {
                                FileNotSavedInfoBar.Title = "The file couldn't be saved.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                            else AutosaveRing.Visibility = Visibility.Visible;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        if (triggeredByAutosave == false)
                        {
                            FileNotSavedInfoBar.Title = "The file is currently in use.";
                            FileNotSavedInfoBar.IsOpen = true;
                        }
                        else AutosaveRing.Visibility = Visibility.Visible;
                        CheckForSaving();
                    }
                }
            }

            //----------Save----------

            if (Background == false && Close == false && Erase == false && NoFile == false)
            {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
                //File dialog configuration
                var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);

                FSP.FileTypeChoices.Add("Rich Ivirius Text", new List<string>() { ".ivrtext" });
                FSP.FileTypeChoices.Add("Universal Rich Text", new List<string>() { ".rtf" });
                FSP.FileTypeChoices.Add("Rich Text", new List<string>() { ".richtxtformat" });
                FSP.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                FSP.SuggestedFileName = "New Rich Text File";

                //Set file content
                TXTFile = await FSP.PickSaveFileAsync();
                if (TXTFile != null)
                {
                    try
                    {
                        CachedFileManager.DeferUpdates(TXTFile);
                        using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                            {
                                RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                            }
                            else
                            {
                                //File failed to check for saving
                                FileNotSavedInfoBar.Title = "An unexpected error occured while syncing your file. Please reopen the file in a new tab to proceed, and close the current tab.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                        }
                        FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                        if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            ToastBuilder.BuildToastForFileSave(TXTFile.Path);
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "File couldn't be saved.";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        FileNotSavedInfoBar.Title = "The file is currently in use.";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
            }

            //----------Save Background Close----------

            if (Background == true && Close == true && Erase == false && NoFile == false)
            {
                if (TXTFile == null)
                {
                    await SaveFile(false, true, false, false);
                }
                else if (TXTFile != null)
                {
                    try
                    {
                        CachedFileManager.DeferUpdates(TXTFile);
                        using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                            {
                                RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                            }
                            else
                            {
                                //File failed to check for saving
                                FileNotSavedInfoBar.Title = "An unexpected error occured while syncing your file. Please reopen the file in a new tab to proceed, and close the current tab.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                        }
                        FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                        if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            IsCloseRequestComplete = true;
                            ToastBuilder.BuildToastForFileSave(TXTFile.Path);
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "File couldn't be saved.";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        FileNotSavedInfoBar.Title = "The file is currently in use.";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
            }

            //----------Save Close----------

            if (Background == false && Close == true && Erase == false && NoFile == false)
            {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
                var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);

                FSP.FileTypeChoices.Add("Rich Ivirius Text", new List<string>() { ".ivrtext" });
                FSP.FileTypeChoices.Add("Universal Rich Text", new List<string>() { ".rtf" });
                FSP.FileTypeChoices.Add("Rich Text", new List<string>() { ".richtxtformat" });
                FSP.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                FSP.SuggestedFileName = "New Rich Text File";

                //Set file content
                TXTFile = await FSP.PickSaveFileAsync();
                if (TXTFile != null)
                {
                    try
                    {
                        CachedFileManager.DeferUpdates(TXTFile);
                        using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                            {
                                RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                            }
                            else
                            {
                                //File failed to check for saving
                                FileNotSavedInfoBar.Title = "An unexpected error occured while syncing your file. Please reopen the file in a new tab to proceed, and close the current tab.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                        }
                        FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                        if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            IsCloseRequestComplete = true;
                            ToastBuilder.BuildToastForFileSave(TXTFile.Path);
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "File couldn't be saved.";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        FileNotSavedInfoBar.Title = "The file is currently in use.";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
            }

            //----------Save Background Erase----------

            if (Background == true && Close == false && Erase == true && NoFile == false)
            {
                if (TXTFile == null)
                {
                    await SaveFile(false, false, true, false);
                }
                else if (TXTFile != null)
                {
                    try
                    {
                        CachedFileManager.DeferUpdates(TXTFile);
                        using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                            {
                                RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                            }
                            else
                            {
                                //File failed to check for saving
                                FileNotSavedInfoBar.Title = "An unexpected error occured while syncing your file. Please reopen the file in a new tab to proceed, and close the current tab.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                        }

                        TXTFile = null;

                        var Options = TextSetOptions.FormatRtf | TextSetOptions.ApplyRtfDocumentDefaults;
                        RTB.Document.SetText(Options, string.Empty);
                        REB.Document.SetText(Options, string.Empty);

                        if (overrideFile != "")
                        {
                            await EraseFile();
                        }

                        FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                        if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            IsCloseRequestComplete = true;
                            ToastBuilder.BuildToastForFileSave(TXTFile.Path);
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "File couldn't be saved.";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        FileNotSavedInfoBar.Title = "The file is currently in use.";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
            }

            //----------Save Erase----------

            if (Background == false && Close == false && Erase == true && NoFile == false)
            {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
                //File dialog configuration
                var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);

                FSP.FileTypeChoices.Add("Rich Ivirius Text", new List<string>() { ".ivrtext" });
                FSP.FileTypeChoices.Add("Universal Rich Text", new List<string>() { ".rtf" });
                FSP.FileTypeChoices.Add("Rich Text", new List<string>() { ".richtxtformat" });
                FSP.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                FSP.SuggestedFileName = "New Rich Text File";

                //Set file content
                TXTFile = await FSP.PickSaveFileAsync();
                if (TXTFile != null)
                {
                    try
                    {
                        CachedFileManager.DeferUpdates(TXTFile);
                        using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            REB.Document.SaveToStream(TextGetOptions.FormatRtf, RAS);
                            if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                            {
                                RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                            }
                            else
                            {
                                //File failed to check for saving
                                FileNotSavedInfoBar.Title = "An unexpected error occured while syncing your file. Please reopen the file in a new tab to proceed, and close the current tab.";
                                FileNotSavedInfoBar.IsOpen = true;
                            }
                        }

                        TXTFile = null;

                        var Options = TextSetOptions.FormatRtf | TextSetOptions.ApplyRtfDocumentDefaults;
                        RTB.Document.SetText(Options, string.Empty);
                        REB.Document.SetText(Options, string.Empty);

                        if (overrideFile != "")
                        {
                            await EraseFile();
                        }

                        FileUpdateStatus Stats = await CachedFileManager.CompleteUpdatesAsync(TXTFile);
                        if (await CachedFileManager.CompleteUpdatesAsync(TXTFile) is FileUpdateStatus.Complete)
                        {
                            //Confirm file saving without close
                            IsCloseRequestComplete = true;
                            ToastBuilder.BuildToastForFileSave(TXTFile.Path);
                            CheckForSaving();
                        }
                        else
                        {
                            //File failed to save message
                            FileNotSavedInfoBar.Title = "File couldn't be saved.";
                            FileNotSavedInfoBar.IsOpen = true;
                            CheckForSaving();
                        }
                    }
                    catch
                    {
                        FileNotSavedInfoBar.Title = "The file is currently in use.";
                        FileNotSavedInfoBar.IsOpen = true;
                        CheckForSaving();
                    }
                }
            }
        }

        public async Task Open()
        {
            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/

            //File dialog configuration
            var FOP = InitializeWithWindow(new FileOpenPicker(),App.WindowHandle);
            FOP.FileTypeFilter.Add(".ivrtext");
            FOP.FileTypeFilter.Add(".rtf");
            FOP.FileTypeFilter.Add(".richtxtformat");
            FOP.SuggestedStartLocation = PickerLocationId.Desktop;

            //Set file content
            TXTFile = await FOP.PickSingleFileAsync();
            if (TXTFile != null)
            {
                try
                {
                    //Set RichEditBox content
                    using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        IsRTF = true;
                        REB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                        RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                        var s = await FileIO.ReadTextAsync(TXTFile);
                        if (s != string.Empty)
                        {
                            if (GetText(RTB).Contains("rtf1") == true && s.Contains("rtf1") == true)
                            {
                                AutoSaveSwitch.IsEnabled = true;
                                if (!(LocalSettings.Values["Autosave"] == null))
                                {
                                    if ((string)LocalSettings.Values["Autosave"] == "On")
                                    {
                                        AutoSaveSwitch.IsOn = true;
                                    }
                                    if ((string)LocalSettings.Values["Autosave"] == "Off")
                                    {
                                        AutoSaveSwitch.IsOn = false;
                                    }
                                }
                                else
                                {
                                    LocalSettings.Values["Autosave"] = "On";
                                    AutoSaveSwitch.IsOn = true;
                                }
                                AutoSaveSwitch.OffContent = "Autosave: Off";
                                FileIntactBlock.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                AutoSaveSwitch.IsEnabled = false;
                                AutoSaveSwitch.IsOn = false;
                                AutoSaveSwitch.OffContent = "Corrupt or not rich text file. Autosave has been turned off for safety reasons.";
                                FileIntactBlock.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            AutoSaveSwitch.IsEnabled = true;
                            if (!(LocalSettings.Values["Autosave"] == null))
                            {
                                if ((string)LocalSettings.Values["Autosave"] == "On")
                                {
                                    AutoSaveSwitch.IsOn = true;
                                }
                                if ((string)LocalSettings.Values["Autosave"] == "Off")
                                {
                                    AutoSaveSwitch.IsOn = false;
                                }
                            }
                            else
                            {
                                LocalSettings.Values["Autosave"] = "On";
                                AutoSaveSwitch.IsOn = true;
                            }
                            AutoSaveSwitch.OffContent = "Autosave: Off";
                            FileIntactBlock.Visibility = Visibility.Collapsed;
                        }
                    }
                    HomePage.Visibility = Visibility.Collapsed;
                    CheckForSaving();
                    _ = REB.Focus(FocusState.Programmatic);
                }
                catch
                {
                    FileNotSavedInfoBar.Title = "This file is currently in use or has been recently closed. Any changes you make in the current document must be saved separately. Try again later";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                    return;
                }
            }
            AutoSaveSwitch.IsEnabled = true;
            if (!(LocalSettings.Values["Autosave"] == null))
            {
                if ((string)LocalSettings.Values["Autosave"] == "On")
                {
                    AutoSaveSwitch.IsOn = true;
                }
                if ((string)LocalSettings.Values["Autosave"] == "Off")
                {
                    AutoSaveSwitch.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Autosave"] = "On";
                AutoSaveSwitch.IsOn = true;
            }
            AutoSaveSwitch.OffContent = "Autosave: Off";
            FileIntactBlock.Visibility = Visibility.Collapsed;
        }

        private static FileOpenPicker InitializeWithWindow(FileOpenPicker obj, IntPtr windowHandle)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(obj, windowHandle);
            return obj;
        }

        public async Task OpenRecent(StorageFile file)
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            //File dialog configuration
            var FOP = InitializeWithWindow(new FileOpenPicker(),App.WindowHandle);
            FOP.FileTypeFilter.Add(".ivrtext");
            FOP.FileTypeFilter.Add(".rtf");
            FOP.FileTypeFilter.Add(".richtxtformat");
            FOP.SuggestedStartLocation = PickerLocationId.Desktop;

            //Set file content
            TXTFile = file;
            if (TXTFile != null)
            {
                try
                {
                    //Set RichEditBox content
                    using (IRandomAccessStream RAS = await TXTFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        IsRTF = true;
                        REB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                        RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                    }
                    HomePage.Visibility = Visibility.Collapsed;
                    CheckForSaving();
                    _ = REB.Focus(FocusState.Programmatic);
                }
                catch
                {
                    FileNotSavedInfoBar.Title = "This file is currently in use or has been recently closed. Any changes you make in the current document must be saved separately. Try again later";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                    return;
                }
            }
        }

        public async Task OpenMultiple()
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            //File dialog configuration
            var FOP = InitializeWithWindow(new FileOpenPicker(),App.WindowHandle);
            FOP.FileTypeFilter.Add(".ivrtext");
            FOP.FileTypeFilter.Add(".rtf");
            FOP.FileTypeFilter.Add(".richtxtformat");
            FOP.SuggestedStartLocation = PickerLocationId.Desktop;

            //Set file content
            var List = await FOP.PickMultipleFilesAsync();
            foreach (var file in List)
            {
                await ((App.Window.Content as Frame).Content as MainPage).AddTabForFile(file);
            }
        }

        public async Task ReadFile(StorageFile file)
        {
            if (file != null)
            {
                try
                {
                    //Set RichEditBox content
                    using (IRandomAccessStream RAS = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        IsRTF = true;
                        REB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                        RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, RAS);
                    }
                    HomePage.Visibility = Visibility.Collapsed;
                    CheckForSaving();
                    _ = REB.Focus(FocusState.Programmatic);
                }
                catch
                {
                    FileNotSavedInfoBar.Title = "This file is currently in use or has been recently closed. Any changes you make in the current document must be saved separately. Try again later";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                    return;
                }
            }
        }

        #endregion Actions

        #region External

        public List<string> Extensions = new List<string>
        {
            ".rtf",
            ".richtxtformat",
            ".rwhi",
            ".ivrtext",
            ".doc"
        };

        protected override async void OnNavigatedTo(NavigationEventArgs EvArgs)
        {
            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

            //Catch file
            base.OnNavigatedTo(EvArgs);
            var Args = EvArgs.Parameter as IActivatedEventArgs;
            var FArgs = Args as FileActivatedEventArgs;
            try
            {
                if (Args != null && Args.Kind == ActivationKind.File)
                {
                    //Write file properties
                    TXTFile = FArgs.Files[0] as StorageFile;
                    IRandomAccessStreamWithContentType Str = await TXTFile.OpenReadAsync();
                    IsRTF = true;
                    REB.Document.LoadFromStream(TextSetOptions.FormatRtf, Str);
                    RTB.Document.LoadFromStream(TextSetOptions.FormatRtf, Str);
                    var sr = new StreamReader(Str.AsStreamForRead());
                    var s = await FileIO.ReadTextAsync(TXTFile);
                    if (s != string.Empty)
                    {
                        if (GetText(RTB).Contains("rtf1") == true && s.Contains("rtf1") == true)
                        {
                            AutoSaveSwitch.IsEnabled = true;
                            if (!(LocalSettings.Values["Autosave"] == null))
                            {
                                if ((string)LocalSettings.Values["Autosave"] == "On")
                                {
                                    AutoSaveSwitch.IsOn = true;
                                }
                                if ((string)LocalSettings.Values["Autosave"] == "Off")
                                {
                                    AutoSaveSwitch.IsOn = false;
                                }
                            }
                            else
                            {
                                LocalSettings.Values["Autosave"] = "On";
                                AutoSaveSwitch.IsOn = true;
                            }
                            AutoSaveSwitch.OffContent = "Autosave: Off";
                            FileIntactBlock.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            AutoSaveSwitch.IsEnabled = false;
                            AutoSaveSwitch.IsOn = false;
                            AutoSaveSwitch.OffContent = "Corrupt or not rich text file. Autosave has been turned off for safety reasons.";
                            FileIntactBlock.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        AutoSaveSwitch.IsEnabled = true;
                        if (!(LocalSettings.Values["Autosave"] == null))
                        {
                            if ((string)LocalSettings.Values["Autosave"] == "On")
                            {
                                AutoSaveSwitch.IsOn = true;
                            }
                            if ((string)LocalSettings.Values["Autosave"] == "Off")
                            {
                                AutoSaveSwitch.IsOn = false;
                            }
                        }
                        else
                        {
                            LocalSettings.Values["Autosave"] = "On";
                            AutoSaveSwitch.IsOn = true;
                        }
                        AutoSaveSwitch.OffContent = "Autosave: Off";
                        FileIntactBlock.Visibility = Visibility.Collapsed;
                    }
                    Str.Dispose();
                    HomePage.Visibility = Visibility.Collapsed;
                    CheckForSaving();
                }
                else
                {
                    CheckForSaving();
                    return;
                }
            }
            catch
            {
                try
                {
                    FileNotSavedInfoBar.Title = "This file is currently in use or has been recently closed. Try again later";
                    FileNotSavedInfoBar.IsOpen = true;
                    CheckForSaving();
                    return;
                }
                catch { return; }
            }
        }

        #endregion External

        #endregion File

        #region Text Editing

        #region Clipboard

        private void Undo_Click(object Sender, RoutedEventArgs EvArgs) => REB.Document.Undo();

        private void Redo_Click(object Sender, RoutedEventArgs EvArgs) => REB.Document.Redo();

        private void MenuFlyoutItem_Click_4(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Paste(0);
        }

        private void MenuFlyoutItem_Click_5(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Paste(1);
        }

        private void CTB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Cut();
            REB.ContextFlyout.Hide();
        }

        private void CB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Copy();
            REB.ContextFlyout.Hide();
        }

        private void PB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Paste(0);
            REB.ContextFlyout.Hide();
        }

        private void SAB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            _ = REB.Focus(FocusState.Pointer);
            string Text = GetText(REB);
            REB.Document.Selection.SetRange(0, Text.Length);
            REB.ContextFlyout.Hide();
        }

        private void MenuFlyoutItem_Click_9(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Cut();
            REB.ContextFlyout.Hide();
        }

        private void MenuFlyoutItem_Click_10(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Copy();
            REB.ContextFlyout.Hide();
        }

        private void MenuFlyoutItem_Click_11(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Paste(0);
            REB.ContextFlyout.Hide();
        }

        private void MenuFlyoutItem_Click_12(object Sender, RoutedEventArgs EvArgs)
        {
            _ = REB.Focus(FocusState.Pointer);
            string Text = GetText(REB);
            REB.Document.Selection.SetRange(0, Text.Length);
            REB.ContextFlyout.Hide();
        }

        #endregion Clipboard

        #region Font & paragraph

        private void MenuFlyoutItem_Click_3(object sender, RoutedEventArgs e)
        {
            REB.Document.Selection?.ChangeCase(LetterCase.Lower);
        }

        private void MenuFlyoutItem_Click_26(object sender, RoutedEventArgs e)
        {
            REB.Document.Selection?.ChangeCase(LetterCase.Upper);
        }

        private void EraseFormatBTN_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Undefined;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Undefined;
                CF.Name = null;
                FontBox.SelectedItem = null;
                CF.Outline = FormatEffect.Undefined;
                CF.Size = (float)10.5;
                FSize.Value = 10.5;
                CF.Underline = UnderlineType.Undefined;
                CF.Strikethrough = FormatEffect.Undefined;
                _ = ST.CharacterFormat.ForegroundColor;
                BackAccent.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                ST.CharacterFormat.BackgroundColor = Microsoft.UI.Colors.Transparent;
                _ = ST.CharacterFormat.ForegroundColor;
                FontAccent.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Black);
                ST.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.Black;
                CF.Subscript = FormatEffect.Undefined;
                CF.Superscript = FormatEffect.Undefined;
            }
        }

        private void MenuFlyoutItem_Click_13(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure bold
            ITextSelection ST = REB.Document.Selection;
            if (ST != null)
            {
                FormatEffect CF = ST.CharacterFormat.Bold;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        break;
                }
                ST.CharacterFormat.Bold = CF;
            }
        }

        private void MenuFlyoutItem_Click_14(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure italic
            ITextSelection ST = REB.Document.Selection;
            if (ST != null)
            {
                FormatEffect CF = ST.CharacterFormat.Italic;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        break;
                }
                ST.CharacterFormat.Italic = CF;
            }
        }

        private void MenuFlyoutItem_Click_15(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure strikethrough
            ITextSelection ST = REB.Document.Selection;
            if (ST != null)
            {
                FormatEffect CF = ST.CharacterFormat.Strikethrough;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        break;
                }
                ST.CharacterFormat.Strikethrough = CF;
            }
        }

        private void MenuFlyoutItem_Click_16(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure subscript
            ITextSelection ST = REB.Document.Selection;
            if (ST != null)
            {
                FormatEffect CF = ST.CharacterFormat.Subscript;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        break;
                }
                ST.CharacterFormat.Subscript = CF;
            }
        }

        private void MenuFlyoutItem_Click_17(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure superscript
            ITextSelection ST = REB.Document.Selection;
            if (ST != null)
            {
                FormatEffect CF = ST.CharacterFormat.Superscript;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        break;
                }
                ST.CharacterFormat.Superscript = CF;
            }
        }

        private void MenuFlyoutItem_Click_1(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure underline
            var MFItem = (MenuFlyoutItem)Sender;
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                MarkerType CF = ST.ParagraphFormat.ListType;
                if (MFItem.Text == "None") CF = MarkerType.None;
                if (MFItem.Text == "Bullet") CF = MarkerType.Bullet;
                if (MFItem.Text == "Numbers") CF = MarkerType.CircledNumber;
                if (MFItem.Text == "Lowercase letters") CF = MarkerType.LowercaseEnglishLetter;
                if (MFItem.Text == "Uppercase letters") CF = MarkerType.UppercaseEnglishLetter;
                if (MFItem.Text == "Roman") CF = MarkerType.UppercaseRoman;
                ST.ParagraphFormat.ListType = CF;
                REB.ContextFlyout.Hide();
            }
        }

        private void BB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure bold
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                FormatEffect CF = ST.CharacterFormat.Bold;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        BB.IsChecked = true;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        BB.IsChecked = false;
                        break;
                }
                ST.CharacterFormat.Bold = CF;
            }
        }

        private void IB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure italic
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                FormatEffect CF = ST.CharacterFormat.Italic;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        IB.IsChecked = true;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        IB.IsChecked = false;
                        break;
                }
                ST.CharacterFormat.Italic = CF;
            }
        }

        private void STB_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure strikethrough
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                FormatEffect CF = ST.CharacterFormat.Strikethrough;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        STB.IsChecked = true;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        STB.IsChecked = false;
                        break;
                }
                ST.CharacterFormat.Strikethrough = CF;
            }
        }

        private void MenuFlyoutItem_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure underline
            var MFItem = (MenuFlyoutItem)Sender;
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                UnderlineType CF = ST.CharacterFormat.Underline;
                if (MFItem.Text == "None") CF = UnderlineType.None;
                if (MFItem.Text == "Single") CF = UnderlineType.Single;
                if (MFItem.Text == "Dash") CF = UnderlineType.Dash;
                if (MFItem.Text == "Dotted") CF = UnderlineType.Dotted;
                if (MFItem.Text == "Double") CF = UnderlineType.Double;
                if (MFItem.Text == "Thick") CF = UnderlineType.Thick;
                if (MFItem.Text == "Wave") CF = UnderlineType.Wave;
                ST.CharacterFormat.Underline = CF;
                REB.ContextFlyout.Hide();
            }
        }

        private void MenuFlyoutItem2_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure paragraph alignment
            var MFItem = (MenuFlyoutItem)Sender;
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ParagraphAlignment CF = ST.ParagraphFormat.Alignment;
                if (MFItem.Text == "Left") CF = ParagraphAlignment.Left;
                if (MFItem.Text == "Center") CF = ParagraphAlignment.Center;
                if (MFItem.Text == "Right") CF = ParagraphAlignment.Right;
                if (MFItem.Text == "Justify") CF = ParagraphAlignment.Justify;
                ST.ParagraphFormat.Alignment = CF;
                REB.ContextFlyout.Hide();
            }
        }

        private void ColorPicker_ColorChanged(object Sender, ColorChangedEventArgs EvArgs)
        {
            //Configure font color
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                _ = ST.CharacterFormat.ForegroundColor;
                var Br = new SolidColorBrush(ColPicker.Color);
                Color CF = ColPicker.Color;
                FontAccent.Foreground = Br;
                ST.CharacterFormat.ForegroundColor = CF;
            }
        }

        private void BackPicker_ColorChanged(object Sender, ColorChangedEventArgs EvArgs)
        {
            //Configure font highlight
            if (!(REB == null))
            {
                ITextSelection ST = REB.Document.Selection;
                if (!(ST == null))
                {
                    _ = ST.CharacterFormat;
                    var Br = new SolidColorBrush(BackPicker.Color);
                    Color CF = BackPicker.Color;
                    if (BackAccent != null) BackAccent.Foreground = Br;
                    ST.CharacterFormat.BackgroundColor = CF;
                }
            }
        }

        private void FontBox_SelectionChanged(object Sender, SelectionChangedEventArgs EvArgs)
        {
            //Configure font family
            ITextSelection ST = REB.Document.Selection;
            if (ST != null && FontBox.SelectedItem != null)
            {
                ST.CharacterFormat.Name = FontBox.SelectedItem.ToString();
            }
            else return;
        }

        private void NumberBox_ValueChanged(object Sender, EventArgs EvArgs)
        {
            //Configure font size
            if (!(REB == null))
            {
                ITextSelection ST = REB.Document.Selection;
                if (!(ST == null))
                {
                    _ = ST.CharacterFormat.Size;
                    if (FSize != null && FSize.Value != double.NaN && FSize.Value != 0)
                    {
                        float CF = (float)FSize.Value;
                        ST.CharacterFormat.Size = CF;
                    }
                    else return;
                }
            }
        }

        private void ForegroundButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure font color
            var BTN = Sender as Button;
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                _ = ST.CharacterFormat.ForegroundColor;
                Brush Br = BTN.Foreground;
                FontAccent.Foreground = Br;
                ST.CharacterFormat.ForegroundColor = (BTN.Foreground as SolidColorBrush).Color;
            }
        }

        private void NullForegroundButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure font color
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                _ = ST.CharacterFormat.ForegroundColor;
                if (ActualTheme == ElementTheme.Light)
                {
                    FontAccent.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Black);
                    ST.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.Black;
                }
                else
                {
                    FontAccent.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
                    ST.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.White;
                }
            }
        }

        private void HighlightButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure font color
            var BTN = Sender as Button;
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                _ = ST.CharacterFormat.ForegroundColor;
                Brush Br = BTN.Foreground;
                BackAccent.Foreground = Br;
                ST.CharacterFormat.BackgroundColor = (BTN.Foreground as SolidColorBrush).Color;
            }
        }

        private void NullHighlightButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure font color
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                _ = ST.CharacterFormat.ForegroundColor;
                BackAccent.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                ST.CharacterFormat.BackgroundColor = Microsoft.UI.Colors.Transparent;
            }
        }

        private void SubscriptButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure subscript
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                FormatEffect CF = ST.CharacterFormat.Subscript;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        SubscriptButton.IsChecked = true;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        SubscriptButton.IsChecked = false;
                        break;
                }
                ST.CharacterFormat.Subscript = CF;
            }
        }

        private void SuperscriptButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure superscript
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                FormatEffect CF = ST.CharacterFormat.Superscript;
                switch (CF)
                {
                    case FormatEffect.Off:
                        CF = FormatEffect.On;
                        SuperscriptButton.IsChecked = true;
                        break;
                    default:
                        CF = FormatEffect.Off;
                        SuperscriptButton.IsChecked = false;
                        break;
                }
                ST.CharacterFormat.Superscript = CF;
            }
        }

        #endregion Font & paragraph

        #region Insert

        StorageFile IMGFile;

        private void Button_Click_11(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Text
                += DateTime.Now.Month.ToString()
                + "/" + DateTime.Now.Day.ToString()
                + "/" + DateTime.Now.Year.ToString()
                + " " + DateTime.Now.Hour.ToString()
                + ":" + DateTime.Now.Minute.ToString()
                + ":" + DateTime.Now.Second.ToString();
            DateTimeInsert.Flyout.Hide();
        }

        private void Button_Click_12(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Text
                += DateTime.Now.Day.ToString()
                + "." + DateTime.Now.Month.ToString()
                + "." + DateTime.Now.Year.ToString()
                + ", " + DateTime.Now.Hour.ToString()
                + ":" + DateTime.Now.Minute.ToString()
                + ":" + DateTime.Now.Second.ToString();
            DateTimeInsert.Flyout.Hide();
        }

        private void Button_Click_13(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Text
                += DateTime.Now.Day.ToString()
                + "." + DateTime.Now.Month.ToString()
                + ", " + DateTime.Now.Hour.ToString()
                + ":" + DateTime.Now.Minute.ToString()
                + ":" + DateTime.Now.Second.ToString()
                + ":" + DateTime.Now.Millisecond.ToString();
            DateTimeInsert.Flyout.Hide();
        }

        private void Button_Click_14(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Text
                += DateTime.Now.Month.ToString()
                + "/" + DateTime.Now.Day.ToString()
                + ", " + DateTime.Now.Hour.ToString()
                + ":" + DateTime.Now.Minute.ToString()
                + ":" + DateTime.Now.Second.ToString()
                + ":" + DateTime.Now.Millisecond.ToString();
            DateTimeInsert.Flyout.Hide();
        }

        private void Button_Click_15(object Sender, RoutedEventArgs EvArgs)
        {
            REB.Document.Selection.Text += DateTime.Now.ToString();
            DateTimeInsert.Flyout.Hide();
        }

        private void Button_Click_6(object Sender, RoutedEventArgs EvArgs)
        {
            if (string.IsNullOrEmpty(REB.Document.Selection.Link)) return; REB.Document.Selection.Link = "";
        }

        private void Button_Click_5(object Sender, RoutedEventArgs EvArgs)
        {
            if (string.IsNullOrEmpty(HLBox.Text)) return;
            if (string.IsNullOrEmpty(REB.Document.Selection.Text)) return;
            REB.Document.Selection.Link = "\"" + HLBox.Text + "\"";
            LinkInsert.Flyout.Hide();
        }

        private async void Image_Insert_Click(object Sender, RoutedEventArgs EvArgs)
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            //File dialog configuration
            var FOP = InitializeWithWindow(new FileOpenPicker(),App.WindowHandle);
            FOP.FileTypeFilter.Add(".png");
            FOP.FileTypeFilter.Add(".jpg");
            FOP.FileTypeFilter.Add(".jpeg");
            FOP.FileTypeFilter.Add(".bmp");
            FOP.SuggestedStartLocation = PickerLocationId.Desktop;
            IMGFile = await FOP.PickSingleFileAsync();
            //Set file content
            if (!(IMGFile == null))
            {
                try
                {
                    //Insert image
                    IRandomAccessStream RAC = await IMGFile.OpenAsync(FileAccessMode.Read);
                    var BMP = new BitmapImage(new Uri(IMGFile.Path));
                    REB.Document.Selection.InsertImage(BMP.PixelWidth, BMP.PixelHeight, 0, VerticalCharacterAlignment.Baseline, IMGFile.DisplayName, RAC);
                }
                catch (Exception Ex)
                {
                    FatalExceptionMessage.Text = $"{Ex.HResult} ===== {Ex.Message}";
                    FatalExceptionBox.Open();
                }
            }
        }

        private void LinkInsert_Click(object Sender, RoutedEventArgs EvArgs) => LinkInsert.Flyout.ShowAt(LinkInsert);

        #endregion Insert

        #region Doc

        //HandwritingView HWV;

        #region TextToSpeech

        // TODO Microsoft.UI.Xaml.Controls.MediaElement is not yet supported in WindowsAppSDK. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported
                //MediaElement ME;

        SpeechSynthesizer Synth;

        private async void ReadButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configuring the sppech synthesizer
            // TODO Microsoft.UI.Xaml.Controls.MediaElement is not yet supported in WindowsAppSDK. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported
            //            ME = new MediaElement();
            //Synth = new SpeechSynthesizer();
            //SpeechSynthesisStream Str = await Synth.SynthesizeTextToStreamAsync(REB.Document.Selection.Text.ToString());
            //ME.SetSource(Str, Str.ContentType);
            //ME.Play();
            //ME.MediaEnded += ME_MediaEnded;
            //KeepReadButton.IsEnabled = true;
            //PauseReadButton.IsEnabled = true;
            //StopReadButton.IsEnabled = true;
        }

        private void ME_MediaEnded(object Sender, RoutedEventArgs EvArgs)
        {
            //Disabling all the action buttons
            KeepReadButton.IsEnabled = false;
            PauseReadButton.IsEnabled = false;
            StopReadButton.IsEnabled = false;
        }

        private void StopReadButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Stops the reading process
            //ME.Stop();
            //KeepReadButton.IsEnabled = false;
            //PauseReadButton.IsEnabled = false;
            //StopReadButton.IsEnabled = false;
        }

        private void PauseReadButton_Click(object Sender, RoutedEventArgs EvArgs) { //ME.Pause();
                                                                                    }

        private void KeepReadButton_Click(object Sender, RoutedEventArgs EvArgs) { 
            //ME.Play();
            }

        #endregion TextToSpeech

        private async void REB_SelectionChanged(object Sender, RoutedEventArgs EvArgs)
        {
            //Variables
            var ST = REB.Document.Selection;
            var CF = ST.CharacterFormat;

            //Microsoft.UI.Colors
            if (BackPicker != null) BackPicker.Color = CF.BackgroundColor;
            if (ColPicker != null) ColPicker.Color = CF.ForegroundColor;
            if (CF.BackgroundColor == Microsoft.UI.Colors.White || CF.BackgroundColor == Microsoft.UI.Colors.Transparent)
            {
                BackAccent.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
            }

            //Autosave
            if (AutoSaveSwitch.IsOn == true)
            {
                await TriggerAutosave();
            }

            //Font
            if (!(FSize == null))
            {
                if (ST.Length is > 0 or < 0) FSize.Value = double.NaN;
                else FSize.Value = CF.Size;
            }

            if (ST.Length is 0)
            {
                FontBox.SelectedIndex = FontBox.Items.IndexOf(CF.Name.ToString());
                FontBox.PlaceholderText = "Segoe UI (Default)";
            }
            else
            {
                FontBox.SelectedItem = null;
                FontBox.PlaceholderText = "Multiple";
            }

            CheckFormatting();

            //Paragraph
            if (ST.Length is > 0 or < 0)
            {

            }
            else
            {
                isTextSelectionChanging = true;
                await CheckIndent();
                isTextSelectionChanging = false;
            }
            CheckAlignment();
            CheckSpacings();

            //Text-to-speech
            if (!(ReadButton == null))
            {
                if (REB.Document.Selection.Text == "") ReadButton.IsEnabled = false;
                else ReadButton.IsEnabled = true;
            }

            //Selected words
            if (ST.Length is > 0 or < 0)
            {
                SelWordGrid.Visibility = Visibility.Visible;
                REB.Document.Selection.GetText(TextGetOptions.None, out var seltext);
                var selwordcount = seltext.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
                SelWordCount.Text = $"Selected words: {selwordcount}";
            }
            else
            {
                SelWordGrid.Visibility = Visibility.Collapsed;
            }

            REB.Document.GetText(TextGetOptions.None, out var text);
            var wordcount = text.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            WordCount.Text = $"Word count: {wordcount}";

            //Disable & enable buttons
            ButtonCheck();
        }

        private void ButtonCheck()
        {
            if (REB.Document.CanCopy() == true)
            {
                CB.IsEnabled = true;
                CBBar.IsEnabled = true;
            }
            else
            {
                CB.IsEnabled = false;
                CBBar.IsEnabled = false;
            }

            if (REB.Document.CanPaste() == true)
            {
                MenuPB.IsEnabled = true;
                PB.IsEnabled = true;
                PBDDB.IsEnabled = true;
                PBBar.IsEnabled = true;
            }
            else
            {
                MenuPB.IsEnabled = false;
                PB.IsEnabled = false;
                PBDDB.IsEnabled = false;
                PBBar.IsEnabled = false;
            }

            if (REB.Document.CanUndo() == true) Undo.IsEnabled = true;
            else Undo.IsEnabled = false;

            if (REB.Document.CanRedo() == true) Redo.IsEnabled = true;
            else Redo.IsEnabled = false;
        }

        private void SizeBox_SelectionChanged(object Sender, SelectionChangedEventArgs EvArgs)
        {
            //Set A4 size
            if ((string)SizeBox.SelectedItem == "A4")
            {
                REB.Width = 744;
                REB.Height = 1052.4;
                PP.MediaSize = Windows.Graphics.Printing.PrintMediaSize.IsoA4;
            }

            //Set Letter size
            if ((string)SizeBox.SelectedItem == "Letter")
            {
                REB.Width = 765;
                REB.Height = 990;
                PP.MediaSize = Windows.Graphics.Printing.PrintMediaSize.NorthAmericaLetter;
            }

            //Set Tabloid size
            if ((string)SizeBox.SelectedItem == "Tabloid")
            {
                REB.Width = 990;
                REB.Height = 1530;
                PP.MediaSize = Windows.Graphics.Printing.PrintMediaSize.NorthAmericaTabloid;
            }
        }

        private void ZoomSlider_ValueChanged(object Sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs EvArgs)
        {
            //Changes the zoom amount
            if (SCR != null && ZoomText != null)
            {
                _ = SCR.ChangeView(0, 0, (float)ZoomSlider.Value);
                string Val = SCR.ZoomFactor.ToString(".00");
                ZoomText.Text = $"Zoom: {Val.Replace('.', '­')}%";
            }
            else
            {

            }
        }

        private void LeftIndent_ValueChanged(object Sender, EventArgs EvArgs)
        {

        }

        private void RightIndent_ValueChanged(object Sender, EventArgs EvArgs)
        {

        }

        bool isTextSelectionChanging;

        private void LeftInd_ValueChanged(object Sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs EvArgs)
        {
            if (isTextSelectionChanging == false) SetParagraphIndents((float)LeftInd.Value, (float)RightInd.Value, (float)TabIndent.Value, true);
        }

        private void RightInd_ValueChanged(object Sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs EvArgs)
        {
            if (isTextSelectionChanging == false) SetParagraphIndents((float)LeftInd.Value, (float)RightInd.Value, (float)TabIndent.Value, true);
        }

        private void SetParagraphIndents(float leftIndent, float rightIndent, float firstLineIndent, bool applyToSelectionOnly = true)
        {
            // Get the ITextDocument interface for the RichEditBox's document
            //ITextDocument document = REB.Document;

            //// Get the current selection's start and end positions
            //int start = document.Selection.StartPosition;
            //int end = document.Selection.EndPosition;

            // If applyToSelectionOnly is true, check if there's any selected text in the RichEditBox
            //if (applyToSelectionOnly && start == end)
            //{
            //    //return;
            //}

            // Get the ITextRange interface for the selection or the entire document
            //ITextRange textRange;
            //if (applyToSelectionOnly)
            //{
            //    textRange = document.Selection;
            //}
            //else
            //{
            //    textRange = document.GetRange(0, GetText(REB).Length);
            //}

            // Get the ITextParagraphFormat interface for the text range
            //ITextParagraphFormat paragraphFormat = textRange.ParagraphFormat;

            // Set the left and right indents for the current selection's paragraph(s)
            //try
            //{
            //    if (document.Selection.Length != 0)
            //    {
            //        paragraphFormat.SetIndents(firstLineIndent, leftIndent, rightIndent);
            //    }
            //    else
            //    {
            //        document.GetRange(document.Selection.StartPosition, document.Selection.EndPosition + 1);
            //        paragraphFormat.SetIndents(firstLineIndent, leftIndent, rightIndent);
            //    }
            //}
            //catch
            //{

            //}

            //// Apply the new paragraph format to the current selection or the entire document
            //textRange.ParagraphFormat = paragraphFormat;

            //LeftIndent.Text = leftIndent.ToString();

            //RightIndent.Text = rightIndent.ToString();
        }

        private Task CheckIndent()
        {
            //LeftIndent.Value = REB.Document.Selection.ParagraphFormat.LeftIndent;
            LeftInd.Value = REB.Document.Selection.ParagraphFormat.LeftIndent;
            LeftIndent.Text = REB.Document.Selection.ParagraphFormat.LeftIndent.ToString();

            //RightIndent.Value = REB.Document.Selection.ParagraphFormat.RightIndent;
            RightInd.Value = REB.Document.Selection.ParagraphFormat.RightIndent;
            RightIndent.Text = REB.Document.Selection.ParagraphFormat.RightIndent.ToString();

            TabIndent.Value = REB.Document.Selection.ParagraphFormat.FirstLineIndent;

            return Task.CompletedTask;
        }

        private void HandButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Configure the handwriting dialog
            //REB.IsHandwritingViewEnabled = true;
            //HWV = REB.HandwritingView;
            //switch (HWV.IsOpen)
            //{
            //    case false:
            //        _ = HWV.TryOpen();
            //        break;
            //    case true:
            //        _ = HWV.TryClose();
            //        break;
            //}
        }

        private void ZoomIn_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Zooms in
            ZoomSlider.Value += 0.1;
        }

        private void ZoomOut_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Zooms out
            ZoomSlider.Value -= 0.1;
        }

        #endregion Doc

        #region Templates

        private void Template1_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Normal
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = (float)10.5;
                FSize.Value = 10.5;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template2_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Title
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                ITextParagraphFormat PF = ST.ParagraphFormat;
                PF.Alignment = ParagraphAlignment.Center;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 28;
                FSize.Value = 28;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template3_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Title 2
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                ITextParagraphFormat PF = ST.ParagraphFormat;
                PF.Alignment = ParagraphAlignment.Center;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 22;
                FSize.Value = 22;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template4_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Important
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.On;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.On;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 16;
                FSize.Value = 16;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template5_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Header
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 14;
                FSize.Value = 14;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template6_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Medium
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 18;
                FSize.Value = 18;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template7_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Subtitle
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 20;
                FSize.Value = 20;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template8_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Strong
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.On;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 18;
                FSize.Value = 18;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template9_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Content
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 16;
                FSize.Value = 16;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template10_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Finished
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.On;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 14;
                FSize.Value = 14;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template11_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Unfinished
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.On;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.Off;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 14;
                FSize.Value = 14;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        private void Template12_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //Strong header
            ITextSelection ST = REB.Document.Selection;
            if (!(ST == null))
            {
                ITextCharacterFormat CF = ST.CharacterFormat;
                CF.Bold = FormatEffect.Off;
//                CF.FontStretch = FontStretch.Undefined;
                CF.Italic = FormatEffect.On;
                CF.Name = "Segoe UI";
                FontBox.SelectedItem = "Segoe UI";

                CF.Outline = FormatEffect.Off;
                CF.Size = 18;
                CF.ForegroundColor = Microsoft.UI.Colors.DimGray;
                FSize.Value = 18;
                CF.Underline = UnderlineType.None;
                ST.CharacterFormat = CF;
            }
        }

        #endregion Templates

        #region Find and Replace

        private void RepAllBTN_Click(object Sender, RoutedEventArgs EvArgs)
        {
            if (ReplaceBox.Text == FindTextBox.Text)
            {
                ActionErrorBox.Open();
                ActionErrorMessage.Text = "Usage of identical characters for Find and Replace is not allowed";
            }
            else if (ReplaceBox.Text.ToLower() == FindTextBox.Text.ToLower() && CaseSensBox.IsChecked == true && FullWordsBox.IsChecked == true)
            {
                ActionErrorBox.Open();
                ActionErrorMessage.Text = "Usage of too similar characters for Find and Replace. Please uncheck the \"Match words\" box to proceed";
            }
            else if (ReplaceBox.Text.ToLower() == FindTextBox.Text.ToLower() && CaseSensBox.IsChecked != true)
            {
                ActionErrorBox.Open();
                ActionErrorMessage.Text = "Usage of too similar characters for Find and Replace. Please check the \"Case Sensitive\" box to proceed";
            }
            else
            {
                Replace(REB, FindTextBox.Text, ReplaceBox.Text, true, CaseSensBox.IsChecked, FullWordsBox.IsChecked, true, ReplaceBox);
            }
            FindFlyout.Hide();
        }

        public static void Replace(RichEditBox RichEditBox, string TextToFind, string TextToReplace, bool ReplaceAll, bool? caseSensitive, bool? matchWords, bool? none, TextBox replaceBox)
        {
            int i = 0;

            if (ReplaceAll == true)
            {
                string Value = GetText(RichEditBox);
                if (!(string.IsNullOrWhiteSpace(Value) && string.IsNullOrWhiteSpace(TextToFind) && string.IsNullOrWhiteSpace(TextToReplace)))
                {
                    RichEditBox.Document.Selection.SetRange(0, GetText(RichEditBox).Length);
                    if (caseSensitive == true)
                    {
                        i = FindAsInt(TextToFind, FindOptions.Case, RichEditBox);
                        _ = RichEditBox.Document.Selection.FindText(TextToFind, GetText(RichEditBox).Length, FindOptions.Case);
                        if (i > j)
                        {
                            try
                            {
                                RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                            }
                            catch (StackOverflowException)
                            {
                                return;
                            }
                            RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                            _ = RichEditBox.Focus(FocusState.Pointer);
                            Replace(RichEditBox, TextToFind, TextToReplace, true, true, false, false, replaceBox);
                        }
                        else
                        {
                            j = 0;
                            return;
                        }
                    }
                    if (matchWords == true)
                    {
                        i = FindAsInt(TextToFind, FindOptions.Word, RichEditBox);
                        _ = RichEditBox.Document.Selection.FindText(TextToFind, GetText(RichEditBox).Length, FindOptions.Word);
                        if (i > j)
                        {
                            try
                            {
                                RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                            }
                            catch (StackOverflowException)
                            {
                                return;
                            }
                            RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                            _ = RichEditBox.Focus(FocusState.Pointer);
                            Replace(RichEditBox, TextToFind, TextToReplace, true, false, true, false, replaceBox);
                        }
                        else
                        {
                            j = 0;
                            return;
                        }
                    }
                    if (none == true)
                    {
                        i = FindAsInt(TextToFind, FindOptions.None, RichEditBox);
                        _ = RichEditBox.Document.Selection.FindText(TextToFind, GetText(RichEditBox).Length, FindOptions.None);
                        if (i > j)
                        {
                            try
                            {
                                RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                            }
                            catch (StackOverflowException)
                            {
                                return;
                            }
                            _ = RichEditBox.Focus(FocusState.Pointer);
                            Replace(RichEditBox, TextToFind, TextToReplace, true, false, false, true, replaceBox);
                        }
                        else
                        {
                            j = 0;
                            return;
                        }
                    }
                    _ = RichEditBox.Focus(FocusState.Pointer);
                }
            }
            else
            {
                RichEditBox.Document.Selection.SetRange(0, GetText(RichEditBox).Length);
                if (caseSensitive == true)
                {
                    _ = RichEditBox.Document.Selection.FindText(TextToFind, GetText(RichEditBox).Length, FindOptions.Case);
                    RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                    _ = RichEditBox.Focus(FocusState.Pointer);
                }
                if (matchWords == true)
                {
                    _ = RichEditBox.Document.Selection.FindText(TextToFind, GetText(RichEditBox).Length, FindOptions.Word);
                    RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                    _ = RichEditBox.Focus(FocusState.Pointer);
                }
                if (none == true)
                {
                    _ = RichEditBox.Document.Selection.FindText(TextToFind, GetText(RichEditBox).Length, FindOptions.None);
                    RichEditBox.Document.Selection.SetText(TextSetOptions.FormatRtf, replaceBox.Text);
                    _ = RichEditBox.Focus(FocusState.Pointer);
                }
                _ = RichEditBox.Focus(FocusState.Pointer);
            }
        }

        public static int FindAsInt(string textToFind, FindOptions options, RichEditBox FindREB)
        {
            ITextRange searchRange = FindREB.Document.GetRange(0, 0);
            int x = 0;
            while (searchRange.FindText(textToFind, TextConstants.MaxUnitCount, options) > 0)
            {
                x++;
            }
            return x;
        }

        private static int j = 0;

        public static string GetText(RichEditBox RichEditor)
        {
            RichEditor.Document.GetText(TextGetOptions.FormatRtf, out string Text);
            ITextRange Range = RichEditor.Document.GetRange(0, Text.Length);
            Range.GetText(TextGetOptions.FormatRtf, out string Value);
            return Value;
        }


        public Task Find(string textToFind, FindOptions options)
        {
            ITextRange searchRange = FindREB.Document.GetRange(0, 0);
            while (searchRange.FindText(textToFind, TextConstants.MaxUnitCount, options) > 0)
            {
                searchRange.CharacterFormat.BackgroundColor = Microsoft.UI.Colors.LightSteelBlue;
                searchRange.CharacterFormat.ForegroundColor = Microsoft.UI.Colors.White;
            }
            return Task.CompletedTask;
        }

        private async void FindBTN_Click(object Sender, RoutedEventArgs EvArgs)
        {
            if (CaseSensBox.IsChecked == true)
            {
                FindREB.Document.SetText(TextSetOptions.FormatRtf, GetText(REB));

                string textToFind = FindTextBox.Text;
                if (textToFind != null)
                {
                    await Find(textToFind, FindOptions.Case);
                    FindREB.IsReadOnly = true;
                }
            }
            if (FullWordsBox.IsChecked == true)
            {
                FindREB.Document.SetText(TextSetOptions.FormatRtf, GetText(REB));

                string textToFind = FindTextBox.Text;
                if (textToFind != null)
                {
                    await Find(textToFind, FindOptions.Word);
                    FindREB.IsReadOnly = true;
                }
            }
            if (CaseSensBox.IsChecked != true && FullWordsBox.IsChecked != true)
            {
                FindREB.Document.SetText(TextSetOptions.FormatRtf, GetText(REB));

                string textToFind = FindTextBox.Text;
                if (textToFind != null)
                {
                    await Find(textToFind, FindOptions.None);
                    FindREB.IsReadOnly = true;
                }
            }
            if (CaseSensBox.IsChecked == true && FullWordsBox.IsChecked == true)
            {
                FindREB.Document.SetText(TextSetOptions.FormatRtf, GetText(REB));

                string textToFind = FindTextBox.Text;
                if (textToFind != null)
                {
                    await Find(textToFind, FindOptions.None);
                    FindREB.IsReadOnly = true;
                }
            }
        }

        private void RepBTN_Click(object Sender, RoutedEventArgs EvArgs)
        {
            Replace(REB, FindTextBox.Text, ReplaceBox.Text, false, false, false, false, ReplaceBox);
            FindFlyout.Hide();
        }

        private void CancelFindRepBTN_Click(object Sender, RoutedEventArgs EvArgs)
        {
            _ = REB.Focus(FocusState.Pointer);
            FindFlyout.Hide();
        }

        #endregion Find and Replace

        #endregion Text Editing

        #region Settings

        #endregion Settings

        #region Layout

        #endregion Layout

        #region Menu Actions

        #region File

        private async void MenuFlyoutItem_Click_23(object sender, RoutedEventArgs e)
        {
            await SaveAsLog();
        }

        private async void MenuFlyoutItem_Click_22(object sender, RoutedEventArgs e)
        {
            var X = (App.Window.Content as Frame).Content as MainPage;
            await X.AddExternalTabAsync();
        }

        #endregion File

        #region Tools

        private async void MenuFlyoutItem_Click_25(object sender, RoutedEventArgs e)
        {
            RestartArgs = "ivirius_text_editor.UserRequestedRestart";
            _ = await CoreApplication.RequestRestartAsync(RestartArgs);
        }

        private void SettingsButton_Click(object Sender, RoutedEventArgs EvArgs) => ((App.Window.Content as Frame).Content as MainPage).LaunchSettingsTab();

        private async void MenuFlyoutItem_Click_20(object Sender, RoutedEventArgs EvArgs)
        {
            ApplicationDataContainer LS = ApplicationData.Current.LocalSettings;
            if (LS.Values["Password"] == null || (string)LS.Values["Password"] == "" || (string)LS.Values["Password"] == "args:passwordNullOrEmpty" || LS != null)
            {
                ActionErrorMessage.Text = "The current user doesn't have a password and can't be logged out";
                ActionErrorBox.Open();
            }
            if (LS.Values["Password"] != null || (string)LS.Values["Password"] != "" || LS != null)
            {
                RestartArgs = "";
                LS.Values["Remember_me"] = "false";
                _ = await CoreApplication.RequestRestartAsync(RestartArgs);
            }
            else
            {
                ActionErrorMessage.Text = "The current user doesn't have a password and can't be logged out";
                ActionErrorBox.Open();
            }
        }

        private void MenuFlyoutItem_Click_2(object Sender, RoutedEventArgs EvArgs) => HomePage.Visibility = Visibility.Visible;

        #endregion Tools

        #region About

        private async void MenuFlyoutItem_Click_7(object Sender, RoutedEventArgs EvArgs)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://errortek-news.vercel.app"));
        }

        private async void MenuFlyoutItem_Click_8(object Sender, RoutedEventArgs EvArgs)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://www.youtube.com/channel/UC-wq6vlXEW3FBj2jMNVMOkg"));
        }

        private void AboutItem_Click(object Sender, RoutedEventArgs EvArgs)
        {
            AboutBox.Open();
        }

        #endregion About

        #endregion Menu Actions

        #region Guide

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {

        }

        public void BeginFullHelpButtons()
        {
            HI1.Visibility = Visibility.Visible;
            HI4.Visibility = Visibility.Visible;
            HI5.Visibility = Visibility.Visible;
            HI6.Visibility = Visibility.Visible;
        }

        private void HI9_Click(object sender, RoutedEventArgs e)
        {
            HI1.Visibility = Visibility.Collapsed;
            HI4.Visibility = Visibility.Collapsed;
            HI5.Visibility = Visibility.Collapsed;
            HI6.Visibility = Visibility.Collapsed;
        }

        DispatcherTimer DTHelp;

        public void BeginFullHelp()
        {
            BeginFullHelpButtons();
            try
            {

            }
            catch
            {
                DTHelp = new DispatcherTimer
                {
                    Interval = new TimeSpan((int)0.1)
                };
                DTHelp.Tick += DTHelp_Tick;
                DTHelp.Start();
            }
        }

        private void DTHelp_Tick(object sender, object e)
        {

            DTHelp.Stop();
        }

        DispatcherTimer DTHW;

        public void OpenHW()
        {
            DTHW = new DispatcherTimer
            {
                Interval = new TimeSpan((int)0.1)
            };
            DTHW.Tick += DTHW_Tick;
            DTHW.Start();
        }

        private void DTHW_Tick(object sender, object e)
        {
            //_ = REB.HandwritingView.TryOpen();
            //_ = REB.HandwritingView.TryClose();
            //_ = REB.HandwritingView.TryOpen();
            DTHW.Stop();
        }

        #endregion Guide

        #region Other Actions

        private void Page_KeyDown(object Sender, KeyRoutedEventArgs EvArgs)
        {
            //Scroll viewer movement
            if (EvArgs.Key == VirtualKey.Shift)
            {
                SCR.IsScrollInertiaEnabled = false;
                SCR.VerticalScrollMode = ScrollMode.Disabled;
                TextCmdBar.IsScrollInertiaEnabled = false;
                TextCmdBar.VerticalScrollMode = ScrollMode.Disabled;
            }
        }

        private void Page_KeyUp(object Sender, KeyRoutedEventArgs EvArgs)
        {
            //Scroll viewer movement
            if (EvArgs.Key == VirtualKey.Shift)
            {
                SCR.IsScrollInertiaEnabled = true;
                SCR.VerticalScrollMode = ScrollMode.Enabled;
                TextCmdBar.IsScrollInertiaEnabled = true;
                TextCmdBar.VerticalScrollMode = ScrollMode.Enabled;
            }
        }

        private void TextCmdBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 515)
            {
                RightToolbar.Visibility = Visibility.Collapsed;
            }
            if (e.NewSize.Width >= 515)
            {
                RightToolbar.Visibility = Visibility.Visible;
            }
            if (e.NewSize.Width < 725)
            {
                WordCount.Visibility = Visibility.Collapsed;
                SelWordCount.Visibility = Visibility.Collapsed;
                SCheckBorder.Visibility = Visibility.Collapsed;
                SCheckBox.Visibility = Visibility.Collapsed;
            }
            if (e.NewSize.Width >= 725)
            {
                WordCount.Visibility = Visibility.Visible;
                SelWordCount.Visibility = Visibility.Visible;
                SCheckBorder.Visibility = Visibility.Visible;
                SCheckBox.Visibility = Visibility.Visible;
            }
            if (e.NewSize.Width < 1125)
            {
                SecondToolbar.Visibility = Visibility.Collapsed;
            }
            if (e.NewSize.Width >= 1125)
            {
                SecondToolbar.Visibility = Visibility.Visible;
            }
            if (e.NewSize.Width < 1285)
            {

            }
            if (e.NewSize.Width >= 1285)
            {

            }
        }

        private async void Button_Click(object Sender, RoutedEventArgs EvArgs) => await Launcher.LaunchUriAsync(new Uri("https://ivirius.vercel.app"));

        private async void Button_Click_1(object sender, RoutedEventArgs EvArgs) => await Launcher.LaunchUriAsync(new Uri("https://ivirius.vercel.app/contact/"));

        private async void Button_Click_2(object Sender, RoutedEventArgs EvArgs) => await Launcher.LaunchUriAsync(new Uri("https://ivirius.webnode.page/ivirius-text-editor-privacy-policy2/"));

        private async void ChangelogButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            await ChangelogWindow.ShowAsync();
        }

        private void Button_Click_7(object Sender, RoutedEventArgs EvArgs)
        {
            LinkInsert.Flyout.Hide();
        }

        private void Button_Click_8(object Sender, RoutedEventArgs EvArgs)
        {
            AboutBox.Open();
        }

        private void SNFCancel_Click(object Sender, RoutedEventArgs EvArgs)
        {
            //deprecated
        }

        private void REB_TextChanged(object Sender, RoutedEventArgs EvArgs)
        {
            CheckForSaving();
        }

        #endregion Other Actions

        #region Home Page

        private void HideHome_Click(object Sender, RoutedEventArgs EvArgs) => HomePage.Visibility = Visibility.Collapsed;

        private async void Button_Click_9(object Sender, RoutedEventArgs EvArgs)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://ivirius.webnode.page/new-setup-for-ivirius-text-editor/"));
        }

        private async void Button_Click_10(object Sender, RoutedEventArgs EvArgs)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://ivirius.webnode.page/tabs-are-coming-for-ivirius-text-editor/"));
        }

        private async void Button_Click_16(object Sender, RoutedEventArgs EvArgs)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://ivirius.webnode.page/ivirius-text-editor-2-0-is-out/"));
        }

        private async void Button_Click_3(object Sender, RoutedEventArgs EvArgs) => await Launcher.LaunchUriAsync(new Uri("https://ivirius.webnode.page/ivirius-text-editor2/"));

        #region Templates

        private void Button_Click_222(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_21(object sender, RoutedEventArgs e)
        {
            overrideFile = "ms-appx:///IviriusTextEditor/Templates/EssayTemp.rtf";
            await NewFile();
        }

        private async void Button_Click_22(object sender, RoutedEventArgs e)
        {
            overrideFile = "ms-appx:///IviriusTextEditor/Templates/ResumeTemp.rtf";
            await NewFile();
        }

        private async void Button_Click_23(object sender, RoutedEventArgs e)
        {
            overrideFile = "ms-appx:///IviriusTextEditor/Templates/CreditsTemp.rtf";
            await NewFile();
        }

        private async void Button_Click_24(object sender, RoutedEventArgs e)
        {
            overrideFile = "ms-appx:///IviriusTextEditor/Templates/ImageEssayTemp.rtf";
            await NewFile();
        }

        private async void Button_Click_25(object sender, RoutedEventArgs e)
        {
            overrideFile = "ms-appx:///IviriusTextEditor/Templates/ImageGalleryTemp.rtf";
            await NewFile();
        }

        private async void Button_Click_26(object sender, RoutedEventArgs e)
        {
            overrideFile = "ms-appx:///IviriusTextEditor/Templates/NewspaperTemp.rtf";
            await NewFile();
        }

        #endregion Templates

        #endregion Home Page

        #region View modes

        private void MenuFlyoutItem_Click_6(object sender, RoutedEventArgs e)
        {
            TopMid.Visibility = Visibility.Visible;
            SP.Visibility = Visibility.Visible;
            RulerGrid.Visibility = Visibility.Visible;
            Bottom.Visibility = Visibility.Visible;
            ToolbarShowButton.Visibility = Visibility.Collapsed;
        }

        private void MenuFlyoutItem_Click_19(object sender, RoutedEventArgs e)
        {
            TopMid.Visibility = Visibility.Collapsed;
            SP.Visibility = Visibility.Visible;
            RulerGrid.Visibility = Visibility.Visible;
            Bottom.Visibility = Visibility.Visible;
            ToolbarShowButton.Visibility = Visibility.Collapsed;
        }

        private void MenuFlyoutItem_Click_21(object sender, RoutedEventArgs e)
        {
            TopMid.Visibility = Visibility.Collapsed;
            SP.Visibility = Visibility.Collapsed;
            RulerGrid.Visibility = Visibility.Collapsed;
            Bottom.Visibility = Visibility.Collapsed;
            ToolbarShowButton.Visibility = Visibility.Visible;
        }

        #endregion View modes

        #region Search

        private List<string> SearchOptions = new List<string>()
        {
            "Cut",
            "Copy",
            "Paste",
            "Select All",
            "Bold",
            "Italic",
            "Underline",
            "Strikethrough",
            "Subscript",
            "Superscript",
            "Font color",
            "Highlight",
            "List",
            "Erase formatting",
            "Font family",
            "Font size",
            "Paragraph alignment",
            "Left indent",
            "Right indent",
            "View modes",
            "Tablet writing",
            "Undo",
            "Redo",
            "Find and replace"
        };

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();
                string[] splitText = sender.Text.ToLower().Split(" ");
                foreach (string option in SearchOptions)
                {
                    bool found = splitText.All((key) =>
                    {
                        return option.ToLower().Contains(key);
                    });
                    if (found)
                    {
                        suitableItems.Add(option);
                    }
                }
                if (suitableItems.Count == 0)
                {
                    suitableItems.Add("No results found");
                }
                sender.ItemsSource = suitableItems;
            }

        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if ((string)args.SelectedItem == "Cut")
            {
                _ = CTB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Copy")
            {
                _ = CB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Paste")
            {
                _ = PB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Select All")
            {
                _ = SAB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Bold")
            {
                _ = BB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Italic")
            {
                _ = IB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Underline")
            {
                _ = UB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Strikethrough")
            {
                _ = STB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Subscript")
            {
                _ = SubscriptButton.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Superscript")
            {
                _ = SuperscriptButton.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Font color")
            {
                _ = CPDDB.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Highlight")
            {
                _ = Back.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "List")
            {
                _ = ListBox.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Erase formatting")
            {
                _ = EraseFormatBTN.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Font family")
            {
                _ = FontBox.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Font size")
            {
                _ = FSize.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Paragraph alignment")
            {
                _ = LeftAl.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Left indent")
            {
                _ = LeftIndent.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Right indent")
            {
                _ = RightIndent.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "View modes")
            {
                _ = ToolbarOptionsButton.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Tablet writing")
            {
                _ = HandButton.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Undo")
            {
                _ = Undo.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Redo")
            {
                _ = Redo.Focus(FocusState.Keyboard);
            }
            if ((string)args.SelectedItem == "Find and replace")
            {
                _ = FindButton.Focus(FocusState.Keyboard);
            }
        }

        #endregion Search

        #region SpellCheck

        private void SCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SCheckOn();
        }

        public void SCheckOn()
        {
            ApplicationDataContainer LS = ApplicationData.Current.LocalSettings;
            LS.Values["SCheck"] = "On";
            REB.IsSpellCheckEnabled = true;
            string Y = GetText(REB);
            //if (T != Y)
            //{
            TextSetOptions Options = TextSetOptions.FormatRtf | TextSetOptions.ApplyRtfDocumentDefaults;
            REB.Document.GetText(TextGetOptions.FormatRtf, out _);
            //REB.Document.SetText(Options, T);
            REB.Document.SetText(Options, string.Empty);
            REB.Document.SetText(Options, Y);
            //}
            //else
            //{

            //}
        }

        public void SCheckOff()
        {
            ApplicationDataContainer LS = ApplicationData.Current.LocalSettings;
            LS.Values["SCheck"] = "Off";
            REB.IsSpellCheckEnabled = false;
            string Y = GetText(REB);
            //if (T != Y)
            //{
            TextSetOptions Options = TextSetOptions.FormatRtf | TextSetOptions.ApplyRtfDocumentDefaults;
            REB.Document.GetText(TextGetOptions.FormatRtf, out _);
            //REB.Document.SetText(Options, T);
            REB.Document.SetText(Options, string.Empty);
            REB.Document.SetText(Options, Y);
            //}
            //else
            //{

            //}
        }

        private void SCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SCheckOff();
        }

        #endregion SpellCheck

        private async void MenuFlyoutItem_Click_35(object sender, RoutedEventArgs e)
        {
            await OpenMultiple();
        }

        private void ZoomReset_Click(object sender, RoutedEventArgs e)
        {
            ZoomSlider.Value = 1;
        }

        private string FileContent;

        private async Task SetDatabase()
        {
            var database = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///IviriusTextEditor.Core/Zippy/ZippyDatabase.txt"));
            var dbstream = await database.OpenAsync(FileAccessMode.Read);
            var dbcontent = await dbstream.ReadTextAsync();
            FileContent = dbcontent.ToString();
            dbstream.Dispose();
        }

        private void SCR_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ZoomSlider.Value = SCR.ZoomFactor;
        }

        private void IndReset_Click(object sender, RoutedEventArgs e)
        {
            LeftIndent.Text = "0";
            RightIndent.Text = "0";
        }


        private void MenuFlyoutFontSize_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as MenuFlyoutItem).Text == "8") REB.Document.Selection.CharacterFormat.Size = 8;
            if ((sender as MenuFlyoutItem).Text == "9") REB.Document.Selection.CharacterFormat.Size = 9;
            if ((sender as MenuFlyoutItem).Text == "10") REB.Document.Selection.CharacterFormat.Size = 10;
            if ((sender as MenuFlyoutItem).Text == "10.5" || (sender as MenuFlyoutItem).Text == "Default") REB.Document.Selection.CharacterFormat.Size = (float)10.5;
            if ((sender as MenuFlyoutItem).Text == "11") REB.Document.Selection.CharacterFormat.Size = 11;
            if ((sender as MenuFlyoutItem).Text == "12") REB.Document.Selection.CharacterFormat.Size = 12;
            if ((sender as MenuFlyoutItem).Text == "14") REB.Document.Selection.CharacterFormat.Size = 14;
            if ((sender as MenuFlyoutItem).Text == "16") REB.Document.Selection.CharacterFormat.Size = 16;
            if ((sender as MenuFlyoutItem).Text == "18") REB.Document.Selection.CharacterFormat.Size = 18;
            if ((sender as MenuFlyoutItem).Text == "20") REB.Document.Selection.CharacterFormat.Size = 20;
            if ((sender as MenuFlyoutItem).Text == "22") REB.Document.Selection.CharacterFormat.Size = 22;
            if ((sender as MenuFlyoutItem).Text == "24") REB.Document.Selection.CharacterFormat.Size = 24;
            if ((sender as MenuFlyoutItem).Text == "26") REB.Document.Selection.CharacterFormat.Size = 26;
            if ((sender as MenuFlyoutItem).Text == "28") REB.Document.Selection.CharacterFormat.Size = 28;
            if ((sender as MenuFlyoutItem).Text == "36") REB.Document.Selection.CharacterFormat.Size = 36;
            if ((sender as MenuFlyoutItem).Text == "48") REB.Document.Selection.CharacterFormat.Size = 48;
            if ((sender as MenuFlyoutItem).Text == "72") REB.Document.Selection.CharacterFormat.Size = 72;
        }

        private void LeftAl_Click(object sender, RoutedEventArgs e)
        {
            var ST = REB.Document.Selection;
            if (ST != null)
            {
                var CF = ST.ParagraphFormat.Alignment;
                if (CF != ParagraphAlignment.Left) CF = ParagraphAlignment.Left;
                else CF = ParagraphAlignment.Justify;
                ST.ParagraphFormat.Alignment = CF;
            }
            CheckAlignment();
        }

        public void CheckAlignment()
        {
            if (REB.Document.Selection.ParagraphFormat.Alignment == ParagraphAlignment.Left)
            {
                LeftAl.IsChecked = true;
                RightAl.IsChecked = false;
                CenterAl.IsChecked = false;
                JustifyAl.IsChecked = false;
            }
            if (REB.Document.Selection.ParagraphFormat.Alignment == ParagraphAlignment.Center)
            {
                LeftAl.IsChecked = false;
                RightAl.IsChecked = false;
                CenterAl.IsChecked = true;
                JustifyAl.IsChecked = false;
            }
            if (REB.Document.Selection.ParagraphFormat.Alignment == ParagraphAlignment.Right)
            {
                LeftAl.IsChecked = false;
                RightAl.IsChecked = true; ;
                CenterAl.IsChecked = false;
                JustifyAl.IsChecked = false;
            }
            if (REB.Document.Selection.ParagraphFormat.Alignment == ParagraphAlignment.Justify)
            {
                LeftAl.IsChecked = false;
                RightAl.IsChecked = false;
                CenterAl.IsChecked = false;
                JustifyAl.IsChecked = true;
            }
            if (REB.Document.Selection.ParagraphFormat.Alignment == ParagraphAlignment.Undefined)
            {
                LeftAl.IsChecked = false;
                RightAl.IsChecked = false;
                CenterAl.IsChecked = false;
                JustifyAl.IsChecked = false;
            }
        }

        private void CenterAl_Click(object sender, RoutedEventArgs e)
        {
            var ST = REB.Document.Selection;
            if (ST != null)
            {
                var CF = ST.ParagraphFormat.Alignment;
                if (CF != ParagraphAlignment.Center) CF = ParagraphAlignment.Center;
                else CF = ParagraphAlignment.Left;
                ST.ParagraphFormat.Alignment = CF;
            }
            CheckAlignment();
        }

        private void RightAl_Click(object sender, RoutedEventArgs e)
        {
            var ST = REB.Document.Selection;
            if (ST != null)
            {
                var CF = ST.ParagraphFormat.Alignment;
                if (CF != ParagraphAlignment.Right) CF = ParagraphAlignment.Right;
                else CF = ParagraphAlignment.Left;
                ST.ParagraphFormat.Alignment = CF;
            }
            CheckAlignment();
        }

        private void JustifyAl_Click(object sender, RoutedEventArgs e)
        {
            var ST = REB.Document.Selection;
            if (ST != null)
            {
                var CF = ST.ParagraphFormat.Alignment;
                if (CF != ParagraphAlignment.Justify) CF = ParagraphAlignment.Justify;
                else CF = ParagraphAlignment.Left;
                ST.ParagraphFormat.Alignment = CF;
            }
            CheckAlignment();
        }

        public void CheckFormatting()
        {
            if (REB.Document.Selection.CharacterFormat.Bold == FormatEffect.On)
            {
                BB.IsChecked = true;
            }
            if (REB.Document.Selection.CharacterFormat.Bold != FormatEffect.On)
            {
                BB.IsChecked = false;
            }
            if (REB.Document.Selection.CharacterFormat.Italic == FormatEffect.On)
            {
                IB.IsChecked = true;
            }
            if (REB.Document.Selection.CharacterFormat.Italic != FormatEffect.On)
            {
                IB.IsChecked = false;
            }
            if (REB.Document.Selection.CharacterFormat.Strikethrough == FormatEffect.On)
            {
                STB.IsChecked = true;
            }
            if (REB.Document.Selection.CharacterFormat.Strikethrough != FormatEffect.On)
            {
                STB.IsChecked = false;
            }
            if (REB.Document.Selection.CharacterFormat.Subscript == FormatEffect.On)
            {
                SubscriptButton.IsChecked = true;
            }
            if (REB.Document.Selection.CharacterFormat.Subscript != FormatEffect.On)
            {
                SubscriptButton.IsChecked = false;
            }
            if (REB.Document.Selection.CharacterFormat.Superscript == FormatEffect.On)
            {
                SuperscriptButton.IsChecked = true;
            }
            if (REB.Document.Selection.CharacterFormat.Superscript != FormatEffect.On)
            {
                SuperscriptButton.IsChecked = false;
            }
        }

        private void NumberBox_ValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
        {
            //Configure font size
            if (!(REB == null))
            {
                var ST = REB.Document.Selection;
                if (!(ST == null))
                {
                    _ = ST.CharacterFormat.Size;
                    if (FSize != null && FSize.Value != double.NaN && FSize.Value != 0)
                    {
                        try
                        {
                            var CF = (float)FSize.Value;
                            ST.CharacterFormat.Size = CF;
                        }
                        catch { }
                    }
                    else return;
                }
            }
        }

        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            FontColorBox.Open();
            CPDDB.Flyout.Hide();
        }

        private void ColorPicker_ColorChanged(Microsoft.UI.Xaml.Controls.ColorPicker sender, Microsoft.UI.Xaml.Controls.ColorChangedEventArgs args)
        {
            //Configure font color
            var ST = REB.Document.Selection;
            if (!(ST == null))
            {
                _ = ST.CharacterFormat.ForegroundColor;
                var Br = new SolidColorBrush(ColPicker.Color);
                var CF = ColPicker.Color;
                FontAccent.Foreground = Br;
                ST.CharacterFormat.ForegroundColor = CF;
            }
        }

        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            AboutBox.Close();
        }

        private async void HyperlinkButton_Click_4(object sender, RoutedEventArgs e)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://mit-license.org/"));
        }

        private void ConsoleTBBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConsoleMSGBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void REB_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RulerGrid.Width = REB.Width;
        }

        private void TableInsertBox_FirstButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void TableInsertBox_SecondButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void SCR3_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }

        private void FSize_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void FSizeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //FSizeBar.Value = 100;
        }

        private void FSize_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void FSizeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //FSizeBar.Value = 0;
        }

        private void MenuBarItem_PointerReleased(object sender, RoutedEventArgs e)
        {
            TextCmdBar.Visibility = Visibility.Visible;
            InsertCmdBar.Visibility = Visibility.Collapsed;
        }

        private void MenuBarItem_PointerReleased_1(object sender, RoutedEventArgs e)
        {
            TextCmdBar.Visibility = Visibility.Collapsed;
            InsertCmdBar.Visibility = Visibility.Visible;
        }

        private void Button_Click_33(object sender, RoutedEventArgs e)
        {
            RunEditConfig();
        }

        private void RunEditConfig()
        {
            TextCmdBar.Visibility = Visibility.Visible;
            InsertCmdBar.Visibility = Visibility.Collapsed;
            EditButton.IsChecked = true;
            InsertButton.IsChecked = false;
        }

        private void Button_Click_34(object sender, RoutedEventArgs e)
        {
            TextCmdBar.Visibility = Visibility.Collapsed;
            InsertCmdBar.Visibility = Visibility.Visible;
            EditButton.IsChecked = false;
            InsertButton.IsChecked = true;
        }

        private void FindAndRepButton_Click(object sender, RoutedEventArgs e)
        {
            FindFlyout.ShowAt(FindAndRepButton);
        }

        private void TabIndent_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (isTextSelectionChanging == false) SetParagraphIndents((float)LeftInd.Value, (float)RightInd.Value, (float)TabIndent.Value, true);
        }

        private void RadioMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

            REB.Document.Selection.ParagraphFormat.SetLineSpacing(LineSpacingRule.Multiple, 1);
            CheckSpacings();
        }

        private void RadioMenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {

            REB.Document.Selection.ParagraphFormat.SetLineSpacing(LineSpacingRule.Multiple, (float)1.15);
            CheckSpacings();
        }

        private void RadioMenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {

            REB.Document.Selection.ParagraphFormat.SetLineSpacing(LineSpacingRule.Multiple, (float)1.5);
            CheckSpacings();
        }

        private void RadioMenuFlyoutItem_Click_3(object sender, RoutedEventArgs e)
        {

            REB.Document.Selection.ParagraphFormat.SetLineSpacing(LineSpacingRule.Multiple, 2);
            CheckSpacings();
        }

        public void CheckSpacings()
        {
            if (REB.Document.Selection.ParagraphFormat.LineSpacing == 1 + FSize.Value && REB.Document.Selection.ParagraphFormat.LineSpacingRule == LineSpacingRule.Multiple)
            {

            }
            if (REB.Document.Selection.ParagraphFormat.LineSpacing == 1.15 + FSize.Value && REB.Document.Selection.ParagraphFormat.LineSpacingRule == LineSpacingRule.Multiple)
            {

            }
            if (REB.Document.Selection.ParagraphFormat.LineSpacing == 1.5 + FSize.Value && REB.Document.Selection.ParagraphFormat.LineSpacingRule == LineSpacingRule.Multiple)
            {

            }
            if (REB.Document.Selection.ParagraphFormat.LineSpacing == 2 + FSize.Value && REB.Document.Selection.ParagraphFormat.LineSpacingRule == LineSpacingRule.Multiple)
            {

            }
        }

        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            SearchBox.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            SearchBox.Visibility = Visibility.Collapsed;
        }

        private void ToggleMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void FSizeBar_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {

        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void TVBorder_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        private void Button_Click_35(object sender, RoutedEventArgs e)
        {

        }

        private void DDTempViewer_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void PrintingInfoBox_FirstButtonClick(object sender, RoutedEventArgs e)
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            var FSP2 = InitializeWithWindow(new FolderPicker(),App.WindowHandle);

            FSP2.CommitButtonText = "Select folder";
            FSP2.SuggestedStartLocation = PickerLocationId.Desktop;

            var TempFolder = await FSP2.PickSingleFolderAsync();

            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(TempFolder);
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/



            var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);

            FSP.FileTypeChoices.Add("Ivirius Printing Bridge File", new List<string>() { ".ivrprintingservice" });
            FSP.SuggestedStartLocation = PickerLocationId.Desktop;
            FSP.SuggestedFileName = "IviriusTextEditorPrinterCachedDocument";

            var TempFile = await FSP.PickSaveFileAsync();

            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(TempFile);

            PrintingInfoBox.Close();
        }

        private static FolderPicker InitializeWithWindow(FolderPicker obj, IntPtr windowHandle)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(obj, windowHandle);
            return obj;
        }

        private async void Button_Click_36(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("elevatewpf-full:///"));
        }

        public async void hell()
        {
/*
    TODO You should replace 'App.WindowHandle' with the your window's handle (HWND) 
    Read more on retrieving window handle here: https://docs.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
*/
            var FSP = InitializeWithWindow(new FileSavePicker(),App.WindowHandle);

            FSP.FileTypeChoices.Add("Ivirius Printing Bridge File", new List<string>() { ".ivrprintingservice" });
            FSP.SuggestedStartLocation = PickerLocationId.Desktop;
            FSP.SuggestedFileName = "IviriusTextEditorPrinterCachedDocument";

            var TempFile = await FSP.PickSaveFileAsync();
        }

        private void LeftIndent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LeftIndent_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                int.TryParse(LeftIndent.Text, out int I);
                LeftInd.Value = I;
            }
        }

        private void RightIndent_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                int.TryParse(RightIndent.Text, out int I);
                RightInd.Value = I;
            }
        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {
            BackColorBox.Open();
        }

        private async void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
            await resourceInfos[0].StartSuspendAsync();
            await Launcher.LaunchUriAsync(new Uri("ms-screenclip:///"));
        }

        private void TableInsertBox_CloseButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void FindREB_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void FindREB_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                FindREB.IsReadOnly = false;
                REB.Document.Selection.SetRange(FindREB.Document.Selection.StartPosition, REB.Document.Selection.EndPosition);
                REB.Focus(FocusState.Pointer);
                FindFlyout.Hide();
            }
        }

        private void Flyout_Closing(FlyoutBase sender, FlyoutBaseClosingEventArgs args)
        {
            FindREB.IsReadOnly = false;
        }

        private void DialogWindow_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsHelper.SetSetting("ZippyAPIKey", ZippyAPIKey.Password.ToString());
            ZippyKeyWindow.Close();
        }

        private void DialogWindow_SecondButtonClick(object sender, RoutedEventArgs e)
        {
            URIHelper.LaunchURI("https://platform.openai.com/api-keys");
            ZippyKeyWindow.Close();
        }

        private void APIButton_Click(object sender, RoutedEventArgs e)
        {
            ZippyKeyWindow.Open();
        }

        private void DiscordItem_Click(object sender, RoutedEventArgs e)
        {
            URIHelper.LaunchURI("https://discord.com/invite/uasSwW5U2B");
        }

        private void TwitterItem_Click(object sender, RoutedEventArgs e)
        {
            URIHelper.LaunchURI("https://twitter.com/IviriusOfficial");
        }

        private async void NewFileBox_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            if (TXTFile == null)
            {
                await SaveFile(false, false, true, false);
            }
            if (TXTFile != null)
            {
                await SaveFile(true, false, true, false);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            URIHelper.LaunchURI("https://apps.microsoft.com/detail/9N4T9H9182J5?hl=en-US&gl=US");
        }
    }
}