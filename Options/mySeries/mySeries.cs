using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Charts;

namespace Options
{   
    class MySeries
    {
        Dictionary<string, List<Point>> seriesDictionary = new Dictionary<string, List<Point>>();

        public MySeries()
        { 

        }

        public LineSeries MakeLineSeries(string name, List<Point> points)
        {
            
            LineSeries myLineSeries = new LineSeries()
            {
                ItemsSource = points,
                XBindingPath = "X",
                YBindingPath = "Y"
            };

            
            seriesDictionary.Add(name, points);

            return myLineSeries;
        }

        public void SetNavigator(SfDateTimeRangeNavigator navigator, SfLineSparkline navigatorChart, string seriesName)
        {
            List<Point> source = seriesDictionary[seriesName];

            navigator.ItemsSource = (List<Point>)source;
            navigator.XBindingPath = "X";
            navigatorChart.ItemsSource = (List<Point>)source;
            navigatorChart.YBindingPath = "Y";
        }
    }
}
