using System.ComponentModel;
using System.Windows.Controls;
using Churilova05.Tools.Managers;
using Churilova05.Tools.Navigation;
using Churilova05.ViewModels;


namespace Churilova05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentOwner
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApp();
        }

        private void InitializeApp()
        {
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.ProcessList);
        }


        public ContentControl ContentControl => _contentControl;

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
    }
}