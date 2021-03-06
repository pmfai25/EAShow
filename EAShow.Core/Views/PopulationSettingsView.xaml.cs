using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EAShow.Core.ViewModels;
using Syncfusion.UI.Xaml.Controls.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace EAShow.Core.Views
{
    public sealed partial class PopulationSettingsView : UserControl
    {
        public PopulationSettingsViewModel ViewModel => DataContext as PopulationSettingsViewModel;

        public PopulationSettingsView()
        {
            this.InitializeComponent();
        }

        private void Population1NumericUpDown_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if ((decimal) Population1NumericUpDown.Value < 2)
                Population1Switch.IsOn = false;
        }

        private void Population2NumericUpDown_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if ((decimal)Population2NumericUpDown.Value < 2)
                Population2Switch.IsOn = false;
        }

        private void SelectorsGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            Population1NumericUpDown.Culture = CultureInfo.CurrentUICulture;
            Population2NumericUpDown.Culture = CultureInfo.CurrentUICulture;
        }
    }
}
