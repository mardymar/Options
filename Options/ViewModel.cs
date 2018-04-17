using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Options
{
    public class ViewModel
    {
        public List<Point> Data { get; set; }

        public ViewModel()
        {
            Data = new List<Point>();
        }

        public void Add(DateTime px, double py)
        {
            Data.Add(new Point { X = px, Y = py });
        }

        public void RemoveAtIndex(Point p)
        {
            Data.Remove(p);
        }
    }
}
