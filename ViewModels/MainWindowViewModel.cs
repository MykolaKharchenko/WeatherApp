using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WeatherApp.DataModels;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        MainWindowModel model;

        ImageSourceConverter converter = new ImageSourceConverter();
        private string searchedData;
        private CityWeatherInfo currentCity;
        private bool isPopupOpen = false;
        private ObservableCollection<CityWeatherInfo> selectedCities;
        private ObservableCollection<CityWeatherInfo> foundedCities;
        private bool citiesNotFilled = true;
        private ImageSource weatherIconSourcePath;

        public string SearchedData
        {
            get { return searchedData; }
            set
            {
                searchedData = value;
                OnPropertyChanged(nameof(SearchedData));
            }
        }
        public CityWeatherInfo CurrentCity
        {
            get { return currentCity; }
            set
            {
                currentCity = value;
                OnPropertyChanged(nameof(CurrentCity));
                OnPropertyChanged(nameof(WeatherIconSourcePath));
                OnPropertyChanged(nameof(Temperature));
                OnPropertyChanged(nameof(FeelsLikeTemp));
                OnPropertyChanged(nameof(WindArrowVisibility));
                OnPropertyChanged(nameof(WindSpeed));
            }
        }
        public bool WindArrowVisibility
        {
            get { return CurrentCity != null; }
        }
        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set
            {
                isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
            }
        }
        public ObservableCollection<CityWeatherInfo> SelectedCities
        {
            get
            { return selectedCities; }
            set
            {
                selectedCities = value;
                OnPropertyChanged(nameof(SelectedCities));
            }
        }
        public ObservableCollection<CityWeatherInfo> FoundedCities
        {
            get { return foundedCities; }
            set
            {
                foundedCities = value;
                OnPropertyChanged(nameof(FoundedCities));
            }
        }
        public bool CitiesNotFilled
        {
            get
            {
                if (SelectedCities != null)
                    citiesNotFilled = SelectedCities.Count == 0;
                else citiesNotFilled = true;
                OnPropertyChanged(nameof(CitiesIsFilled));
                return citiesNotFilled;
            }
            set
            {
                citiesNotFilled = value;
                OnPropertyChanged(nameof(CitiesNotFilled));
            }
        }
        public bool CitiesIsFilled
        {
            get { return !citiesNotFilled; }
            set
            {
                citiesNotFilled = value;
                OnPropertyChanged(nameof(CitiesIsFilled));
            }
        }
        public ImageSource WeatherIconSourcePath
        {
            get
            {
                if (CurrentCity != null)
                    weatherIconSourcePath = (ImageSource)converter.ConvertFromString(@$"WeatherIcons\{CurrentCity.Weather[0].Icon}@2x.png");
                return weatherIconSourcePath;
            }
        }
        public string Temperature
        {
            get
            {
                if (CurrentCity != null)
                    return Math.Round(CurrentCity.Main.Temp).ToString() + "°С";
                else
                    return "";
            }
        }
        public string FeelsLikeTemp
        {
            get
            {
                if (CurrentCity != null)
                    return $"feels like: {Math.Round(CurrentCity.Main.Feels_like).ToString()} °С";
                else
                    return "";
            }
        }
        public string WindSpeed
        {
            get
            {
                if (CurrentCity != null)
                    return Math.Round(CurrentCity.Wind.Speed).ToString() + " m/s";
                else
                    return "";
            }
        }

        private RelayCommand addCityCommand;
        private RelayCommand findCityCommand;
        private RelayCommand openPopupCommand;
        private RelayCommand closePopupCommand;
        private RelayCommand closingCommand;
        private RelayCommand removeCityCommand;
        private RelayCommand citiesChangedCommand;

        public RelayCommand AddCityCommand => addCityCommand ??= new RelayCommand(AddCityCommandExecute, AddCityCommandCanExecute());
        public RelayCommand FindCityCommand => findCityCommand ??= new RelayCommand(FindCityCommandExecute);
        public RelayCommand OpenPopupCommand => openPopupCommand ??= new RelayCommand(OpenPopupCommandExecute);
        public RelayCommand ClosePopupCommand => closePopupCommand ??= new RelayCommand(ClosePopupCommandExecute);
        public RelayCommand ClosingCommand => closingCommand ??= new RelayCommand(ClosingCommandExecute);
        public RelayCommand RemoveCityCommand => removeCityCommand ??= new RelayCommand(RemoveCityCommandExecute);
        public RelayCommand CitiesChangedCommand => citiesChangedCommand ??= new RelayCommand(CitiesChangedCommandExecute);

        public MainWindowViewModel()
        {
            model = new MainWindowModel();
            SelectedCities = new ObservableCollection<CityWeatherInfo>(model.LoadCities());
            FoundedCities = new ObservableCollection<CityWeatherInfo>();
        }

        private async Task<CityWeatherInfo> UpdateCitiesData(CityWeatherInfo city)
        {
            if (city != null)
            {
                var res = await model.UpdateCityData(city.Id);
                if (SelectedCities.Where(key => key.Id == city.Id).Count() == 0)
                {
                    SelectedCities.Add(city);
                }
                return res;
            }
            else
                return city;
        }

        private async void FindCityCommandExecute()
        {
            try
            {
                FoundedCities = new ObservableCollection<CityWeatherInfo>(await model.FindMatchedCities(SearchedData));
                if (FoundedCities.Count == 0)
                    MessageBox.Show("No results found");
            }
            catch (System.Net.WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCityCommandExecute()
        {
            OnPropertyChanged(nameof(CitiesNotFilled));
            ClosePopupCommandExecute();
        }

        private Func<object, bool> AddCityCommandCanExecute()
        {
            return (obj) => CurrentCity != null;
        }

        private void OpenPopupCommandExecute()
        {
            IsPopupOpen = true;
            SearchedData = string.Empty;
            FoundedCities.Clear();
        }

        private void ClosePopupCommandExecute()
        {
            IsPopupOpen = false;
        }

        private void ClosingCommandExecute()
        {
            model.SaveCities(SelectedCities);
        }

        private void RemoveCityCommandExecute()
        {
            if (CurrentCity != null)
                SelectedCities.Remove(SelectedCities.Where(key => key.Id == CurrentCity.Id).SingleOrDefault());
        }

        private async void CitiesChangedCommandExecute(object parameter)
        {
            if (parameter is CityWeatherInfo city)
            CurrentCity = await UpdateCitiesData(city);
        }
    }
}
