using BookManagerApp.DataAccessLayer;
using BookManagerApp.ViewModels;
using System.Windows;

namespace BookManagerApp.Views
{
    /// <summary>
    /// Interakční logika pro Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        public Graph(ICollection<Book> books)
        {
            InitializeComponent();
            var graphViewModel = new GraphViewModel(books);
            DataContext = graphViewModel;
        }
    }
}
