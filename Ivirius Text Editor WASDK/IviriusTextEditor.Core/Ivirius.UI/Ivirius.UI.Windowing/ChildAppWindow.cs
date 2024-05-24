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
using Windows.UI.Core;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;
using Microsoft.UI.Xaml.Hosting;
using Color = Windows.UI.Color;
using Border = Microsoft.UI.Xaml.Controls.Border;
using static Ivirius.UI.Windowing.DialogWindow;
using Microsoft.UI.Xaml.Media.Animation;
//using MicaForUWP.Media;
using CommunityToolkit.WinUI.UI;
using Ivirius_Text_Editor;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Ivirius.UI.Windowing
{
    public partial class ChildAppWindow : UserControl
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
        { }
           
         

            private void TL_ThemeChanged(ThemeListener sender)
            {
                ChangeBackdrop();
            }

            public ChildAppWindow()
            {
                InitializeComponent();
                DataContext = this;
                if (Parent is ChildAppWindowContainer || Parent is null)
                {

                }
                else
                {
                    throw new ArgumentException("The parent of ChildAppWindow must be a ChildAppWindowContainer");
                }
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
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(BackdropKind.Mica, BackdropChange) // Default value
        );

        private static void BackdropChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChildAppWindow)d).ChangeBackdrop();
        }

        private void ChangeBackdrop()
        {
            ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
            if (Backdrop == BackdropKind.Mica)
            {
                var TL = new ThemeListener();
                if (RequestedTheme == ElementTheme.Light)
                {
                }
                else if (RequestedTheme == ElementTheme.Dark)
                {
                }
            }
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
                //if (RequestedTheme == ElementTheme.Light)
                //{
                //    TBar.Background = new AcrylicBrush()
                //    {
                //        TintColor = Microsoft.UI.Colors.White
                //    };
                //}
                //else if (RequestedTheme == ElementTheme.Dark)
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
        [Description("The backdrop of ChildAppWindow's title bar")]
        public BackdropKind Backdrop
        {
            get { return (BackdropKind)GetValue(BackdropProperty); }
            set { SetValue(BackdropProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
        "Title", // The name of the property
        typeof(string), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata("Title") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        [Description("The title of the ChildAppWindow")]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty FirstButtonTextProperty =
        DependencyProperty.Register(
        "FirstButtonText", // The name of the property
        typeof(string), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata("Yes") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public string FirstButtonText
        {
            get { return (string)GetValue(FirstButtonTextProperty); }
            set { SetValue(FirstButtonTextProperty, value); }
        }

        public static readonly DependencyProperty SecondButtonTextProperty =
        DependencyProperty.Register(
        "SecondButtonText", // The name of the property
        typeof(string), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata("No") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public string SecondButtonText
        {
            get { return (string)GetValue(SecondButtonTextProperty); }
            set { SetValue(SecondButtonTextProperty, value); }
        }

        public static readonly DependencyProperty CancelButtonTextProperty =
        DependencyProperty.Register(
        "CancelButtonText", // The name of the property
        typeof(string), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata("Cancel") // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public string CancelButtonText
        {
            get { return (string)GetValue(CancelButtonTextProperty); }
            set { SetValue(CancelButtonTextProperty, value); }
        }

        public static readonly DependencyProperty AppWindowWidthProperty =
        DependencyProperty.Register(
        "AppWindowWidth", // The name of the property
        typeof(double), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(double.Parse("300")) // Default value
        );

        [Browsable(true)]
        [Category("Layout")]
        public double AppWindowWidth
        {
            get { return (double)GetValue(AppWindowWidthProperty); }
            set { SetValue(AppWindowWidthProperty, value); }
        }

        public static readonly DependencyProperty AppWindowHeightProperty =
        DependencyProperty.Register(
        "AppWindowHeight", // The name of the property
        typeof(double), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(double.Parse("250")) // Default value
        );

        [Browsable(true)]
        [Category("Layout")]
        public double AppWindowHeight
        {
            get { return (double)GetValue(AppWindowHeightProperty); }
            set { SetValue(AppWindowHeightProperty, value); }
        }

        public static new readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(
        "Content", // The name of the property
        typeof(UIElement), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
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

        public event RoutedEventHandler MaxResButtonClick;

        public event RoutedEventHandler MinimizeButtonClick;

        public enum ButtonStyle
        {
            Win32,
            UWP
        };

        public static readonly DependencyProperty CloseButtonStyleProperty =
        DependencyProperty.RegisterAttached(
        "CloseButtonStyleClick", // The name of the property
        typeof(ButtonStyle), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(ButtonStyle.Win32, OnCloseButtonStyleChanged) // Default value
        );

        private static void OnCloseButtonStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChildAppWindow)d).SetValue();
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
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(true, CloseButtonEnabled) // Default value
        );

        private static void CloseButtonEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChildAppWindow)d).SetEnabledValue();
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

        public static readonly DependencyProperty IsMaximizeRestoreButtonEnabledProperty =
        DependencyProperty.RegisterAttached(
        "IsMaximizeRestoreButtonEnabled", // The name of the property
        typeof(bool), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(true, MaximizeRestoreButtonEnabled) // Default value
        );

        private static void MaximizeRestoreButtonEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChildAppWindow)d).SetMaximizeRestoreEnabledValue();
        }

        private void SetMaximizeRestoreEnabledValue()
        {
            if (IsMaximizeRestoreButtonEnabled != false)
            {
                MaximizeRestoreButton.IsEnabled = true;
            }
            else
            {
                MaximizeRestoreButton.IsEnabled = false;
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public bool IsMaximizeRestoreButtonEnabled
        {
            get { return (bool)GetValue(IsMaximizeRestoreButtonEnabledProperty); }
            set { SetValue(IsMaximizeRestoreButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsMinimizeButtonEnabledProperty =
        DependencyProperty.RegisterAttached(
        "IsMinimizeButtonEnabled", // The name of the property
        typeof(bool), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(true, MinimizeButtonEnabled) // Default value
        );

        private static void MinimizeButtonEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChildAppWindow)d).SetMinimizeEnabledValue();
        }

        private void SetMinimizeEnabledValue()
        {
            if (IsMinimizeButtonEnabled != false)
            {
                MinButton.IsEnabled = true;
            }
            else
            {
                MinButton.IsEnabled = false;
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public bool IsMinimizeButtonEnabled
        {
            get { return (bool)GetValue(IsMinimizeButtonEnabledProperty); }
            set { SetValue(IsMinimizeButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty RememberPositionProperty =
        DependencyProperty.RegisterAttached(
        "RememberPosition", // The name of the property
        typeof(bool), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
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
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(false) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public bool PlaySound
        {
            get { return (bool)GetValue(PlaySoundProperty); }
            set { SetValue(PlaySoundProperty, value); }
        }

        public static readonly DependencyProperty CanMinimizeProperty =
        DependencyProperty.RegisterAttached(
        "CanMinimize", // The name of the property
        typeof(bool), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(true) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public bool CanMinimize
        {
            get { return (bool)GetValue(CanMinimizeProperty); }
            set { SetValue(CanMinimizeProperty, value); }
        }

        public static readonly DependencyProperty CanMaximizeProperty =
        DependencyProperty.RegisterAttached(
        "CanMaximize", // The name of the property
        typeof(bool), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(true) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public bool CanMaximize
        {
            get { return (bool)GetValue(CanMaximizeProperty); }
            set { SetValue(CanMaximizeProperty, value); }
        }

        public static readonly DependencyProperty HasIconProperty =
        DependencyProperty.RegisterAttached(
        "HasIcon", // The name of the property
        typeof(bool), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
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
            ((ChildAppWindow)d).DetectIconChange();
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
            ((ChildAppWindow)d).DetectBitmapIconChange();
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
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(null, BitmapIconChanged) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty ChildAppWindowVisibilityProperty =
        DependencyProperty.RegisterAttached(
        "ChildAppWindowVisibility", // The name of the property
        typeof(Visibility), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(Visibility.Visible) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public Visibility ChildAppWindowVisibility
        {
            get { return (Visibility)GetValue(ChildAppWindowVisibilityProperty); }
            set { SetValue(ChildAppWindowVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MinimizeButtonVisibilityProperty =
        DependencyProperty.RegisterAttached(
        "MinimizeButtonVisibility", // The name of the property
        typeof(Visibility), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(Visibility.Visible) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public Visibility MinimizeButtonVisibility
        {
            get { return (Visibility)GetValue(MinimizeButtonVisibilityProperty); }
            set { SetValue(MinimizeButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MaximizeButtonVisibilityProperty =
        DependencyProperty.RegisterAttached(
        "MaximizeButtonVisibility", // The name of the property
        typeof(Visibility), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(Visibility.Visible) // Default value
        );

        [Browsable(true)]
        [Category("Common")]
        public Visibility MaximizeButtonVisibility
        {
            get { return (Visibility)GetValue(MaximizeButtonVisibilityProperty); }
            set { SetValue(MaximizeButtonVisibilityProperty, value); }
        }

        private Point lastPosition;
        private bool isDragging;
        private bool isHold;

        private void PrimaryWindow_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            lastPosition = e.GetCurrentPoint(this).Position;
            (sender as Border).CapturePointer(e.Pointer);
            isDragging = true;
            var owner = Parent;
            if (owner is ChildAppWindowContainer SecWinHost)
            {
                uint lastIndex = (uint)(SecWinHost.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost.Children.IndexOf(this);

                SecWinHost.Children.Move(currentIndex, lastIndex);
            }
            isHold = true;


            oldTBPos = e.GetCurrentPoint(TBar).Position;
        }

        private void PrimaryWindow_PointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
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
            isHold = false;
        }

        private void PrimaryWindow_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var newPosition = e.GetCurrentPoint(this).Position;
            var newPositionTB = e.GetCurrentPoint(TBar).Position;
            if (e.GetCurrentPoint(TBar).IsInContact == true && isDragging == true && CanDragWin == true)
            {
                var deltaX = newPosition.X - lastPosition.X;
                var deltaY = newPosition.Y - lastPosition.Y;

                var margin = WindowBorder.Margin;
                margin.Left += deltaX;
                margin.Top += deltaY;
                WindowBorder.Margin = margin;

                lastPosition = newPosition;
            }
            var pointerX = e.GetCurrentPoint(Parent as ChildAppWindowContainer).Position.X;
            if (CanDragWin == false && isHold == true && e.GetCurrentPoint(TBar).IsInContact == true)
            {
                WinMargin = new Thickness(pointerX - WinWidth / 2, 0, 0, 0);
                if (Math.Abs(Math.Truncate(oldTBPos.X) - Math.Truncate(newPositionTB.X)) > 4 || Math.Abs(Math.Truncate(oldTBPos.Y) - Math.Truncate(newPositionTB.Y)) > 4) MaximizeRestore(true);
            }

            TriggerShadow();
        }

        public void TriggerShadow()
        {
            if (isOn == true)
            {
                var compositor = ElementCompositionPreview.GetElementVisual(ShadowBorder).Compositor;

                // create a red sprite visual
                var myVisual = compositor.CreateSpriteVisual();
                myVisual.Brush = compositor.CreateColorBrush(Microsoft.UI.Colors.Transparent);
                if (WindowBorder.Margin.Top > ActualHeight - AppWindowHeight)
                {
                    myVisual.Size = new System.Numerics.Vector2((float)WindowBorder.ActualWidth + 10, (float)WindowBorder.ActualHeight + 25);
                }
                else myVisual.Size = new System.Numerics.Vector2((float)WindowBorder.ActualWidth + 10, (float)WindowBorder.ActualHeight + 25);

                // create a blue drop shadow
                var shadow = compositor.CreateDropShadow();
                shadow.Offset = new System.Numerics.Vector3(-5, -5, 0);
                shadow.Color = Color.FromArgb(75, 0, 0, 0);
                shadow.BlurRadius = 45;
                myVisual.Shadow = shadow;

                // render on page
                ElementCompositionPreview.SetElementChildVisual(ShadowBorder, myVisual);
            }
            else
            {
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

        private void PrimaryWindowBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TriggerShadow();
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

        public static readonly DependencyProperty SetHeightProperty =
        DependencyProperty.RegisterAttached(
        "SetHeight", // The name of the property
        typeof(float), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata((float)0) // Default value
        );

        [Browsable(false)]
        [Category("Common")]
        public float SetHeight
        {
            get { return (float)GetValue(SetHeightProperty); }
            set { SetValue(SetHeightProperty, value); }
        }

        public void Open()
        {
            ChildAppWindowVisibility = Visibility.Visible;
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
            if (owner is ChildAppWindowContainer SecWinHost)
            {
                uint lastIndex = (uint)(SecWinHost.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost.Children.IndexOf(this);

                SecWinHost.Children.Move(currentIndex, lastIndex);
            }

            TriggerShadow();

            var animation = this.FindResource("FullAnimationOpen") as Storyboard;
            animation.Begin();
        }

        public void Close()
        {
            IsOn = false;
            var compositor = ElementCompositionPreview.GetElementVisual(ShadowBorder).Compositor;

            // create a red sprite visual
            var myVisual = compositor.CreateSpriteVisual();
            myVisual.Brush = compositor.CreateColorBrush(Microsoft.UI.Colors.Transparent);
            myVisual.Size = new System.Numerics.Vector2(0,0);

            // create a blue drop shadow
            var shadow = compositor.CreateDropShadow();
            shadow.Offset = new System.Numerics.Vector3(0, 0, 0);
            shadow.Color = Microsoft.UI.Colors.Transparent;
            myVisual.Shadow = shadow;

            // render on page
            ElementCompositionPreview.SetElementChildVisual(ShadowBorder, myVisual);

            var animation = this.FindResource("FullAnimationClose") as Storyboard;
            animation.Completed += Animation_Completed;
            animation.Begin();
        }

        private void Animation_Completed(object sender, object e)
        {
            ChildAppWindowVisibility = Visibility.Collapsed;
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
            if (owner is ChildAppWindowContainer SecWinHost)
            {
                uint lastIndex = (uint)(SecWinHost.Children.Count - 1);
                uint currentIndex = (uint)SecWinHost.Children.IndexOf(this);

                SecWinHost.Children.Move(currentIndex, lastIndex);
            }
        }

        private void MaximizeRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaxResButtonClick != null)
                MaxResButtonClick(this, new RoutedEventArgs());
            else
            {
                MaximizeRestore(false);
            }
        }

        public bool IsMaximized = false;

        private bool CanDragWin = true;

        private Thickness WinMargin = new Thickness();

        private int WinWidth;

        private int WinHeight;

        public void MaximizeRestore(bool triggeredOnMaximize)
        {
            if (CanMaximize == true)
            {
                if (IsMaximized == true)
                {
                    IsMaximized = false;
                    MaxResIcon.Glyph = "";
                    CanDragWin = true;
                    WindowBorder.Margin = WinMargin;
                    AppWindowWidth = WinWidth;
                    AppWindowHeight = WinHeight;
                    WindowBorder.CornerRadius = new CornerRadius(8, 8, 8, 8);
                    TBar.CornerRadius = new CornerRadius(7, 7, 0, 0);
                    //TBAccent.CornerRadius = new CornerRadius(7, 7, 0, 0);
                    CloseButton.CornerRadius = new CornerRadius(0, 7, 0, 0);
                    ToolTipService.SetToolTip(MaximizeRestoreButton, "Maximize");

                    var animation = this.FindResource("FullAnimationMin") as Storyboard;
                    animation.Begin();
                }
                else
                {
                    IsMaximized = true;
                    MaxResIcon.Glyph = "";
                    CanDragWin = false;
                    WinMargin = WindowBorder.Margin;
                    WinWidth = (int)WindowBorder.ActualWidth;
                    WinHeight = (int)WindowBorder.ActualHeight;
                    TriggerSizeSync();
                    WindowBorder.Margin = new Thickness(0, 0, 0, 0);
                    WindowBorder.CornerRadius = new CornerRadius(0, 0, 0, 0);
                    TBar.CornerRadius = new CornerRadius(0, 0, 0, 0);
                    //TBAccent.CornerRadius = new CornerRadius(0, 0, 0, 0);
                    CloseButton.CornerRadius = new CornerRadius(0, 0, 0, 0);
                    ToolTipService.SetToolTip(MaximizeRestoreButton, "Restore");

                    var animation = this.FindResource("FullAnimationMax") as Storyboard;
                    animation.Begin();
                }
            }

            TriggerShadow();
        }

        public void TriggerSizeSync()
        {
            AppWindowWidth = (int)ActualWidth;
            AppWindowHeight = (int)ActualHeight;
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            if (MinimizeButtonClick != null)
                MinimizeButtonClick(this, new RoutedEventArgs());
            else
            {
                AllButtonsClickDefault();
            }
        }

        private void FullBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsMaximized == true)
            {
                TriggerSizeSync();
            }
        }

        private Point lastSizePos;

        private bool canResize;

        private bool displayResizer;

        private void LBCorner_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void LBCorner_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.X + AppWindowWidth >= WindowBorder.Margin.Left)
                    {
                        AppWindowWidth -= lastSizePos.X - Pos.X;
                        lastSizePos.X = Pos.X;

                        TriggerShadow();
                    }
                    if (Pos.Y + AppWindowHeight >= WindowBorder.Margin.Top)
                    {
                        AppWindowHeight -= lastSizePos.Y - Pos.Y;
                        lastSizePos.Y = Pos.Y;

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void LBCorner_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void RBCorner_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeNorthwestSoutheast, e.Pointer.PointerId);
        }

        private void RBCorner_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void Border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeNorthwestSoutheast, e.Pointer.PointerId);
            canResize = true;
        }

        private void PrimaryWindowContent_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            canResize = false;
        }

        private void TBar_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            MaximizeRestore(false);
        }


        public static readonly DependencyProperty TitleBarStyleProperty =
        DependencyProperty.RegisterAttached(
        "TitleBarStyle", // The name of the property
        typeof(TBStyle), // The type of the property
        typeof(ChildAppWindow), // The type of the owner class
        new PropertyMetadata(TBStyle.Color, OnTBStyleChanged) // Default value
        );

        private static void OnTBStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ChildAppWindow)d).SetTBValue();
        }

        private void SetTBValue()
        {
            if (TitleBarStyle == TBStyle.Color)
            {

            }
            if (TitleBarStyle == TBStyle.Transparent)
            {
            //    TBar.Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
            //    TBAccent.Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
            }
        }

        [Browsable(true)]
        [Category("Common")]
        public TBStyle TitleBarStyle
        {
            get { return (TBStyle)GetValue(TitleBarStyleProperty); }
            set { SetValue(TitleBarStyleProperty, value); }
        }

        private void LeftResizeCorner_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeNortheastSouthwest, e.Pointer.PointerId);
        }

        private void LeftResizeCorner_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void LeftResizeCorner_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.X < lastSizePos.X + AppWindowWidth - 300)
                    {
                        var distance = lastSizePos.X - Pos.X;
                        AppWindowWidth += distance;
                        WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left - distance, WindowBorder.Margin.Top, 0, 0);

                        TriggerShadow();
                    }
                    if (Pos.Y + AppWindowHeight >= WindowBorder.Margin.Top)
                    {
                        AppWindowHeight -= lastSizePos.Y - Pos.Y;
                        lastSizePos.Y = Pos.Y;

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void LeftResizeCorner_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void LeftResizeCorner_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        Point oldTBPos;

        private void TBar_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void TBar_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void TopResizeCorner_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeNorthwestSoutheast, e.Pointer.PointerId);
        }

        private void TopResizeCorner_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void TopResizeCorner_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.X < lastSizePos.X + AppWindowWidth - 300)
                    {
                        var distance = lastSizePos.X - Pos.X;
                        AppWindowWidth += distance;
                        WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left - distance, WindowBorder.Margin.Top, 0, 0);

                        TriggerShadow();
                    }
                    if (Pos.Y < lastSizePos.Y + AppWindowHeight - 225)
                    {
                        var distance = lastSizePos.Y - Pos.Y;
                        AppWindowHeight += distance;
                        WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left, WindowBorder.Margin.Top - distance, 0, 0);

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void TopResizeCorner_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void TopResizeCorner_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void RSide_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeWestEast, e.Pointer.PointerId);
        }

        private void RSide_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void RSide_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.X + AppWindowWidth >= WindowBorder.Margin.Left)
                    {
                        AppWindowWidth -= lastSizePos.X - Pos.X;
                        lastSizePos.X = Pos.X;

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void RSide_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void RSide_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void DSide_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeNorthSouth, e.Pointer.PointerId);
        }

        private void DSide_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void DSide_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.Y + AppWindowHeight >= WindowBorder.Margin.Top)
                    {
                        AppWindowHeight -= lastSizePos.Y - Pos.Y;
                        lastSizePos.Y = Pos.Y;

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void DSide_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void DSide_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void LSide_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeWestEast, e.Pointer.PointerId);
        }

        private void LSide_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void LSide_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.X < lastSizePos.X + AppWindowWidth - 300)
                    {
                        var distance = lastSizePos.X - Pos.X;
                        AppWindowWidth += distance;
                        WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left - distance, WindowBorder.Margin.Top, 0, 0);

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void LSide_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void LSide_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void TSide_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (IsMaximizeRestoreButtonEnabled == true && IsMaximized == false) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.SizeNorthSouth, e.Pointer.PointerId);
        }

        private void TSide_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }

        private void TSide_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var Pos = e.GetCurrentPoint(WindowBorder).Position;
            try
            {
                if (e.Pointer.IsInContact == true && canResize == true && IsMaximized == false && IsMaximizeRestoreButtonEnabled == true)
                {
                    if (Pos.Y < lastSizePos.Y + AppWindowHeight - 225)
                    {
                        var distance = lastSizePos.Y - Pos.Y;
                        AppWindowHeight += distance;
                        WindowBorder.Margin = new Thickness(WindowBorder.Margin.Left, WindowBorder.Margin.Top - distance, 0, 0);

                        TriggerShadow();
                    }
                }
            }
            catch
            {

            }
        }

        private void TSide_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).CapturePointer(e.Pointer);
            lastSizePos = e.GetCurrentPoint(WindowBorder).Position;
            canResize = true;
            displayResizer = true;
        }

        private void TSide_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            (sender as Border).ReleasePointerCapture(e.Pointer);
            displayResizer = false;
            if (displayResizer != true) App.Window.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, e.Pointer.PointerId);
        }
    }
}
