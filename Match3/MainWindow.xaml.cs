using Match3.Model;
using System.Windows;
using System.Windows.Controls;

namespace Match3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Settings _standartSettings;

        public MainWindow()
        {
            InitializeComponent();

            _standartSettings = new Settings(new BoardSize(8, 8), 1);
            Switcher.PageSwitcher = this;
            Switcher.Switch(new MainMenu(_standartSettings));
        }

        public void Navigate(UserControl nextPage)
        {
            Content = nextPage;
        }
    }
}