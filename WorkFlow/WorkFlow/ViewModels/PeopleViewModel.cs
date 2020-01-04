using System;
using System.Collections.ObjectModel;
using System.Text;

using WorkFlow.Models;
using LiteDB;
using Xamarin.Forms;

namespace WorkFlow.ViewModels
{
    public class PeopleViewModel:BaseViewModel
    {
        public ObservableCollection<Person> People { get; set; }
        public Command GetAllPeople { get; set; }
        public PeopleViewModel()
        {
            People = new ObservableCollection<Person>();
            GetAllPeople = new Command(async () =>
            {
                People.Clear();
                var enumerable = (await DataBase.GetItemsByQuery<Person>("People", Query.Where("UserFlag", x => x.AsBoolean == false))).GetEnumerator();
                while (enumerable.MoveNext())
                {
                    enumerable.Current.IsWorking = enumerable.Current.WorksToday();
                    People.Add(enumerable.Current);
                }
            });
        }
    }
}
