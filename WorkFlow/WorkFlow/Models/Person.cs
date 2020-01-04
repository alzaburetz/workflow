using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlow.Models
{
    public class Person
    {
        [LiteDB.BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorkDays { get; set; }
        public int WeekDays { get; set; }
        public string FormedWorkflow { get; set; }
        public DateTime NextWorkDay { get; set; }
        public bool UserFlag { get; set; }
        public bool IsWorking { get; set; }

        public bool WorksToday()
        {
            if (DateTime.Now.DayOfYear - NextWorkDay.DayOfYear < 0)
            {
                return false;
            }
            return (DateTime.Now.DayOfYear - NextWorkDay.DayOfYear) % (WorkDays + WeekDays) <= WorkDays - 1;
        }

        public bool WorksDayOf(int day)
        {
            if (day - NextWorkDay.DayOfYear < 0)
            {
                return false;
            }
            return (day - NextWorkDay.DayOfYear) % (WorkDays+WeekDays) <= WorkDays - 1;
        }

        public void FormGraph()
        {
            FormedWorkflow = $"{WorkDays} / {WeekDays}";
        }

    }
}
