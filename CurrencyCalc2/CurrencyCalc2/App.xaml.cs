using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CurrencyCalc2
{

    public partial class App : Application
    {
        public IList<string> CurrencyList { get; set; }

        public static int ThemeID;

        public enum FavoritesCurrency { RUR, GBP, USD, EUR, CNY, JPY, CHF };

        public static string[] FavoritesDefault = new string[7] { "RUR", "GBP", "USD", "EUR", "CNY", "JPY", "CHF" };

        public static int[] pickersID = new[] { (int)FavoritesCurrency.RUR, (int)FavoritesCurrency.USD };

        public static ObservableCollection<Currency> _valutes = new ObservableCollection<Currency>();

        public static ObservableCollection<Currency> Valuta { get { return _valutes; } }

        public App ()
		{
#if DEBUG
            //   LiveReload.Init();
#endif
            //Если не проинициализировать то получим ошибку StaticResource not found for key
            InitializeComponent();
            //CurrencyList = new List<string>();
            //MainPage = new MainPage(); 
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new LoadingPage();  
            MainPage = new Menu();

            //Resources = new Xamarin.Forms.Themes;
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
        
    }
}
