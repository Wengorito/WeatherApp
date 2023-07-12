using System.ComponentModel;
using WeatherApp.Models;
using WeatherApp.ViewModels.Commands;
using WeatherApp.ViewModels.Helpers;

namespace WeatherApp.ViewModels
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private CurrentConditions currrentConditions;

        public CurrentConditions CurrrentConditions
        {
            get { return currrentConditions; }
            set
            {
                currrentConditions = value;
                OnPropertyChanged("CurrrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };
                CurrrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Unit
                        {
                            Value = 21
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
        }
    }
}
