using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Models;
using WeatherApp.ViewModels.Commands;
using WeatherApp.ViewModels.Helpers;

namespace WeatherApp.ViewModels
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string _query;
        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private CurrentConditions _currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return _currentConditions; }
            set
            {
                _currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        private City _selectedCity;
        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                if (_selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };

                CurrentConditions = new CurrentConditions
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
            Cities = new ObservableCollection<City>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
            Cities.Clear();
        }
    }
}
