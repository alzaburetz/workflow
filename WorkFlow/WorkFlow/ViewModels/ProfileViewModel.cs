using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LiteDB;
using WorkFlow.Services;
using WorkFlow.Models;

namespace WorkFlow.ViewModels
{
    public class ProfileViewModel: BaseViewModel
    {
        public Command GetUser { get; set; }
        public Person Profile { get; set; }
        public bool UserExists { get; set; }
        public ProfileViewModel()
        {
            Title = "Профиль";
            Profile = new Person();
            UserExists = true;
            GetUser = new Command(async () => 
            {
                Profile = await DataBase.GetItem<Person>("People", Query.Where("UserFlag", x => x.AsBoolean == true));
                if (Profile != null)
                    Profile.FormGraph();
                UserExists = Profile != null;
            });
        }
    }
}
