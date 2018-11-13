using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CurrencyCalc2
{

    public partial class App : Application
    {
        public const string cbr = "http://www.cbr.ru/scripts/XML_daily.asp";
        public const string ecb = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        public static string SourceUrl = cbr;

        public static string MySumString = "100";  //TODO: Пеееееренести в загрузку из состояний приложения

        public IList<string> CurrencyList { get; set; }     

        public static int ThemeID;        

        public enum FavoritesCurrency { RUR, GBP, USD, EUR, CNY, JPY, CHF };

        public static string[] FavoritesDefault = new string[7] { "RUR", "GBP", "USD", "EUR", "CNY", "JPY", "CHF" };

        public static int[] pickersID = new[] { (int)FavoritesCurrency.RUR, (int)FavoritesCurrency.USD };

        public static ObservableCollection<Currency> _valutes = new ObservableCollection<Currency>();

        public static ObservableCollection<Currency> Valuta { get { return _valutes; } }

        public static double cross_kurs = 1;
        public static double cross_kurs2 = 1;

        public static string CalculateItog(string tmp)
        {
            if (!String.IsNullOrEmpty(tmp))
            {
                return ((Double.Parse(tmp) * cross_kurs)).ToString("F2", CultureInfo.CurrentCulture);
            }
            else return "0";
        }

        public static string dateTime = DateTime.Now.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture("ru-ru"));



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
