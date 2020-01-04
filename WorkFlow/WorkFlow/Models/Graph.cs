using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlow.Models
{
    public class Graph
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public bool IsWorking { get; set; }

        public Graph() { }
        public Graph(int day, bool isWorking): this()
        {
            Day = day;
            IsWorking = isWorking;
            Date = new DateTime(DateTime.Now.Year, 1, 1).AddDays(Day - 1);
        }
    }
}
