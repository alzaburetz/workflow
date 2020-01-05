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
        public Command DeletePerson { get; set; }
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
                    enumerable.Current.FormGraph();
                    People.Add(enumerable.Current);
                }
            });
            DeletePerson = new Command(async (person) =>
            {
                await DataBase.DeleteItem<Person>("People", Query.Where("_id", x => x.AsInt32 == (person as Person).Id));
                People.Remove(person as Person);
            });
        }
    }
}
