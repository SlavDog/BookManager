using BookManagerApp.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BookManagerApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        // Preheat the database
        PreheatAsync();
    }

    public async Task PreheatAsync()
    {
        var db = new AppDbContext();
        await Task.Run(() =>
        {
            var users = db.Users.Take(10).ToList();
        });
    }
}

