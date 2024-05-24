//using MicaForUWP.Media;
using CommunityToolkit.WinUI.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace Ivirius.UI.Windowing
{
    public partial class DialogWindow : UserControl
    {
        private bool IsDesignTime
        {
            get
            {
#if DEBUG
                return Windows.ApplicationModel.DesignMode.DesignModeEnabled;
#else
            return false;
#endif
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsDesignTime == false)
            {
                SetValue(DialogWindowVisibilityProperty, Visibility.Collapsed);
                Close();
            }
            else
            {
                SetValue(DialogWindowVisibilityProperty, Visibility.Visible);
            }
            var TL = new ThemeListener();
            if (TL.CurrentTheme == ApplicationTheme.Light)
            {
                //ABrush.TintColor = Microsoft.UI.Colors.White;
                //ABrush.FallbackColor = Microsoft.UI.Colors.White;
            }
            else if (TL.CurrentTheme == ApplicationTheme.Dark)
            {
            //    ABrush.TintColor = Microsoft.UI.Colors.Black;
            //    ABrush.FallbackColor = Microsoft.UI.Colors.Black;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
            DataContext = this;
            if (Parent is DialogWindowContainer || Parent is null)
            {

            }
            else
            {
                throw new ArgumentException("The parent of DialogWindow must be a DialogWindowContainer");
            }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
        "Title", // The name of the property
        typeof(string), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata("Title") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        [Description("The title of the DialogWindow")]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty FirstButtonTextProperty =
        DependencyProperty.Register(
        "FirstButtonText", // The name of the property
        typeof(string), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata("Yes") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public string FirstButtonText
        {
            get { return (string)GetValue(FirstButtonTextProperty); }
            set { SetValue(FirstButtonTextProperty, value); }
        }

        public enum BackdropKind
        {
            Acrylic,
            Mica,
            MicaAlt
        }

        public static readonly DependencyProperty BackdropProperty =
        DependencyProperty.Register(
        "Backdrop", // The name of the property
        typeof(BackdropKind), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(BackdropKind.Mica, BackdropChange) // Default value
        );

        private static void BackdropChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).ChangeBackdrop();
        }

        private void ChangeBackdrop()
        {
            if (Backdrop == BackdropKind.Mica)
            {
                var TL = new ThemeListener();
                if (TL.CurrentTheme == ApplicationTheme.Light)
                {
                    
                }
                else if (TL.CurrentTheme == ApplicationTheme.Dark)
                {
                }
            }
            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
            if (Backdrop == BackdropKind.MicaAlt)
            {
                if ((string)LocalSettings.Values["Theme"] == "Custom" && (string)LocalSettings.Values["ElemTheme"] == "Light"
                    || (string)LocalSettings.Values["Theme"] == "Light"
                    || (string)LocalSettings.Values["Theme"] == "Mica Light"
                    || (string)LocalSettings.Values["Theme"] == "Dark"
                    || (string)LocalSettings.Values["Theme"] == "Nostalgic Windows"
                    || (string)LocalSettings.Values["Theme"] == "Acrylic"
                    || (string)LocalSettings.Values["Theme"] == "Luna"
                    || (string)LocalSettings.Values["Theme"] == "Old"
                    || (string)LocalSettings.Values["Theme"] == null)
                {
                    
                }
                else if ((string)LocalSettings.Values["Theme"] == "Custom" && (string)LocalSettings.Values["ElemTheme"] == "Dark"
                    || (string)LocalSettings.Values["Theme"] == "Full Dark"
                    || (string)LocalSettings.Values["Theme"] == "Mica Dark")
                {
                }
            }
            if (Backdrop == BackdropKind.Acrylic)
            {
                //TBAccent.Background = null;
                //var TL = new ThemeListener();
                //if (TL.CurrentTheme == ApplicationTheme.Light)
                //{
                //    TBar.Background = new AcrylicBrush()
                //    {
                //        TintColor = Microsoft.UI.Colors.White
                //    };
                //}
                //else if (TL.CurrentTheme == ApplicationTheme.Dark)
                //{
                //    TBar.Background = new AcrylicBrush()
                //    {
                //        TintColor = Color.FromArgb(255, 220, 220, 220)
                //    };
                //}
            }
        }

        [Browsable(true)]
        [Category("Common")]
        [Description("The backdrop of PrimaryWindow's title bar")]
        public BackdropKind Backdrop
        {
            get { return (BackdropKind)GetValue(BackdropProperty); }
            set { SetValue(BackdropProperty, value); }
        }

        public static readonly DependencyProperty SecondButtonTextProperty =
        DependencyProperty.Register(
        "SecondButtonText", // The name of the property
        typeof(string), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata("No") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public string SecondButtonText
        {
            get { return (string)GetValue(SecondButtonTextProperty); }
            set { SetValue(SecondButtonTextProperty, value); }
        }

        public static readonly DependencyProperty WindowBackgroundProperty =
        DependencyProperty.Register(
        "WindowBackground", // The name of the property
        typeof(Brush), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public Brush WindowBackground
        {
            get { return (Brush)GetValue(WindowBackgroundProperty); }
            set { SetValue(WindowBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CancelButtonTextProperty =
        DependencyProperty.Register(
        "CancelButtonText", // The name of the property
        typeof(string), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata("Cancel") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public string CancelButtonText
        {
            get { return (string)GetValue(CancelButtonTextProperty); }
            set { SetValue(CancelButtonTextProperty, value); }
        }

        public static readonly DependencyProperty DialogAppWindowWidthProperty =
        DependencyProperty.Register(
        "DialogAppWindowWidth", // The name of the property
        typeof(int), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(0) // Default value
        );

        [Browsable(true)]
        [Category("Layout")]
        public int DialogAppWindowWidth
        {
            get { return (int)GetValue(DialogAppWindowWidthProperty); }
            set { SetValue(DialogAppWindowWidthProperty, value); }
        }

        public static readonly DependencyProperty DialogAppWindowHeightProperty =
        DependencyProperty.Register(
        "DialogAppWindowHeight", // The name of the property
        typeof(int), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(0) // Default value
        );

        [Browsable(true)]
        [Category("Layout")]
        public int DialogAppWindowHeight
        {
            get { return (int)GetValue(DialogAppWindowHeightProperty); }
            set { SetValue(DialogAppWindowHeightProperty, value); }
        }

        public static new readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(
        "Content", // The name of the property
        typeof(UIElement), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(null) // Default value
        );

        public new UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public event RoutedEventHandler FirstButtonClick;

        public event RoutedEventHandler SecondButtonClick;

        public event RoutedEventHandler CancelButtonClick;

        public event RoutedEventHandler CloseButtonClick;

        public enum ButtonStyle
        {
            Win32,
            UWP
        };

        public enum TBStyle
        {
            Color,
            Transparent
        };

        public static readonly DependencyProperty CloseButtonStyleProperty =
        DependencyProperty.RegisterAttached(
        "CloseButtonStyleClick", // The name of the property
        typeof(ButtonStyle), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(ButtonStyle.Win32, OnCloseButtonStyleChanged) // Default value
        );

        private static void OnCloseButtonStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).SetValue();
        }

        private void SetValue()
        {
            if (CloseButtonStyle == ButtonStyle.Win32)
            {
                CloseButton.Width = 35;
            }
            if (CloseButtonStyle == ButtonStyle.UWP)
            {
                CloseButton.Width = 47;
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public ButtonStyle CloseButtonStyle
        {
            get { return (ButtonStyle)GetValue(CloseButtonStyleProperty); }
            set { SetValue(CloseButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty IsCloseButtonEnabledProperty =
        DependencyProperty.RegisterAttached(
        "IsCloseButtonEnabled", // The name of the property
        typeof(bool), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(true, CloseButtonEnabled) // Default value
        );

        public static readonly DependencyProperty TitleBarStyleProperty =
        DependencyProperty.RegisterAttached(
        "TitleBarStyle", // The name of the property
        typeof(TBStyle), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(TBStyle.Color, OnTBStyleChanged) // Default value
        );

        private static void OnTBStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).SetTBValue();
        }

        private void SetTBValue()
        {
            if (TitleBarStyle == TBStyle.Color)
            {

            }
            if (TitleBarStyle == TBStyle.Transparent)
            {
                //TBar.Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                //TBAccent.Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public TBStyle TitleBarStyle
        {
            get { return (TBStyle)GetValue(TitleBarStyleProperty); }
            set { SetValue(TitleBarStyleProperty, value); }
        }

        private static void CloseButtonEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).SetEnabledValue();
        }

        private void SetEnabledValue()
        {
            if (IsCloseButtonEnabled != false)
            {
                CloseButton.IsEnabled = true;
            }
            else
            {
                CloseButton.IsEnabled = false;
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            set { SetValue(IsCloseButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsSingleButtonProperty =
        DependencyProperty.RegisterAttached(
        "IsSingleButton", // The name of the property
        typeof(bool), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(false, IsSingleButtonChanged) // Default value
        );

        private static void IsSingleButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).SetSingleButtonValue();
        }

        private void SetSingleButtonValue()
        {
            if (IsSingleButton == true)
            {
                BTN2.Visibility = Visibility.Collapsed;
                BTN3.Visibility = Visibility.Collapsed;
            }
            else
            {
                BTN2.Visibility = Visibility.Visible;
                BTN3.Visibility = Visibility.Visible;
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public bool IsSingleButton
        {
            get { return (bool)GetValue(IsSingleButtonProperty); }
            set { SetValue(IsSingleButtonProperty, value); }
        }

        public static readonly DependencyProperty RememberPositionProperty =
        DependencyProperty.RegisterAttached(
        "RememberPosition", // The name of the property
        typeof(bool), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(true) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public bool RememberPosition
        {
            get { return (bool)GetValue(RememberPositionProperty); }
            set { SetValue(RememberPositionProperty, value); }
        }

        public static readonly DependencyProperty PlaySoundProperty =
        DependencyProperty.RegisterAttached(
        "PlaySound", // The name of the property
        typeof(bool), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(false) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public bool PlaySound
        {
            get { return (bool)GetValue(PlaySoundProperty); }
            set { SetValue(PlaySoundProperty, value); }
        }

        public static readonly DependencyProperty HasIconProperty =
        DependencyProperty.RegisterAttached(
        "HasIcon", // The name of the property
        typeof(bool), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(false, IconChanged) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public bool HasIcon
        {
            get { return (bool)GetValue(HasIconProperty); }
            set { SetValue(HasIconProperty, value); }
        }

        private static void IconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).DetectIconChange();
        }

        private void DetectIconChange()
        {
            if (HasIcon == true)
            {
                TitleBarIcon.Visibility = Visibility.Visible;
            }
            else
            {
                TitleBarIcon.Visibility = Visibility.Collapsed;
            }
        }

        private static void BitmapIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialogWindow)d).DetectBitmapIconChange();
        }

        private void DetectBitmapIconChange()
        {
            if (Icon != null)
            {
                TitleBarIcon.Source = Icon;
            }
            else
            {

            }
        }

        public static readonly DependencyProperty IconProperty =
        DependencyProperty.RegisterAttached(
        "Icon", // The name of the property
        typeof(ImageSource), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(null, BitmapIconChanged) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty DialogWindowVisibilityProperty =
        DependencyProperty.RegisterAttached(
        "DialogWindowVisibility", // The name of the property
        typeof(Visibility), // The type of the property
        typeof(DialogWindow), // The type of the owner class
        new PropertyMetadata(Visibility.Visible) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public Visibility DialogWindowVisibility
        {
            get { return (Visibility)GetValue(DialogWindowVisibilityProperty); }
            set { SetValue(DialogWindowVisibilityProperty, value); }
        }

        private Point lastPosition;
        private bool isDragging;

        private void DialogWindow_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            lastPosition = e.GetCurrentPoint(this).Position;
            (sender as Border).CapturePointer(e.Pointer);
            isDragging = true;
            var owner = Parent;
            if (owner is DialogWindowContainer SecWinHost)
            {
                uint lastIndex = (uint)(SecWinHost.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost.Children.IndexOf(this);

                SecWinHost.Children.Move(currentIndex, lastIndex);
            }
        }

        private void DialogWindow_PointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            isDragging = false;
            if (WindowBorder.Margin.Left < 0)
            {
                WindowBorder.Margin = new Thickness(0, WindowBorder.Margin.Top, 0, 0);
            }
            if (WindowBorder.Margin.Top < 0)
            {
                WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left, 0, 0, 0);
            }
            if (WindowBorder.Margin.Left > ActualWidth - WindowBorder.ActualWidth)
            {
                WindowBorder.Margin = new Thickness(ActualWidth - WindowBorder.ActualWidth, WindowBorder.Margin.Top, 0, 0);
            }
            if (WindowBorder.Margin.Top > ActualHeight - 35)
            {
                WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left, ActualHeight - 35, 0, 0);
            }
        }

        private void DialogWindow_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (e.Pointer.IsInContact && isDragging == true)
            {
                var newPosition = e.GetCurrentPoint(this).Position;
                var deltaX = newPosition.X - lastPosition.X;
                var deltaY = newPosition.Y - lastPosition.Y;

                var margin = WindowBorder.Margin;
                margin.Left += deltaX;
                margin.Top += deltaY;
                WindowBorder.Margin = margin;

                lastPosition = newPosition;
            }
        }

        private void Border_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            isDragging = false;
        }

        private void Border_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            isDragging = false;
        }

        private void WindowBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (DialogAppWindowWidth > WindowBorder.MinWidth)
            {
                BTN1.Width = (DialogAppWindowWidth - 55) / 3;
                BTN2.Width = (DialogAppWindowWidth - 55) / 3;
                BTN3.Width = (DialogAppWindowWidth - 55) / 3;
            }
            else if (WindowBorder.Width <= WindowBorder.MinWidth)
            {
                BTN1.Width = (300 - 55) / 3;
                BTN2.Width = (300 - 55) / 3;
                BTN3.Width = (300 - 55) / 3;
            }
        }

        protected void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstButtonClick != null)
                FirstButtonClick(this, new RoutedEventArgs());
            else
            {
                AllButtonsClickDefault();
            }
        }

        private void AllButtonsClickDefault()
        {
            Close();
        }

        private bool firstOpen = false;
        private bool isOn;

        public bool IsOn
        {
            get { return isOn; }
            private set
            {
                if (isOn != value)
                {
                    isOn = value;
                    UpdateParentBlockBorderVisibility();
                }
            }
        }

        public void SetMyProperty(bool value)
        {
            IsOn = value;
        }

        private void UpdateParentBlockBorderVisibility()
        {
            var owner = Parent;
            if (owner is DialogWindowContainer SecWinHost)
            {
                SecWinHost.SetBlockBorder();
            }
        }

        public void Open()
        {
            DialogWindowVisibility = Visibility.Visible;
            if (RememberPosition == true && firstOpen == false)
            {
                double X = FullBox.ActualWidth;
                double X2 = Math.Round((X / 2) - (WindowBorder.ActualWidth / 2));
                double Y = FullBox.ActualHeight;
                double Y2 = Math.Round((Y / 2) - (WindowBorder.ActualHeight / 2));

                WindowBorder.Margin = new Thickness(X2, Y2, 0, 0);
                WindowBorder.HorizontalAlignment = HorizontalAlignment.Left;
                WindowBorder.VerticalAlignment = VerticalAlignment.Top;
                firstOpen = true;
            }
            if (RememberPosition == false)
            {
                double X = FullBox.ActualWidth;
                double X2 = Math.Round((X / 2) - (WindowBorder.ActualWidth / 2));
                double Y = FullBox.ActualHeight;
                double Y2 = Math.Round((Y / 2) - (WindowBorder.ActualHeight / 2));

                WindowBorder.Margin = new Thickness(X2, Y2, 0, 0);
                WindowBorder.HorizontalAlignment = HorizontalAlignment.Left;
                WindowBorder.VerticalAlignment = VerticalAlignment.Top;
                firstOpen = true;
            }
            if (PlaySound == true)
            {
                var mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("C:\\Windows\\Media\\Windows Background.wav"));
                mediaPlayer.Play();
            }
            IsOn = true;
            var owner = Parent;
            if (owner is DialogWindowContainer SecWinHost)
            {
                uint lastIndex = (uint)(SecWinHost.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost.Children.IndexOf(this);

                SecWinHost.Children.Move(currentIndex, lastIndex);
            }
            if (owner is ChildAppWindowContainer PriWinHost)
            {
                uint lastIndex = (uint)(PriWinHost.Children.Count - 1);
                uint currentIndex = (uint)PriWinHost.Children.IndexOf(this);

                PriWinHost.Children.Move(currentIndex, lastIndex);
            }
            var compositor = ElementCompositionPreview.GetElementVisual(ShadowBorder).Compositor;

            // create a red sprite visual
            var myVisual = compositor.CreateSpriteVisual();
            myVisual.Brush = compositor.CreateColorBrush(Microsoft.UI.Colors.Transparent);
            if (WindowBorder.Margin.Top > ActualHeight - DialogAppWindowHeight)
            {
                myVisual.Size = new System.Numerics.Vector2((float)DialogAppWindowWidth + 10, (float)DialogAppWindowHeight + 25);
            }
            else myVisual.Size = new System.Numerics.Vector2((float)DialogAppWindowWidth + 10, (float)DialogAppWindowHeight + 25);

            // create a blue drop shadow
            var shadow = compositor.CreateDropShadow();
            shadow.Offset = new System.Numerics.Vector3(-5, -5, 0);
            //shadow.Color = Color.FromArgb(75, 0, 0, 0);
            shadow.BlurRadius = 45;
            myVisual.Shadow = shadow;

            // render on page
            ElementCompositionPreview.SetElementChildVisual(ShadowBorder, myVisual);

            FullAnimationOpen.Begin();
        }

        public void Close()
        {
            IsOn = false;
            var compositor = ElementCompositionPreview.GetElementVisual(ShadowBorder).Compositor;

            // create a red sprite visual
            var myVisual = compositor.CreateSpriteVisual();
            myVisual.Brush = compositor.CreateColorBrush(Microsoft.UI.Colors.Transparent);
            myVisual.Size = new System.Numerics.Vector2(0, 0);

            // create a blue drop shadow
            var shadow = compositor.CreateDropShadow();
            shadow.Offset = new System.Numerics.Vector3(0, 0, 0);
            shadow.Color = Microsoft.UI.Colors.Transparent;
            myVisual.Shadow = shadow;

            // render on page
            ElementCompositionPreview.SetElementChildVisual(ShadowBorder, myVisual);

            FullAnimationClose.Begin();
        }

        private void SecondButton_Click(object sender, RoutedEventArgs e)
        {
            if (SecondButtonClick != null)
                SecondButtonClick(this, new RoutedEventArgs());
            else
            {
                AllButtonsClickDefault();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CancelButtonClick != null)
                CancelButtonClick(this, new RoutedEventArgs());
            else
            {
                AllButtonsClickDefault();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (CloseButtonClick != null)
                CloseButtonClick(this, new RoutedEventArgs());
            else
            {
                AllButtonsClickDefault();
            }
        }

        private void UserControl_LayoutUpdated(object sender, object e)
        {

        }

        private void FullBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var owner = Parent;
            if (owner is DialogWindowContainer SecWinHost)
            {
                uint lastIndex = (uint)(SecWinHost.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost.Children.IndexOf(this);

                SecWinHost.Children.Move(currentIndex, lastIndex);
            }
            if (owner is ChildAppWindowContainer SecWinHost2)
            {
                uint lastIndex = (uint)(SecWinHost2.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost2.Children.IndexOf(this);

                SecWinHost2.Children.Move(currentIndex, lastIndex);
            }
        }

        private void FullAnimationClose_Completed(object sender, object e)
        {
            DialogWindowVisibility = Visibility.Collapsed;
        }
    }
}
