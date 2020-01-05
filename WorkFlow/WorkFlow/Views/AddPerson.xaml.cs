using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WorkFlow.ViewModels;
using WorkFlow.Models;

namespace WorkFlow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPerson : ContentPage
    {
        AddPersonViewModel viewModel;
        Person person { get; set; }
        bool isEditing { get; set; }
        public AddPerson(bool isUser, bool isEditing = false)
        {
            viewModel = new AddPersonViewModel(isUser);
            this.isEditing = isEditing;
            BindingContext = viewModel.CurrentPerson;
            person = new Person();
            person.UserFlag = isUser;
            if (!isUser)
                viewModel.Title = "Добавить график";
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.isUser)
            {
                viewModel.Getuser.Execute(null);
                await Task.Delay(200);
                try
                {
                    var person = viewModel.CurrentPerson;
                    Name.Text = person.Name;
                    Date.Date = person.NextWorkDay;
                    Graph.Text = $"{person.WorkDays} / {person.WeekDays}";
                }
                catch
                {

                }
            }
        }

        private async void Save(object sender, EventArgs args)
        {
            person.Name = Name.Text;
            person.NextWorkDay = Date.Date;
            var days = Graph.Text.Split('/');
            person.WorkDays = int.Parse(days[0]);
            person.WeekDays = int.Parse(days[1]);
            if (!isEditing)
                viewModel.Save.Execute(person);
            else
                viewModel.Update.Execute(person);
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}