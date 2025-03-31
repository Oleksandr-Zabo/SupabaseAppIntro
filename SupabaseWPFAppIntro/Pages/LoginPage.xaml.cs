using System.Windows;
using System.Windows.Controls;
using ConsoleSupabase.Data.Sourсe.Remote.SupabaseDB;

namespace SupabaseWPFAppIntro.Pages;

public partial class LoginPage : UserControl
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Login and password cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        
            try
            {
                var supabaseService = new SupabaseService();
                await supabaseService.InitServiceAsync();
        
                var session = await supabaseService.LoginAsync(login, password);
        
                if (session != null)
                {
                    MessageBox.Show("Login successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow?.MainWindowFrame.Navigate(new HomePage(login));
                }
                else
                {
                    MessageBox.Show("Login failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    

    private async void OnSignUpButtonClick(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordBox.Password;
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Login and password cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
    
        try
        {
            var supabaseService = new SupabaseService();
            await supabaseService.InitServiceAsync();
    
            var session = await supabaseService.RegisterAsync(login, password);
    
            if (session != null)
            {
                MessageBox.Show("Registration successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Registration failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}