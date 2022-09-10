using Match3.Model;
using System.Windows;
using System.Windows.Controls;

namespace Match3
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private readonly Settings _settings;

        public MainMenu(Settings settings)
        {
            InitializeComponent();

            _settings = settings;
        }

        public void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new GameInterface(_settings));
        }

        public void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new SettingsWindow());
        }

        public void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}