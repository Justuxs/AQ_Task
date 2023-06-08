using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task.Helpers;

namespace Task.Views
{
    /// <summary>
    /// Interaction logic for MismatchWindow.xaml
    /// </summary>
    public partial class MismatchWindow : Window
    {
        public MismatchWindow(List<HousingMismatch> housingMismatch)
        {
            InitializeComponent();
            MismatchGridView.ItemsSource = housingMismatch;
        }
    }
}
