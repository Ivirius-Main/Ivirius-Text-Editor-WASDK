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

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Ivirius.UI.Windowing
{
    public sealed class ChildAppWindowContainer : Grid
    {
        public ChildAppWindowContainer()
        {
            foreach (UIElement child in Children)
            {
                if (!(child is DialogWindow || child is ChildAppWindow))
                {
                    throw new ArgumentException("Only ChildAppWindow and DialogWindow elements are allowed as content in the ChildAppWindowHost.");
                }
            }
        }
    }
}
