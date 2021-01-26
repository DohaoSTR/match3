using Match3.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Match3
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : UserControl
    {
        private Settings _standartSettings;

        public SettingsWindow()
        {
            InitializeComponent();

            _standartSettings = new Settings(new BoardSize(8, 8), 1);

            SizeChooseBox.Items.Add("4x4");
            SizeChooseBox.Items.Add("6x6");
            SizeChooseBox.Items.Add("8x8");
        }

        public void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            BoardSize boardSize = new BoardSize();
            int gameTime;

            switch (SizeChooseBox.SelectedItem)
            {
                case "4x4":
                    boardSize = new BoardSize(4, 4);
                    break;
                case "6x6":
                    boardSize = new BoardSize(6, 6);
                    break;
                case "8x8":
                    boardSize = new BoardSize(8, 8);
                    break;
            }

            gameTime = Convert.ToInt32(GameTimeTextBox.Text);

            Switcher.Switch(new MainMenu(new Settings(boardSize, gameTime)));
        }

        public void ReturnToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu(_standartSettings));
        }
    }
}

