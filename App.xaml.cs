using System.Windows;
using Unity;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public partial class App : Application
    {
        IUnityContainer container = new UnityContainer();

        public App()
        {
            container.RegisterType<IWeatherService, OpenWeatherMap>();
            container.RegisterType<IDataLoader, LocalDataLoader>();

            container.RegisterType<MainWindowModel, MainWindowModel>();
            container.RegisterType<MainWindowViewModel, MainWindowViewModel>();            
        }

        protected override void OnStartup(StartupEventArgs e)
        {          
            var mainWindowViewModel = container.Resolve<MainWindowViewModel>();
            var window = new MainWindow { DataContext = mainWindowViewModel };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
