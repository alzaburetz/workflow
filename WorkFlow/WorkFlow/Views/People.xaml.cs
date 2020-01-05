using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WorkFlow.ViewModels;

namespace WorkFlow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class People : ContentPage
    {
        PeopleViewModel viewModel { get; set; }
        public People()
        {
            BindingContext = viewModel = new PeopleViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetAllPeople.Execute(null);
        }

        private async void AddUser(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushAsync(new AddPerson(false));
        }

        private async void WatchCalendar(object sender, EventArgs args)
        {
            var id = Convert.ToInt32((sender as Frame).ClassId);
            await Shell.Current.Navigation.PushAsync(new Calendar(false, id));
        }
    }
}