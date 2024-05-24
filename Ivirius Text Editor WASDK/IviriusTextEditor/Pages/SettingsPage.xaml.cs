using IviriusTextEditor.Core.Helpers;
using IviriusTextEditor.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
//using ColorHelper = CommunityToolkit.WinUI.Helpers.ColorHelper;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IviriusTextEditor.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            ElementThemeBox.Items.Add("Light");
            ElementThemeBox.Items.Add("Dark");
            ThemeBox.Items.Add("Light");
            ThemeBox.Items.Add("Dark");

            LangBox.Items.Add("English (en-us)");
            LangBox.Items.Add("Română (ro-ro)");
            LangBox.Items.Add("Polski (pl-pl)");

            #region SettingsComponents

            if (SettingsHelper.GetSettingString("Language") == null)
            {
                SettingsHelper.SetSetting("Language", "en-us");
                StringTable.ReadLanguage();
                LangBox.SelectedItem = "English (en-us)";
            }
            if (SettingsHelper.GetSettingString("Language") == "en-us")
            {
                StringTable.ReadLanguage();
                LangBox.SelectedItem = "English (en-us)";
            }
            if (SettingsHelper.GetSettingString("Language") == "ro-ro")
            {
                StringTable.ReadLanguage();
                LangBox.SelectedItem = "Română (ro-ro)";
            }
            if (SettingsHelper.GetSettingString("Language") == "pl-pl")
            {
                StringTable.ReadLanguage();
                LangBox.SelectedItem = "Polski (pl-pl)";
            }

            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

            LocalSettings.Values.Remove("TextColor");

            //Color settings
            if (SettingsHelper.GetSettingString("ElemTheme") != null)
            {
                if (SettingsHelper.GetSettingString("ElemTheme") == "Light")
                    ElementThemeBox.SelectedItem = "Light";
                if (SettingsHelper.GetSettingString("ElemTheme") == "Dark")
                    ElementThemeBox.SelectedItem = "Dark";
            }
            else
            {
                ElementThemeBox.SelectedItem = "Light";
                SettingsHelper.SetSetting("ElemTheme", "Light");
            }

            //Theme settings
            if (!(LocalSettings.Values["Theme"] == null))
            {
                if ((string)LocalSettings.Values["Theme"] == "Light")
                {
                    ThemeBox.SelectedItem = "Legacy Light";
                }
                if ((string)LocalSettings.Values["Theme"] == "Full Dark")
                {
                    ThemeBox.SelectedItem = "Legacy Dark";
                    RequestedTheme = ElementTheme.Dark;
                }
                if ((string)LocalSettings.Values["Theme"] == "Nostalgic Windows")
                {
                    ThemeBox.SelectedItem = "Acrylic";
                }
                if ((string)LocalSettings.Values["Theme"] == "Acrylic")
                {
                    ThemeBox.SelectedItem = "Acrylic Glass";
                }
                if ((string)LocalSettings.Values["Theme"] == "Luna")
                {
                    ThemeBox.SelectedItem = "Luna";
                }
                if ((string)LocalSettings.Values["Theme"] == "Old")
                {
                    ThemeBox.SelectedItem = "Old";
                }
                if ((string)LocalSettings.Values["Theme"] == "Custom")
                {
                    ThemeBox.SelectedItem = "Custom";
                }
                if ((string)LocalSettings.Values["Theme"] == "Mica Light")
                {
                    ThemeBox.SelectedItem = "Light";
                }
                if ((string)LocalSettings.Values["Theme"] == "Mica Dark")
                {
                    ThemeBox.SelectedItem = "Dark";
                    RequestedTheme = ElementTheme.Dark;
                }
            }
            else
            {
                LocalSettings.Values["Theme"] = "Light";
                ThemeBox.SelectedItem = "Legacy Light";
            }

            //Accent border settings
            if (!(LocalSettings.Values["AccentBorder"] == null))
            {
                if ((string)LocalSettings.Values["AccentBorder"] == "On")
                {
                    AccentBorderToggle.IsChecked = true;
                }
                if ((string)LocalSettings.Values["AccentBorder"] == "Off")
                {
                    AccentBorderToggle.IsChecked = false;
                }
            }
            else
            {
                LocalSettings.Values["AccentBorder"] = "Off";
                AccentBorderToggle.IsChecked = false;
            }

            //New settings
            if (!(LocalSettings.Values["New"] == null))
            {
                if ((string)LocalSettings.Values["New"] == "On")
                {
                    NewToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["New"] == "Off")
                {
                    NewToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["New"] = "On";
                NewToggle.IsOn = true;
            }

            //Flat UI settings
            if (!(LocalSettings.Values["FlatUI"] == null))
            {
                if ((string)LocalSettings.Values["FlatUI"] == "On")
                {
                    FlatUIToggle.IsChecked = true;
                }
                if ((string)LocalSettings.Values["FlatUI"] == "Off")
                {
                    FlatUIToggle.IsChecked = false;
                }
            }
            else
            {
                LocalSettings.Values["FlatUI"] = "Off";
                FlatUIToggle.IsChecked = false;
            }

            //Open settings
            if (!(LocalSettings.Values["Open"] == null))
            {
                if ((string)LocalSettings.Values["Open"] == "On")
                {
                    OpenToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Open"] == "Off")
                {
                    OpenToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Open"] = "On";
                OpenToggle.IsOn = true;
            }

            //Print settings
            if (!(LocalSettings.Values["Print"] == null))
            {
                if ((string)LocalSettings.Values["Print"] == "On")
                {
                    PrintToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Print"] == "Off")
                {
                    PrintToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Print"] = "On";
                PrintToggle.IsOn = true;
            }

            //Changelog settings
            if (!(LocalSettings.Values["Changelog"] == null))
            {
                if ((string)LocalSettings.Values["Changelog"] == "On")
                {
                    ChangelogToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Changelog"] == "Off")
                {
                    ChangelogToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Changelog"] = "On";
                ChangelogToggle.IsOn = true;
            }

            //Cut settings
            if (!(LocalSettings.Values["Cut"] == null))
            {
                if ((string)LocalSettings.Values["Cut"] == "On")
                {
                    CutToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Cut"] == "Off")
                {
                    CutToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Cut"] = "On";
                CutToggle.IsOn = true;
            }

            //Copy settings
            if (!(LocalSettings.Values["Copy"] == null))
            {
                if ((string)LocalSettings.Values["Copy"] == "On")
                {
                    CopyToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Copy"] == "Off")
                {
                    CopyToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Copy"] = "On";
                CopyToggle.IsOn = true;
            }

            //Paste settings
            if (!(LocalSettings.Values["Paste"] == null))
            {
                if ((string)LocalSettings.Values["Paste"] == "On")
                {
                    PasteToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Paste"] == "Off")
                {
                    PasteToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Paste"] = "On";
                PasteToggle.IsOn = true;
            }

            //Delete settings
            if (!(LocalSettings.Values["Delete"] == null))
            {
                if ((string)LocalSettings.Values["Delete"] == "On")
                {
                    DeleteToggle.IsOn = true;
                }
                if ((string)LocalSettings.Values["Delete"] == "Off")
                {
                    DeleteToggle.IsOn = false;
                }
            }
            else
            {
                LocalSettings.Values["Delete"] = "On";
                DeleteToggle.IsOn = true;
            }

            //Home page settings
            if (!(LocalSettings.Values["HomePage"] == null))
            {
                if ((string)LocalSettings.Values["HomePage"] == "On")
                {
                    HomeToggle.IsChecked = true;
                }
                if ((string)LocalSettings.Values["HomePage"] == "Off")
                {
                    HomeToggle.IsChecked = false;
                }
            }
            else
            {
                LocalSettings.Values["HomePage"] = "Off";
                HomeToggle.IsChecked = false;
            }

            //Ruler settings
            if (!(LocalSettings.Values["Ruler"] == null))
            {
                if ((string)LocalSettings.Values["Ruler"] == "On")
                {
                    RulerToggle.IsChecked = true;
                }
                if ((string)LocalSettings.Values["Ruler"] == "Off")
                {
                    RulerToggle.IsChecked = false;
                }
            }
            else
            {
                LocalSettings.Values["Ruler"] = "On";
                RulerToggle.IsChecked = true;
            }

            //Status bar settings
            if (!(LocalSettings.Values["StatusBar"] == null))
            {
                if ((string)LocalSettings.Values["StatusBar"] == "On")
                {
                    StatusBarToggle.IsChecked = true;
                }
                if ((string)LocalSettings.Values["StatusBar"] == "Off")
                {
                    StatusBarToggle.IsChecked = false;
                }
            }
            else
            {
                LocalSettings.Values["StatusBar"] = "On";
                StatusBarToggle.IsChecked = true;
            }

            //DEV settings
            if (!(LocalSettings.Values["DEV"] == null))
            {
                if ((string)LocalSettings.Values["DEV"] == "On")
                {
                    DevToggle.IsChecked = true;
                }
                if ((string)LocalSettings.Values["DEV"] == "Off")
                {
                    DevToggle.IsChecked = false;
                }
            }
            else
            {
                LocalSettings.Values["DEV"] = "Off";
            }

            //EXP settings
            if (!(LocalSettings.Values["Experiment #5924"] == null))
            {
                if ((bool)LocalSettings.Values["Experiment #5924"] == true)
                {
                    PrivacySection.Visibility = Visibility.Visible;
                }
            }
            else
            {
                LocalSettings.Values["Experiment #5924"] = false;
            }

            #endregion SettingsComponents
        }

        private void SSButton_Click_1(object Sender, RoutedEventArgs EvArgs)
        {
            ActionWarningBox.Open();
        }

        string RestartArgs;

        private async void SettingsSaveButton_Click(object Sender, RoutedEventArgs EvArgs)
        {
            if ((string)LangBox.SelectedItem == "English (en-us)")
            {
                SettingsHelper.SetSetting("Language", "en-us");
            }
            if ((string)LangBox.SelectedItem == "Română (ro-ro)")
            {
                SettingsHelper.SetSetting("Language", "ro-ro");
            }
            if ((string)LangBox.SelectedItem == "Polski (pl-pl)")
            {
                SettingsHelper.SetSetting("Language", "pl-pl");
            }
            ApplicationDataContainer LS = ApplicationData.Current.LocalSettings;
            if (ThemeBox != null)
            {
                if ((string)ThemeBox.SelectedItem == "Legacy Light")
                {
                    LS.Values["Theme"] = "Light";
                }
                if ((string)ThemeBox.SelectedItem == "Legacy Dark")
                {
                    LS.Values["Theme"] = "Full Dark";
                }
                if ((string)ThemeBox.SelectedItem == "Acrylic")
                {
                    LS.Values["Theme"] = "Nostalgic Windows";
                }
                if ((string)ThemeBox.SelectedItem == "Acrylic Glass")
                {
                    LS.Values["Theme"] = "Acrylic";
                }
                if ((string)ThemeBox.SelectedItem == "Luna")
                {
                    LS.Values["Theme"] = "Luna";
                }
                if ((string)ThemeBox.SelectedItem == "Old")
                {
                    LS.Values["Theme"] = "Old";
                }
                if ((string)ThemeBox.SelectedItem == "Custom")
                {
                    LS.Values["Theme"] = "Custom";
                }
                if ((string)ThemeBox.SelectedItem == "Light")
                {
                    LS.Values["Theme"] = "Mica Light";
                }
                if ((string)ThemeBox.SelectedItem == "Dark")
                {
                    LS.Values["Theme"] = "Mica Dark";
                }
            }
            if (AccentBorderToggle != null)
            {
                if (AccentBorderToggle.IsChecked == true)
                {
                    LS.Values["AccentBorder"] = "On";
                }
                else
                {
                    LS.Values["AccentBorder"] = "Off";
                }
            }
            if (ChangelogToggle != null)
            {
                if (ChangelogToggle.IsOn == true)
                {
                    LS.Values["Changelog"] = "On";
                }
                else
                {
                    LS.Values["Changelog"] = "Off";
                }
            }
            if (StatusBarToggle != null)
            {
                if (StatusBarToggle.IsChecked == true)
                {
                    LS.Values["StatusBar"] = "On";
                }
                else
                {
                    LS.Values["StatusBar"] = "Off";
                }
            }
            else
            {
                LS.Values["StatusBar"] = "On";
            }
            if (CutToggle != null)
            {
                if (CutToggle.IsOn == true)
                {
                    LS.Values["Cut"] = "On";
                }
                else
                {
                    LS.Values["Cut"] = "Off";
                }
            }
            else
            {
                LS.Values["Cut"] = "On";
            }
            if (CopyToggle != null)
            {
                if (CopyToggle.IsOn == true)
                {
                    LS.Values["Copy"] = "On";
                }
                else
                {
                    LS.Values["Copy"] = "Off";
                }
            }
            else
            {
                LS.Values["Copy"] = "On";
            }
            if (PasteToggle != null)
            {
                if (PasteToggle.IsOn == true)
                {
                    LS.Values["Paste"] = "On";
                }
                else
                {
                    LS.Values["Paste"] = "Off";
                }
            }
            else
            {
                LS.Values["Paste"] = "On";
            }
            if (DeleteToggle != null)
            {
                if (DeleteToggle.IsOn == true)
                {
                    LS.Values["Delete"] = "On";
                }
                else
                {
                    LS.Values["Delete"] = "Off";
                }
            }
            else
            {
                LS.Values["Delete"] = "On";
            }
            if (NewToggle != null)
            {
                if (NewToggle.IsOn == true)
                {
                    LS.Values["New"] = "On";
                }
                else
                {
                    LS.Values["New"] = "Off";
                }
            }
            else
            {
                LS.Values["New"] = "Off";
            }
            if (OpenToggle != null)
            {
                if (OpenToggle.IsOn == true)
                {
                    LS.Values["Open"] = "On";
                }
                else
                {
                    LS.Values["Open"] = "Off";
                }
            }
            else
            {
                LS.Values["Open"] = "Off";
            }
            if (PrintToggle != null)
            {
                if (PrintToggle.IsOn == true)
                {
                    LS.Values["Print"] = "On";
                }
                else
                {
                    LS.Values["Print"] = "Off";
                }
            }
            else
            {
                LS.Values["Print"] = "Off";
            }
            if (HomeToggle != null)
            {
                if (HomeToggle.IsChecked == true)
                {
                    LS.Values["HomePage"] = "On";
                }
                if (HomeToggle.IsChecked == false)
                {
                    LS.Values["HomePage"] = "Off";
                }
            }
            else
            {
                LS.Values["HomePage"] = "Off";
            }
            if (RulerToggle != null)
            {
                if (RulerToggle.IsChecked == true)
                {
                    LS.Values["Ruler"] = "On";
                }
                if (RulerToggle.IsChecked == false)
                {
                    LS.Values["Ruler"] = "Off";
                }
            }
            else
            {
                LS.Values["Ruler"] = "On";
            }
            if (DevToggle.IsChecked == true)
            {
                LS.Values["DEV"] = "On";
            }
            if (DevToggle.IsChecked == false)
            {
                LS.Values["DEV"] = "Off";
            }
            if (ToolbarColor != null)
            {
                LS.Values["ToolbarColor"] = ToolbarColor.Color.ToString();
                LS.Values["ToolbarColorOpacity"] = O.Value;
                LS.Values["ToolbarColorLumOpacity"] = LO.Value;
            }
            if (BKGColor != null)
            {
                LS.Values["BKGColor"] = BKGColor.Color.ToString();
                LS.Values["BKGColorOpacity"] = BKGO.Value;
                LS.Values["BKGColorLumOpacity"] = BKGLO.Value;
            }
            if (TBColor != null)
            {
                LS.Values["BKGColorTB"] = TBColor.Color.ToString();
                LS.Values["BKGColorTBOpacity"] = TBO.Value;
                LS.Values["BKGColorTBLumOpacity"] = TBLO.Value;
            }
            if (ElementThemeBox != null)
            {
                if ((string)ElementThemeBox.SelectedItem == "Light")
                    LS.Values["ElemTheme"] = "Light";
                if ((string)ElementThemeBox.SelectedItem == "Dark")
                    LS.Values["ElemTheme"] = "Dark";
            }
            if (FlatUIToggle.IsChecked == true) LS.Values["FlatUI"] = "On";
            if (FlatUIToggle.IsChecked == false) LS.Values["FlatUI"] = "Off";

            RestartArgs = "e";
            _ = await CoreApplication.RequestRestartAsync(RestartArgs);
        }

        private void ThemeBox_SelectionChanged(object Sender, SelectionChangedEventArgs EvArgs)
        {
            if ((string)ThemeBox.SelectedItem == "Full Dark")
            {
                CustomThemePanel.Visibility = Visibility.Collapsed;
            }
            if ((string)ThemeBox.SelectedItem == "Custom")
            {
                CustomThemePanel.Visibility = Visibility.Visible;
            }
            else if ((string)ThemeBox.SelectedItem is not "Full Dark" and not "Custom")
            {
                CustomThemePanel.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private async void ActionWarningBox_FirstButtonClick(object sender, RoutedEventArgs e)
        {
            RestartArgs = "e";
            ApplicationDataContainer LS = ApplicationData.Current.LocalSettings;
            foreach (KeyValuePair<string, object> item in LS.Values.ToList())
            {
                LS.Values.Remove(item.Key);
            }
            _ = await CoreApplication.RequestRestartAsync(RestartArgs);
        }
    }
}
