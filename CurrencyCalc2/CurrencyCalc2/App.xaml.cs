using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
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
        public static string cultureName = "";

        public static string MySumString = "100";  //TODO: Пеееееренести в загрузку из состояний приложения

        public IList<string> CurrencyList { get; set; }     

        public static int ThemeID;       
        
        public static CultureInfo ci;

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

        public static string dateTime = DateTime.Now.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture(Thread.CurrentThread.CurrentUICulture.Name));



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

            if (cultureName == "") cultureName = Thread.CurrentThread.CurrentUICulture.Name;            

            ci = CultureInfo.CreateSpecificCulture(cultureName);

            //System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.CurrentUICulture.Name);

            //System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.CurrentCulture.Name);            

                //System.Diagnostics.Debug.WriteLine(ci.EnglishName);
            Resx.AppResources.Culture = ci; // set the RESX for resource localization
            

            //Если не проинициализировать то получим ошибку StaticResource not found for key
            InitializeComponent();
           
            MainPage = new Menu();            

            //Resources = new Xamarin.Forms.Themes;
        }

       

        protected override void OnStart ()
		{
            // Handle when your app starts           
            System.Diagnostics.Debug.WriteLine("OnStart");
            IDictionary<string, object> properties = Application.Current.Properties;
            if (properties.ContainsKey("source"))
            {
                SourceUrl = (string)properties["source"];
            }
            if (properties.ContainsKey("culture"))
            {
                cultureName = (string)properties["culture"];
            }
            
        }

        protected override void OnSleep ()
		{
            // Handle when your app sleeps
            System.Diagnostics.Debug.WriteLine("OnSleep");
            Application.Current.Properties["source"] = SourceUrl;
            Application.Current.Properties["culture"] = cultureName;
            Application.Current.Properties["pickersID0"] = pickersID[0];
            Application.Current.Properties["pickersID1"] = pickersID[1];
        }

		protected override void OnResume ()
		{
            // Handle when your app resumes   
            //Task<bool> task = DisplayAlert("Simple Alert", "Decide on an option",

            //                                "yes or ok", "no or cancel");

            //task.ContinueWith((Task<bool> taskResult) =>

            //{

            //    Device.BeginInvokeOnMainThread(() =>

            //    {

            //        label.Text = String.Format("Alert {0} button was pressed",

            //                                   taskResult.Result ? "OK" : "Cancel");

            //    });

            //});

            //label.Text = "Alert is currently displayed";

        }

    }
}
