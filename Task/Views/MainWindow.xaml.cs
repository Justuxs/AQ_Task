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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task.Data;
using Task.Models;
using Task.Services;
using Task.Views;

namespace Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DatabaseManager databaseManager { get; set; }

        public MainWindow()
        {
           InitializeComponent();
        }

        private void btnGeneruoti(object sender, RoutedEventArgs e)
        {
            List<Harness_drawing> harness_Wires = databaseManager.SelectHarnessDrawings();
            List<Harness_drawing>? harness_Drawings = Generator.GenerateRandomSets(harness_Wires);
            if (harness_Drawings != null)
            {
                drawingsGridView.ItemsSource = harness_Drawings;

                List<HarnessDrawingMatrixRow>? matrix = Generator.GenerateDrawingMatrix(harness_Drawings);

                matrixGridView.ItemsSource = Generator.GenerateDrawingMatrix(harness_Drawings);
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drawingsGridView.SelectedItem is Harness_drawing selectedDrawing)
            {
                Window wireWindow = new Window();

                DataGrid wireDataGrid = new DataGrid();
                wireDataGrid.ItemsSource = selectedDrawing.harness_wires;

                wireWindow.Content = wireDataGrid;

                wireWindow.Show();
            }
        }

        private void Matrix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (matrixGridView.SelectedItem is HarnessDrawingMatrixRow harnessDrawingMatrixRow && !harnessDrawingMatrixRow.housing)
            {
                Window mismatchWindow = new MismatchWindow(harnessDrawingMatrixRow.housingMismatches);

                mismatchWindow.Show();
            }
        }
    }
}
