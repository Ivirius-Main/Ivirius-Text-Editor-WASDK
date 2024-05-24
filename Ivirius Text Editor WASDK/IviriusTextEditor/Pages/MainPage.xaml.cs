using IviriusTextEditor.Core.Helpers;
using IviriusTextEditor.Languages;
//using MicaForUWP.Media;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Microsoft.Windows.System;
using Windows.UI;
using Microsoft.UI.Composition;
using Windows.UI.Core.Preview;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using AcrylicBrush = Microsoft.UI.Xaml.Media.AcrylicBrush;
using Windows.System;
using Ivirius_Text_Editor;

namespace IviriusTextEditor.Pages
{
    public sealed partial class MainPage : Page
    {
        DispatcherTimer DT = new();

        public MainPage()
        {
            StringTable.ReadLanguage();

            InitializeComponent();

            //Variables
            //var CoreTB = CoreApplication.GetCurrentView().TitleBar;
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            //var AppTB = ApplicationView.GetForCurrentView().TitleBar;
            var LocalSettings = ApplicationData.Current.LocalSettings;

            //Title bar
            //CoreTB.ExtendViewIntoTitleBar = true;
            //CoreTB.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            //AppTB.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
            //AppTB.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
            //AppTB.ButtonInactiveForegroundColor = Color.FromArgb(255, 125, 125, 125);
            //CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            //SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += MainPage_CloseRequested;
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Windows.Foundation.Size(800, 400));
            //App.Window.SetTitleBar(CustomDragRegion);

            //Others
            try
            {
                DT.Interval = new TimeSpan(0, 0, (int)0.1);
                DT.Tick += DT_Tick;
                DT.Start();
            }
            catch { }
            TabbedView.TabItems.Clear();

            //Settings

            //Accent border settings
            if (SettingsHelper.GetSettingString("AccentBorder") == null)
            {
                SettingsHelper.SetSetting("AccentBorder", "Off");
                AccentBorder.Visibility = Visibility.Collapsed;
            }
            if (SettingsHelper.GetSettingString("AccentBorder") == "On")
            {
                AccentBorder.Visibility = Visibility.Visible;
            }
            if (SettingsHelper.GetSettingString("AccentBorder") == "Off")
            {
                AccentBorder.Visibility = Visibility.Collapsed;
            }

            //Theme settings
            if (SettingsHelper.GetSettingString("Theme") == null)
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }

