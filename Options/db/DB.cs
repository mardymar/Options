using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Point = Options.Point;
using ViewModel = Options.ViewModel;

namespace Stocks.DB
{
    class DB
    {
        static NpgsqlConnection conn;
        static bool open = false;

        public DB() {
            OpenConn();
        }

        public void OpenConn()
        {
            var connString = "User ID = postgres; Password = ''''; Host = localhost; Port = 5432; Database = options";
            conn = new NpgsqlConnection(connString);
            
             conn.Open();
             open = true;
                
            
            
        }

        public List<string> GetSymbolList()
        {
            List<string> result = new List<string>();
            while (!open) {
                System.Threading.Thread.Sleep(100);
            }
            using (var cmd = new NpgsqlCommand("select * from releases", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add((string)reader["symbol"]);
                    }

                    return result;
                }
            }
        }

        public List<DateTime> GetStartDates()
        {

            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            List<DateTime> result = new List<DateTime>();
            using (var cmd = new NpgsqlCommand("select * from date", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add((DateTime) reader["date"]);
                    }

                    return result;
                }
            }
        }
  
        public List<DateTime> GetExperationDates(string symbol, DateTime start)
        {
            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            List<DateTime> result = new List<DateTime>();
            using (var cmd = new NpgsqlCommand("select distinct expirationdate from options where symbol='"+symbol+"' and datadate='"+start+"' and expirationdate>='1/1/2017' and expirationdate<='12/12/2017' order by expirationdate", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add((DateTime)reader["expirationdate"]);
                    }

                    return result;
                }
            }

        }

        public List<DateTime> GetEndDates(string symbol, DateTime start, DateTime end)
        {
            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            List<DateTime> result = new List<DateTime>();
            using (var cmd = new NpgsqlCommand("select * from date where date >= '" + start + "' and date<'" + end + "'", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add((DateTime)reader["date"]);
                    }

                    return result;
                }
            }

        }

        public Double GetNearestStrike(string symbol, DateTime day, DateTime expDate)
        {
            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            using (var cmd = new NpgsqlCommand("select * from options where symbol='" + symbol + "' and expirationdate='" + expDate + "' and datadate='" + day + "' ORDER BY ABS( underlyingprice - strikeprice ) limit 1", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return Decimal.ToDouble((decimal) reader["strikeprice"]);
                    }

                    return 0;
                }
            }
        }

        public Tuple<List<Point>,List<Point>> GetChart(string symbol, DateTime startDate, DateTime expirationDate, string putcall)
        {
            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            List<Point> pricesAsk = new List<Point>();
            List<Point> pricesBid = new List<Point>();

            using (var cmd = new NpgsqlCommand("select askprice, bidprice, datadate from options where symbol='"+symbol+"' and datadate>='"+startDate+"' and strikeprice='"+ GetNearestStrike(symbol,startDate,expirationDate) +"' and expirationdate='"+expirationDate+"' and putcall='"+putcall+"'", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pricesAsk.Add( new Point { X = (DateTime)reader["datadate"], Y = Decimal.ToDouble((Decimal)reader["askprice"]) });
                        pricesBid.Add(new Point { X = (DateTime)reader["datadate"], Y =  Decimal.ToDouble((Decimal)reader["bidprice"]) });
                    }

                    return new Tuple<List<Point>, List<Point>>(pricesAsk, pricesBid);
                }
            }
        }

        public List<DateTime> GetReleaseDates(string symbol, DateTime start, DateTime end)
        {
            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            List<DateTime> results = new List<DateTime>();
            using (var cmd = new NpgsqlCommand("select * from releases where symbol='" + symbol + "'", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.AddRange(((DateTime[]) reader["date"]).Where(d => {
                            return d.CompareTo(start) >= 0 && d.CompareTo(end) <= 0;
                        }));
                    }

                    return results;
                    
                }
            }
        }

        public List<Point> GetStockPrices(string symbol, DateTime start, DateTime end)
        {
            while (!open)
            {
                System.Threading.Thread.Sleep(100);
            }

            List<Point> results = new List<Point>();

            using (var cmd = new NpgsqlCommand("select distinct datadate, underlyingprice from options where symbol='"+symbol +"' and datadate>='"+start+"' and datadate<='"+end+"'", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Point p = new Point { X = (DateTime)reader["datadate"], Y = Decimal.ToDouble((Decimal)reader["underlyingprice"]) };
                        results.Add(p);
                    }

                    return results.OrderBy(d => d.X).ToList();
                }
            }
        }

    }

    
}
