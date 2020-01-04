using System;
using System.Collections.Generic;
using System.Text;

using WorkFlow.Models;
using LiteDB;

using Xamarin.Forms;

namespace WorkFlow.ViewModels
{
    public class AddPersonViewModel:BaseViewModel
    {
        public Command Save { get; set; }
        public Command Update { get; set; }
        public Command Getuser { get; set; }
        public Person CurrentPerson { get; set; }
        public bool isUser { get; set; }

        public AddPersonViewModel(bool isUser)
        {
            this.isUser = isUser;
            CurrentPerson = new Person();
            Getuser = new Command(async () =>
            {
                CurrentPerson = await DataBase.GetItem<Person>("People", Query.Where("UserFlag", x => x.AsBoolean == true));
            });
            Save = new Command(async (person) =>
            {
                await DataBase.WriteItem<Person>("People", person as Person);
            });
            Update = new Command(async (person) =>
            {
                var updated = await DataBase.UpdateItem<Person>("People", person as Person, null);
            });
        }
    }
}
