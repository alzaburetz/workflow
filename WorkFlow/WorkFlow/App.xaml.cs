using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkFlow.Services;
using WorkFlow.Views;

namespace WorkFlow
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DatabaseService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
