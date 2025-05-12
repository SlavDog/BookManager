using BookManagerApp.Managers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookManagerApp.Views;
using System.Diagnostics;

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
            int result = await UserManager.AddUser(Username, Password);
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
            bool success = await UserManager.CheckCredentialsUser(Username, Password);
            InfoText = success ? "Correct! Welcome master!" : "Wrong! You shall not pass!";
            if (success)
            {
                Debug.Assert(Username != null);
                var user = await UserManager.GetUser(Username);
                booksOverviewWindow = new BooksOverview(user);
                var loginWindow = obj as Login;
                booksOverviewWindow.Show();
                Debug.Assert(loginWindow != null);
                loginWindow.Close();
            }
        }

        // Background generator
        public string EmojiSpam => string.Concat(Enumerable.Repeat("📕📖", 414));
    }
}