            //Disabled themes
            if (SettingsHelper.GetSettingString("Theme") == "Custom")
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Dark")
            {
                SettingsHelper.SetSetting("Theme", "Mica Dark");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Light")
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Full Dark")
            {
                SettingsHelper.SetSetting("Theme", "Mica Dark");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Nostalgic Windows")
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Acrylic")
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Luna")
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }
            if (SettingsHelper.GetSettingString("Theme") == "Old")
            {
                SettingsHelper.SetSetting("Theme", "Mica Light");
            }

            //Mica light theme
            if (SettingsHelper.GetSettingString("Theme") == "Mica Light")
            {
                //Text Microsoft.UI.Colors
                Application.Current.Resources["AppTextBrush"] = Application.Current.Resources["TextFillColorPrimaryBrush"];

                //TitleBar buttons
                //AppTB.ButtonForegroundColor = Microsoft.UI.Colors.Black;
                //AppTB.ButtonHoverBackgroundColor = Color.FromArgb(25, 0, 0, 0);
                //AppTB.ButtonPressedBackgroundColor = Color.FromArgb(65, 0, 0, 0);
                //AppTB.ButtonHoverForegroundColor = Microsoft.UI.Colors.Black;
                //AppTB.ButtonPressedForegroundColor = Microsoft.UI.Colors.Black;

                ////TitleBar color
                //Application.Current.Resources["AppTitleBarBrush"] = new BackdropMicaBrush()
                //{
                //    LuminosityOpacity = 0.8F,
                //    TintOpacity = 0F,
                //    BackgroundSource = BackgroundSource.WallpaperBackdrop,
                //    Opacity = 1,
                //    TintColor = Color.FromArgb(255, 230, 230, 230),
                //    FallbackColor = Color.FromArgb(255, 230, 230, 230)
                //};
                //MainPageComponent.Background = (Brush)Application.Current.Resources["AppTitleBarBrush"];

                ////Tab color
                //Application.Current.Resources["AppBackgroundBrush"] = new BackdropMicaBrush()
                //{
                //    LuminosityOpacity = 0.9F,
                //    TintOpacity = 0F,
                //    BackgroundSource = BackgroundSource.WallpaperBackdrop,
                //    Opacity = 1,
                //    TintColor = Color.FromArgb(255, 255, 255, 255),
                //    FallbackColor = Color.FromArgb(255, 255, 255, 255)
                //};

                //Application.Current.Resources["TabViewItemHeaderBackgroundSelected"] = Application.Current.Resources["AppBackgroundBrush"];

                //Final settings
                RequestedTheme = ElementTheme.Light;
            }

            //Mica dark theme
            if (SettingsHelper.GetSettingString("Theme") == "Mica Dark")
            {
                //Text Microsoft.UI.Colors
                Application.Current.Resources["AppTextBrush"] = Application.Current.Resources["TextFillColorPrimaryBrush"];

                //TitleBar buttons
                //AppTB.ButtonForegroundColor = Microsoft.UI.Colors.White;
                //AppTB.ButtonHoverBackgroundColor = Color.FromArgb(25, 0, 0, 0);
                //AppTB.ButtonPressedBackgroundColor = Color.FromArgb(65, 0, 0, 0);
                //AppTB.ButtonHoverForegroundColor = Microsoft.UI.Colors.White;
                //AppTB.ButtonPressedForegroundColor = Microsoft.UI.Colors.White;

                ////TitleBar color
                //Application.Current.Resources["AppTitleBarBrush"] = new BackdropMicaBrush()
                //{
                //    LuminosityOpacity = 1F,
                //    TintOpacity = 0.3F,
                //    BackgroundSource = BackgroundSource.WallpaperBackdrop,
                //    Opacity = 1,
                //    TintColor = Color.FromArgb(255, 13, 13, 13),
                //    FallbackColor = Color.FromArgb(255, 13, 13, 13)
                //};
                //MainPageComponent.Background = (Brush)Application.Current.Resources["AppTitleBarBrush"];

                ////Tab color
                //Application.Current.Resources["AppBackgroundBrush"] = new BackdropMicaBrush()
                //{
                //    LuminosityOpacity = 1F,
                //    TintOpacity = 0.8F,
                //    BackgroundSource = BackgroundSource.WallpaperBackdrop,
                //    Opacity = 1,
                //    TintColor = Color.FromArgb(255, 33, 33, 33),
                //    FallbackColor = Color.FromArgb(255, 33, 33, 33)
                //};

                //Application.Current.Resources["TabViewItemHeaderBackgroundSelected"] = Application.Current.Resources["AppBackgroundBrush"];

                //Final settings
                RequestedTheme = ElementTheme.Dark;
            }
        }

        private void DT_Tick(object sender, object e)
        {
            IList<object> TabItems = TabbedView.TabItems;
            foreach (TabViewItem TabItem in TabItems.Cast<TabViewItem>())
            {
                if ((TabItem.Content as Frame).Content is TabbedMainPage)
                {
                    if (((TabItem.Content as Frame).Content as TabbedMainPage) != null)
                    {
                        string FileName = ((TabItem.Content as Frame).Content as TabbedMainPage).FileNameTextBlock.Text;
                        TabItem.Header = FileName;
                    }
                }
            }
        }

        public async void Shutdown()
        {
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            await ApplicationView.GetForCurrentView().TryConsolidateAsync();
        }

        private void MainPage_CloseRequested(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;

            if (TabbedView.TabItems.Count > 1)
            {
                CloseWarningBox1.Open();
                CloseWarningBox1.Title = "Ivirius Text Editor";
                CWFContent1.Text = "Are you sure you want to close?";
            }
            if (TabbedView.TabItems.Count == 1)
            {
                SysSender = TabbedView;
                var TabItem = (TabViewItem)SysSender.TabItems[0];
                if ((TabItem.Content as Frame).Content is TabbedMainPage)
                {
                    if (((TabItem.Content as Frame).Content as TabbedMainPage).isWorkSaved == false)
                    {
                        CloseWarningBox2.Open();
                        CloseWarningBox2.Title = "Ivirius Text Editor";
                        CWFContent2.Text = "Do you want to save your file?";
                    }
                    else
                    {
                        TabbedView.TabItems.Clear();
                        Shutdown();
                    }
                }
                else Shutdown();
            }
        }

