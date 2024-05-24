using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace Ivirius.UI.Windowing
{
    public sealed class DialogWindowContainer : Grid
    {
        public DialogWindowContainer()
        {
            SetBlockBorder();
        }

        public void SetBlockBorder()
        {
            bool AnyElementOn = false;

            foreach (UIElement child in Children)
            {
                if (!(child is DialogWindow || child is ChildAppWindow))
                {
                    throw new ArgumentException("Only ChildAppWindow and DialogWindow elements are allowed as content in the DialogWindowHost.");
                }
            }

            foreach (DialogWindow box in Children.OfType<DialogWindow>())
            {
                if (box.IsOn == true)
                {
                    AnyElementOn = true;
                    break;
                }
            }

            foreach (ChildAppWindow box in Children.OfType<ChildAppWindow>())
            {
                if (box.IsOn == true)
                {
                    AnyElementOn = true;
                    break;
                }
            }

            if (AnyElementOn == true)
            {
                Background = new SolidColorBrush(Color.FromArgb(89, 0, 0, 0));
            }
            else
            {
                Background = null;
            }
        }
    }
}
