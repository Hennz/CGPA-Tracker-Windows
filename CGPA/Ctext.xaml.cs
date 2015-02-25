using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CGPA
{
    public sealed partial class Ctext : UserControl
    {
        public Ctext()
        {
            this.InitializeComponent();
        }
        public void MoveTo(double x, double y)
        {
            Margin = new Thickness { Left = x + 1, Top = y + 1 };
        }
    }
}