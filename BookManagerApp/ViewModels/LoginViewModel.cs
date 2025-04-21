using BookManagerApp.DataAccessLayer;
using BookManagerApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookManagerApp.ViewModels
{
 
    public partial class LoginViewModel : ObservableObject
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        [ObservableProperty]
        private string? infoText = "Please enter your credentials to log in or register a new account.";

        [RelayCommand]
        private async Task Register(object obj)
        {
            bool success = await UserService.AddUser(Username, Password);
            InfoText = success ? "User successfully registered!" : "This user already exists!";
        }

        [RelayCommand]
        private async Task Login(object obj)
        {
            bool success = await UserService.CheckCredentialsUser(Username, Password);
            InfoText = success ? "Correct! Welcome master!" : "Wrong! You shall not pass!";
        }
    }
}