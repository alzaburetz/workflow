using System;
using System.Collections.Generic;
using System.Text;

using WorkFlow.Models;
using LiteDB;

using Xamarin.Forms;

namespace WorkFlow.ViewModels
{
    public class CalendarViewModel: BaseViewModel
    {
        public List<Graph> Calendar { get; set; }
        public Command GetCalendar { get; set; }

        public CalendarViewModel()
        {
            Calendar = new List<Graph>();
            GetCalendar = new Command(async () =>
            {
                var User = await DataBase.GetItem<Person>("People", Query.Where("UserFlag", x => x.AsBoolean == true));
                var day = DateTime.Now.DayOfYear;
                Calendar.Clear();
                for (int i = 0; i < 28; i++)
                {
                    var graph = new Graph(day + i, User.WorksDayOf(day + i));
                    Calendar.Add(graph);
                }
            });
        }
    }
}
