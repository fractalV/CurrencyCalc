using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CurrencyCalc2
{
	public partial class App : Application
	{
        public IList<string> CurrencyList { get; set; }
        

        public App ()
		{
#if DEBUG
            //   LiveReload.Init();
#endif
            //InitializeComponent();
            //CurrencyList = new List<string>();
            //MainPage = new MainPage(); 
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new LoadingPage();            
            MainPage = new Menu();

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
