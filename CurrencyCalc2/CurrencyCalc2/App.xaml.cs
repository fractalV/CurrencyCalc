using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
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
            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");
            CultureInfo ci;
            ci = CultureInfo.CreateSpecificCulture("en-GB");
            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            //if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            //{
                
               // if (DependencyService.Get<ILocalize>().GetCurrentCultureInfo() == null)
              // {
                    
              //  }
             //  else
              //  {
              //      // determine the correct, supported .NET culture
             //       ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
             //   }

                System.Diagnostics.Debug.WriteLine(ci.EnglishName);
                Resx.AppResources.Culture = ci; // set the RESX for resource localization
               // DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
           // }

            //Если не проинициализировать то получим ошибку StaticResource not found for key
            InitializeComponent();
           
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
