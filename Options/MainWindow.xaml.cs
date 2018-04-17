using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.Charts;




namespace Options 
{
    public partial class MainWindow : Window
    {
        Stocks.DB.DB data = new Stocks.DB.DB();
        MySeries graphs = new MySeries();

        public MainWindow()
        {
            InitializeComponent();
            DisableDates(dpStart);
            DisableDates(dpExpiration);

            symbolBox.ItemsSource = data.GetSymbolList();

            Random rnd = new Random();
            string randomStock = ((List<string>)symbolBox.ItemsSource)[ rnd.Next(((List<string>) symbolBox.ItemsSource).Count)];
            Run name = new Run();
            name.FontSize = 28;
            name.Text = randomStock;
            
            lblInfo.Text = "Stock symbol highlight:\n";
            lblInfo.Inlines.Add(name);
           
            ChartTrackBallBehavior trackBall = new ChartTrackBallBehavior();
            mainChart.Behaviors.Add(trackBall);
            primaryMain.EnableAutoIntervalOnZooming = true;
            primaryMain.ShowTrackBallInfo = true;
            secondaryMain.EnableAutoIntervalOnZooming = true;

            ChartZoomPanBehavior zooming = new ChartZoomPanBehavior();
            mainChart.Behaviors.Add(zooming);

            LineSeries stockPriceSeries = graphs.MakeLineSeries("stockPriceSeries", data.GetStockPrices(randomStock, DateTime.Parse("1/1/2017"), DateTime.Parse("12/31/2017")));

            mainChart.Series.Add(stockPriceSeries);

            graphs.SetNavigator(navigator, navigatorChart, "stockPriceSeries");


            List<DateTime> startDates = data.GetStartDates();
            foreach (var k in startDates)
            {
                while (dpStart.BlackoutDates.Any(bd => bd.Start.Date == k.Date))
                {
                    dpStart.BlackoutDates.Remove(dpStart.BlackoutDates.FirstOrDefault(bd => bd.Start.Date == k.Date));
                }
            }
        }

        private void DisableDates(DatePicker dP)
        {

            dP.BlackoutDates.Clear();
            dP.SelectedDate = null;
            for (var dt = DateTime.Parse("1/3/2017"); dt <= DateTime.Parse("12/31/2017"); dt = dt.AddDays(1))
            {
                if (!dP.BlackoutDates.Contains(dt))
                {
                    dP.BlackoutDates.Add(
                        new CalendarDateRange(dt, dt)
                        );
                }
            }

            //dP.IsEnabled = false;
        }

        public void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DisableDates(dpExpiration);

            List<List<DateTime>> comboExpirationDates = new List<List<DateTime>>();

            int symbolCount = 0;
            foreach (string symbol in symbolBox.SelectedItems)
            {
                symbolCount++;
                comboExpirationDates.Add(data.GetExperationDates(symbol, (DateTime)dpStart.SelectedDate));
            }

            Dictionary<DateTime, int> expDict = new Dictionary<DateTime, int>();

            foreach(List<DateTime> lst in comboExpirationDates)
            {
                foreach(DateTime date in lst)
                {
                    if(expDict.ContainsKey(date))
                    {
                        expDict[date] += 1;
                    } else
                    {
                        expDict[date] = 1;
                    }
                }
            }

            List<DateTime> expirationDates = expDict.Where(kvp => kvp.Value > 1 || symbolCount == 1 ).Select(kvp => kvp.Key).ToList();


            dpExpiration.IsEnabled = true;
            dpExpiration.DisplayDateStart = dpStart.SelectedDate;

            foreach (var k in expirationDates)
            {
                while (dpExpiration.BlackoutDates.Any(bd => bd.Start.Date == k.Date))
                {
                    dpExpiration.BlackoutDates.Remove(dpExpiration.BlackoutDates.FirstOrDefault(bd => bd.Start.Date == k.Date));
                }
            }
        }

        public void dpExpiration_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            mainChart.Series.Clear();
            string first = "";

