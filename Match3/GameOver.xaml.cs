using Match3.Model;
using System.Windows;
using System.Windows.Controls;

namespace Match3
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : UserControl
    {
        private Settings _standartSettings;
        public int Points { get; }

        public GameOver(int points)
        {
            InitializeComponent();

            _standartSettings = new Settings(new BoardSize(8, 8), 1);
            Points = points;
            DataContext = this;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu(_standartSettings));
        }
    }
}