        public void RemoveCurrentTab(TabView Sender, TabViewTabCloseRequestedEventArgs EvArgs)
        {
            SysArgs = EvArgs;
            SysSender = Sender;

            if ((SysArgs.Tab.Content as Frame).Content is TabbedMainPage)
            {
                if (((SysArgs.Tab.Content as Frame).Content as TabbedMainPage).isWorkSaved == false)
                {
                    CloseWarningBox3.Open();
                    CloseWarningBox3.Title = "Ivirius Text Editor";
                    CWFContent3.Text = "Do you want to save your file?";
                }
                else
                {
                    _ = SysSender.TabItems.Remove(SysArgs.Tab);
                    if (TabbedView.TabItems.Count == 0) Shutdown();
                }
            }
            else
            {
                _ = SysSender.TabItems.Remove(SysArgs.Tab);
                if (TabbedView.TabItems.Count == 0) Shutdown();
            }
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            CloseWarningBox1.Close();
            CloseWarningBox2.Close();
            CloseWarningBox3.Close();
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            CustomDragRegion.Height = ShellTitlebarInset.Height = sender.Height;
        }

        private void TabView_AddTabButtonClick(TabView sender, object args)
        {
            sender.TabItems.Add(CreateNewTab());
            TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;
        }

        protected override void OnNavigatedTo(NavigationEventArgs EvArgs)
        {
            //Catch file
            base.OnNavigatedTo(EvArgs);
            if (EvArgs.Parameter is IActivatedEventArgs Args && Args.Kind == ActivationKind.File)
            {
                try
                {
                    //Write file properties
                    var RF = new Frame();
                    _ = RF.Navigate(typeof(TabbedMainPage), Args);

                    var NewTabItem = new TabViewItem
                    {
                        IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                        Content = RF,
                        Header = "New Tab"
                    };
                    TabbedView.TabItems.Add(NewTabItem);
                }
                catch
                {

                }
            }
        }

        public void BeginFullHelpButtonsForNewTab()
        {
            var RF = new Frame();
            _ = RF.Navigate(typeof(TabbedMainPage));

            var NewTabItem = new TabViewItem
            {
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                Content = RF,
                Header = "New Tab"
            };

            TabbedView.TabItems.Add(NewTabItem);
            TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;

            var X = RF.Content as TabbedMainPage;
            X.BeginFullHelpButtons();
        }

        public void LaunchSettingsTab()
        {
            var RF = new Frame();
            _ = RF.Navigate(typeof(SettingsPage));

            var NewTabItem = new TabViewItem
            {
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Setting },
                Content = RF,
                Header = "Settings"
            };