            foreach(string symbol in symbolBox.SelectedItems)
            {
                if(first == "")
                {
                    first = symbol;
                }
                Tuple<List<Point>, List<Point>> output = data.GetChart(symbol, (DateTime)dpStart.SelectedDate, (DateTime)dpExpiration.SelectedDate, "call");

                LineSeries askPrices = graphs.MakeLineSeries("askPrices" + symbol, output.Item1);
                mainChart.Series.Add(askPrices);

                LineSeries bidPrices = graphs.MakeLineSeries("bidPrices" + symbol, output.Item2);
                mainChart.Series.Add(bidPrices);
            }
           

            graphs.SetNavigator(navigator, navigatorChart, "askPrices" + first);
        }

        private void btnQueryPrice_Click(object sender, RoutedEventArgs e)
        {
            mainChart.Series.Clear();
            string first = "";

            foreach (string symbol in symbolBox.SelectedItems)
            {
                if (first == "")
                {
                    first = symbol;
                }
                Tuple<List<Point>, List<Point>> output = data.GetChart(symbol, (DateTime)dpStart.SelectedDate, (DateTime)dpExpiration.SelectedDate, "call");

                double start = output.Item1[0].Y;

                List<Point> askListPercents = new List<Point>();
                foreach (Point p in output.Item1)
                {
                    Point newPoint = new Point { X = p.X, Y = (p.Y - start) / p.Y };
                    askListPercents.Add(newPoint);
                }

                List<Point> bidListPercents = new List<Point>();
                foreach (Point p in output.Item2)
                {
                    Point newPoint = new Point { X = p.X, Y = (p.Y - start) / p.Y };
                    bidListPercents.Add(newPoint);
                }

                LineSeries askPrices = graphs.MakeLineSeries("askPrices" + symbol, askListPercents);
                mainChart.Series.Add(askPrices);

                LineSeries bidPrices = graphs.MakeLineSeries("bidPrices" + symbol, bidListPercents);
                mainChart.Series.Add(bidPrices);
            }

            
            graphs.SetNavigator(navigator, navigatorChart, "askPrices" + first);


        }
    }

}


//Code Dump :

//ViewModel queryStockPrices = data.GetStockPrices(randomStock, DateTime.Parse("1/1/2017"), DateTime.Parse("12/31/2017"));

//List<Point> source = queryStockPrices.Data.OrderBy(d => d.X).ToList();
//LineSeries stockPriceGraph = new LineSeries()
//{
//    ItemsSource = source,
//    XBindingPath = "X",
//    YBindingPath = "Y"
//};

//Binding myBinding = new Binding("ZoomFactor");
//myBinding.Source = navigator;
//myBinding.Mode = BindingMode.TwoWay;
//myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
//BindingOperations.SetBinding(primaryMain, SfChart.PrimaryAxisProperty, myBinding);

//Binding myBinding2 = new Binding("ZoomPosition");
//myBinding2.Source = navigator;
//myBinding2.Mode = BindingMode.TwoWay;
//myBinding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
//BindingOperations.SetBinding(primaryMain, SfChart.PrimaryAxisProperty, myBinding2);

//primaryMain.ZoomFactor = navigator.ZoomFactor;
//primaryMain.ZoomPosition = navigator.ZoomPosition;

//navigator.Content = mainChart;

//navigator.ItemsSource = (List<Point>) source;
//navigator.XBindingPath = "X";
//navigatorChart.ItemsSource = (List<Point>)source;
//navigatorChart.YBindingPath = "Y";


//Tuple<List<Point>, List<Point>> output = data.GetChart(symbolBox.SearchText, (DateTime)dpStart.SelectedDate, (DateTime)dpExpiration.SelectedDate, "call");

//LineSeries askPrices = graphs.MakeLineSeries("askPrices", output.Item1);
//mainChart.Series.Add(askPrices);

//LineSeries bidPrices = graphs.MakeLineSeries("bidPrices", output.Item2);
//mainChart.Series.Add(bidPrices);