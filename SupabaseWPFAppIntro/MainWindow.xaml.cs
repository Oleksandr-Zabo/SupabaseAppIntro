using System.Text;
using System.Windows;

namespace SupabaseWPFAppIntro;

public partial class MainWindow : Window
{
    public MainWindow()
    {
    }

    [Obsolete("Obsolete")]
    private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        MainWindowFrame.Navigate(new Pages.LoginPage());
    }
}