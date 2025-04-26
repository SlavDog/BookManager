using BookManagerApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookManagerApp.Views;

namespace BookManagerApp.ViewModels
{
 
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? username;
        [ObservableProperty]
        private string? password;
        private BooksOverview? booksOverviewWindow;

        [ObservableProperty]
        private string? infoText = "Please enter your credentials to log in or register a new account.";

        [RelayCommand]
        private async Task Register(object obj)
        {
            int result = await UserService.AddUser(Username, Password);
            switch (result)
            {
                case 0:
                    InfoText = "User successfully registered!";
                    break;
                case -1:
                    InfoText = "User already exists!";
                    break;
                case -2:
                    InfoText = "Username or password cannot be empty!";
                    break;
            }
        }

        [RelayCommand]
        private async Task Login(object obj)
        {
            bool success = await UserService.CheckCredentialsUser(Username, Password);
            InfoText = success ? "Correct! Welcome master!" : "Wrong! You shall not pass!";
            if (success)
            {
                var user = await UserService.GetUser(Username);
                booksOverviewWindow = new BooksOverview(user);
                var loginWindow = obj as Login;
                booksOverviewWindow.Show();
                loginWindow.Close();
            }
        }
    }
}