using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WorkFlow.ViewModels;

namespace WorkFlow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentPage
    {
        CalendarViewModel viewModel { get; set; }
        bool isUser { get; set; }
        int idUser { get; set; }
        public Calendar()
        {
            isUser = true;
            BindingContext = viewModel = new CalendarViewModel();
            InitializeComponent();
        }
        public Calendar(bool isUser = true, int id = 0): this()
        {
            this.isUser = isUser;
            if (!isUser)
            {
                idUser = id;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (isUser)
            viewModel.GetCalendar.Execute(null);
            else
            viewModel.GetCalendar.Execute(idUser);

            await Task.Delay(150);
            CreateCalendarGrid();
        }

        void CreateCalendarGrid()
        {
            var Calendar = viewModel.Calendar;
            CalendarGrid.Children.Clear();
            Mounth.Text = Calendar[0].Date.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru")).ToUpper();
            for (int i = 0; i < Calendar.Count; i++)
            {
                var cal = Calendar[i];
                var daylabel = new Label();
                daylabel.Text = cal.Date.Day.ToString();
                daylabel.TextColor = cal.IsWorking ? Color.Red : Color.Green;
                daylabel.HorizontalTextAlignment = TextAlignment.Center;
                daylabel.VerticalTextAlignment = TextAlignment.Center;
                daylabel.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                int column = Convert.ToInt32(cal.Date.DayOfWeek) == 0 ? 6 : Convert.ToInt32(cal.Date.DayOfWeek) - 1;
                int columnFirstDay = Convert.ToInt32(new DateTime(cal.Date.Year, cal.Date.Month, 1).DayOfWeek) == 0 ? 6 : Convert.ToInt32(new DateTime(cal.Date.Year, cal.Date.Month, 1).DayOfWeek) - 1;
                int row = (cal.Day + columnFirstDay - 1) / 7 + 1;
                CalendarGrid.Children.Add(daylabel, column, row);
                CalendarGrid.Children.Add(new Label()
                {
                    Text = cal.Date.ToString("ddd", CultureInfo.CreateSpecificCulture("ru")).ToUpper(),
                    TextColor = Color.FromHex("#1C1C1C"),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                }, column, 0);
            }
        }
    }
}