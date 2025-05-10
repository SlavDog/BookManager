using OxyPlot;
using OxyPlot.Series;
using BookManagerApp.DataAccessLayer;
using OxyPlot.Axes;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookManagerApp.ViewModels
{
    public partial class GraphViewModel : ObservableObject
    {
        [ObservableProperty]
        private PlotModel? plotModel;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateMonthCommand))]
        [NotifyCanExecuteChangedFor(nameof(CreateYearCommand))]
        private bool currentlyShowingMonthGraph = true;

        [ObservableProperty]
        private string textToShow = "";
        public ICollection<Book> Books { get; set; }
        public GraphViewModel(ICollection<Book> books)
        {
            CreateMonthModel(books);
            Books = books;
        }

        [RelayCommand(CanExecute = nameof(CanCreateYear))]
        public void CreateYear()
        {
            CurrentlyShowingMonthGraph = false;
            CreateYearModel(Books);
        }
        public bool CanCreateYear()
        {
            return CurrentlyShowingMonthGraph;
        }

        [RelayCommand(CanExecute = nameof(CanCreateMonth))]
        public void CreateMonth()
        {
            CurrentlyShowingMonthGraph = true;
            CreateMonthModel(Books);
        }

        public bool CanCreateMonth()
        {
            return !CurrentlyShowingMonthGraph;
        }

        private void CreateYearModel(ICollection<Book> books)
        {
            if (!books.Any(b => b.FinishDate != null))
            {
                TextToShow = "There must be some books with finish date for graph to show!";
                return;
            }
            else
            {
                TextToShow = "";
            }

            PlotModel = new PlotModel { Title = "Yearly Statistics" };
            var barSeries = new BarSeries
            {
                StrokeThickness = 1,
                FillColor = OxyColors.Orange,
                Background = OxyColors.Transparent,
                LabelPlacement = LabelPlacement.Outside
            };

            var grouped = books
                .Where(b => b.FinishDate != null)
                .GroupBy(b => b.FinishDate.Value.Year )
                .OrderBy(g => g.Key)
                .ToList();

            var currDate = new DateTime(grouped[0].Key, 1, 1);
            var maxDate = new DateTime(grouped[^1].Key, 1, 1);

            List<string> labels = new();

            while (currDate <= maxDate)
            {
                labels.Add(currDate.ToString("yyyy"));
                var yearBooksGroup = grouped
                    .FirstOrDefault(g => g.Key == currDate.Year);
                if (yearBooksGroup != null)
                {
                    barSeries.Items.Add(new BarItem(yearBooksGroup.Count()));
                }
                else
                {
                    barSeries.Items.Add(new BarItem(0));
                }
                currDate = currDate.AddYears(1);
            }

            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Year"
            };

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Books Count",
                MajorStep = 1,
                MinorStep = 1,
            };

            foreach (var label in labels)
            {
                categoryAxis.Labels.Add(label);
            }

            PlotModel.Axes.Add(categoryAxis);
            PlotModel.Axes.Add(valueAxis);
            PlotModel.Series.Add(barSeries);
            PlotModel.InvalidatePlot(true);
        }

        private void CreateMonthModel(ICollection<Book> books)
        {
            if (!books.Any(b => b.FinishDate != null))
            {
                TextToShow = "There must be some books with finish date for graph to show!";
                return;
            }
            else
            {
                TextToShow = "";
            }
            PlotModel = new PlotModel { Title = "Monthly Statistics" };
            var barSeries = new BarSeries
            {
                StrokeThickness = 1,
                FillColor = OxyColors.Orange,
                Background = OxyColors.Transparent,
                LabelPlacement = LabelPlacement.Outside
            };

            var grouped = books
                .Where(b => b.FinishDate != null)
                .GroupBy(b => new { b.FinishDate.Value.Month, b.FinishDate.Value.Year })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .ToList();

            var currDate = new DateTime(grouped[0].Key.Year, grouped[0].Key.Month, 1);
            var maxDate = new DateTime(grouped[^1].Key.Year, grouped[^1].Key.Month, 1);

            List<string> labels = new();

            while (currDate <= maxDate)
            {
                labels.Add(currDate.ToString("Y"));
                var monthBooksGroup = grouped
                    .FirstOrDefault(g => g.Key.Month == currDate.Month
                                         && g.Key.Year == currDate.Year);
                if (monthBooksGroup != null)
                {
                    barSeries.Items.Add(new BarItem(monthBooksGroup.Count()));
                }
                else
                {
                    barSeries.Items.Add(new BarItem(0));
                }
                currDate = currDate.AddMonths(1);
            }

            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Month and Year"
            };

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Books Count",
                MajorStep = 1,
                MinorStep = 1,
            };

            foreach (var label in labels)
            {
                categoryAxis.Labels.Add(label);
            }

            PlotModel.Axes.Add(categoryAxis);
            PlotModel.Axes.Add(valueAxis);
            PlotModel.Series.Add(barSeries);
            PlotModel.InvalidatePlot(true);
        }
    }
}
