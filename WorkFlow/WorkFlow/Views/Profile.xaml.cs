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
    public partial class Profile : ContentPage
    {
        ProfileViewModel viewModel { get; set; }
        public Profile()
        {
            BindingContext = viewModel = new ProfileViewModel();
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetUser.Execute(null);
            await Task.Delay(100);
            if (!viewModel.UserExists)
            {
                await Shell.Current.Navigation.PushModalAsync(new AddPerson(true));
            }
            else
            {
                var StatusConverter = new Converters.StatusColorConverter();
                UserName.Text = viewModel.Profile.Name;
                Formed.Text = viewModel.Profile.FormedWorkflow;
                Status.Text = viewModel.Profile.IsWorking ? "РАБОЧИЙ ДЕНЬ" : "ВЫХОДНОЙ ДЕНЬ";
                Status.TextColor = (Color)StatusConverter.Convert(viewModel.Profile.IsWorking, typeof(Color), null, null);
            }
        }

        private async  void EditProfile(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushAsync(new AddPerson(true, true));
        }
    }
}