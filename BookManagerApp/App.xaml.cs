using BookManagerApp.DataAccessLayer;
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
        // so the first login doesn't take so long
        PreheatDb();
    }

    public void PreheatDb()
    {
        using var db = new AppDbContext();
        db.EnsureCreated();
        var users = db.Users.Take(10).ToList();
    }
}

