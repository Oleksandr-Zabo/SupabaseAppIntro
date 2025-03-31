using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SupabaseWPFAppIntro.Pages;

public partial class HomePage : UserControl
{
    public HomePage(string email)
    {
        InitializeComponent();
        EmailTextBlock.Text = $"Your email: {email}";
    }
    private void Logout(object sender, RoutedEventArgs e)
    {
        var mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        mainWindow?.MainWindowFrame.Navigate(new LoginPage());
    }
}