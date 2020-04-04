using System.Windows.Controls;
using System.Windows.Input;
using Churilova05.Tools.Navigation;
using Churilova05.ViewModels;

namespace Churilova05.Views
{
    /// <summary>
    /// Interaction logic for TaskListView.xaml
    /// </summary>
    public partial class TaskListView : UserControl, INavigatable
    {
        public TaskListView()
        {
            InitializeComponent();
            DataContext = new ProcessListViewModel();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}