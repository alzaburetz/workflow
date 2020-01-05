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

        public Person User { get; set; }
        public CalendarViewModel()
        {
            Calendar = new List<Graph>();
            User = new Person();
            GetCalendar = new Command(async (id) =>
            {
                if (id == null)
                {
                    User = await DataBase.GetItem<Person>("People", Query.Where("UserFlag", x => x.AsBoolean == true));
                    Title = "Ваш календарь";
                }
                    
                else
                {
                    User = await DataBase.GetItem<Person>("People", Query.Where("_id", x => x.AsInt32 == (int)id));
                    Title = $"Календарь {User.Name}";
                }
                    
                var now = DateTime.Now;
                var day = new DateTime(now.Year, now.Month, 1).DayOfYear;
                Calendar.Clear();
                for (int i = 0; i < DateTime.DaysInMonth(now.Year, now.Month); i++)
                {
                    var graph = new Graph(day + i, User.WorksDayOf(day + i));
                    Calendar.Add(graph);
                }
            });
        }
    }
}