            TabbedView.TabItems.Add(NewTabItem);
            TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;
        }

        public void BeginFullHelpForNewTab()
        {
            var RF = new Frame();
            _ = RF.Navigate(typeof(TabbedMainPage));

            var NewTabItem = new TabViewItem
            {
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                Content = RF,
                Header = "New Tab"
            };

            TabbedView.TabItems.Add(NewTabItem);
            TabbedView.UpdateLayout();
            TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;

            var X = RF.Content as TabbedMainPage;
            X.BeginFullHelp();
        }

        public void BeginHandwritingForNewTab()
        {
            var RF = new Frame();
            _ = RF.Navigate(typeof(TabbedMainPage));

            var NewTabItem = new TabViewItem
            {
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                Content = RF,
                Header = "New Tab"
            };

            TabbedView.TabItems.Add(NewTabItem);
            TabbedView.UpdateLayout();
            TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;

            var X = RF.Content as TabbedMainPage;
            X.OpenHW();
        }

        public void AddTabForFile(FileActivatedEventArgs EvArgs)
        {
            try
            {
                var RF = new Frame();
                _ = RF.Navigate(typeof(TabbedMainPage), EvArgs);

                var NewTabItem = new TabViewItem
                {
                    IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                    Content = RF,
                    Header = "New Tab"
                };

                TabbedView.TabItems.Add(NewTabItem);
                TabbedView.UpdateLayout();
                TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;
            }
            catch
            {

            }
        }

        public async Task AddTabForFile(StorageFile File)
        {
            try
            {
                var RF = new Frame();
                _ = RF.Navigate(typeof(TabbedMainPage));

                var NewTabItem = new TabViewItem
                {
                    IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                    Content = RF,
                    Header = "New Tab"
                };

                TabbedView.TabItems.Add(NewTabItem);
                TabbedView.UpdateLayout();
                TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;

                await (RF.Content as TabbedMainPage).ReadFile(File);
            }
            catch
            {

            }
        }

        public Task AddExternalTabAsync()
        {
            TabbedView.TabItems.Add(CreateNewTab());
            TabbedView.SelectedIndex = TabbedView.TabItems.Count - 1;
            return Task.CompletedTask;
        }

        public TabViewItem CreateNewTab()
        {
            var RF = new Frame();
            _ = RF.Navigate(typeof(TabbedMainPage));

            var NewTabItem = new TabViewItem
            {
                Header = "New Tab",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document },
                Content = RF
            };

            TabbedView.UpdateLayout();
            TabbedView.SelectedIndex = TabbedView.TabItems.Count;

            return NewTabItem;
        }

        private void TabbedView_Loaded(object sender, RoutedEventArgs e)
        {
            var S = sender as TabView;
            if (S.TabItems.Count == 0) S.TabItems.Add(CreateNewTab());
        }

        TabView SysSender;

        TabViewTabCloseRequestedEventArgs SysArgs;

        private void TabbedView_TabCloseRequested(TabView Sender, TabViewTabCloseRequestedEventArgs EvArgs)
        {
            RemoveCurrentTab(Sender, EvArgs);
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            _ = await Launcher.LaunchUriAsync(new Uri("https://ivirius.webnode.page/experimental-options-terms-and-conditions/"));
        }

        private void TabbedView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabbedView.AllowDropTabs = true;
            _ = TabbedView.Focus(FocusState.Pointer);
            TabbedView.IsEnabled = true;
        }

        async void XClick(object sender2, RoutedEventArgs e2)
        {
            CWFBtn4.Visibility = Visibility.Collapsed;
            await (((TabbedView.SelectedItem as TabViewItem).Content as Frame).Content as TabbedMainPage).SaveFile(false, false, false, false);
            CloseWarningFlyoutHost.Flyout.Hide();
            CWFBtn1.Click -= XClick;
        }

        async void XClick2(object sender2, RoutedEventArgs e2)
        {
            CloseWarningFlyoutHost.Flyout.Hide();
            CWFBtn4.Visibility = Visibility.Collapsed;
            CWFBtn2.Click -= XClick2;
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            _ = await ApplicationView.GetForCurrentView().TryConsolidateAsync();
        }

        void XClick3(object sender2, RoutedEventArgs e2)
        {
            CloseWarningFlyoutHost.Flyout.Hide();
            CWFBtn4.Visibility = Visibility.Collapsed;
            CWFBtn3.Click -= XClick3;
        }

        async void XClick4(object sender2, RoutedEventArgs e2)
        {
            CloseWarningFlyoutHost.Flyout.Hide();
            CWFBtn4.Visibility = Visibility.Collapsed;
            CWFBtn4.Click -= XClick4;
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            _ = await ApplicationView.GetForCurrentView().TryConsolidateAsync();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            bool FS = ApplicationView.GetForCurrentView().IsFullScreenMode;
            switch (FS)
            {
                case false:
                    // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
                    _ = ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                    break;
                default:
                    // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
                    ApplicationView.GetForCurrentView().ExitFullScreenMode();
                    break;
            }
        }

        private async void Button_Click_188(object sender, RoutedEventArgs e)
        {
            IList<AppDiagnosticInfo> infos = await AppDiagnosticInfo.RequestInfoForAppAsync();
            IList<AppResourceGroupInfo> resourceInfos = infos[0].GetResourceGroups();
            await resourceInfos[0].StartSuspendAsync();
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            SetupCloseBox.Open();
        }

        private async void SetupCloseBox_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            var Win = ApplicationView.GetForCurrentView();
            await Win.TryConsolidateAsync();
        }

        private async void CloseWarningBox1_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            _ = await ApplicationView.GetForCurrentView().TryConsolidateAsync();
            CloseWarningBox1.Close();
        }

        private async void CloseWarningBox2_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            await (((TabbedView.SelectedItem as TabViewItem).Content as Frame).Content as TabbedMainPage).SaveFile(false, false, false, false);
            CloseWarningBox2.Close();
        }

        private async void CloseWarningBox2_SecondButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
            _ = await ApplicationView.GetForCurrentView().TryConsolidateAsync();
            CloseWarningBox2.Close();
        }

        private async void CloseWarningBox3_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            await ((SysArgs.Tab.Content as Frame).Content as TabbedMainPage).SaveFile(false, false, false, false);
            CloseWarningBox3.Close();
        }

        private async void CloseWarningBox3_SecondButtonClick(object sender, RoutedEventArgs e)
        {
            _ = SysSender.TabItems.Remove(SysArgs.Tab);
            if (TabbedView.TabItems.Count == 0) // TODO Windows.UI.ViewManagement.ApplicationView is no longer supported. Use Microsoft.UI.Windowing.AppWindow instead. For more details see https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/windowing
_ = await ApplicationView.GetForCurrentView().TryConsolidateAsync();
            CloseWarningBox3.Close();
        }

        private void MainPageComponent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.Window.SetTitleBar(CustomDragRegion);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
