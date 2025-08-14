using System.Windows;
using System.Windows.Controls;
using BookManagerApp.ViewModels;

namespace BookManagerApp.Views
{
    /// <summary>
    /// Interakční logika pro Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            var loginViewModel = new LoginViewModel();
            DataContext = loginViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = PasswordBox.Password;
            }
        }
    }
}
