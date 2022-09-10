using System.Windows.Controls;

namespace Match3.Model
{
    public static class Switcher
    {
        public static MainWindow PageSwitcher;

        public static void Switch(UserControl nextPage)
        {
            PageSwitcher.Navigate(nextPage);
        }
    }
}