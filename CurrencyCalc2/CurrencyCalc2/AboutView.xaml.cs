using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

namespace CurrencyCalc2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutView : ContentView
	{       

        public AboutView ()
		{
			InitializeComponent ();

            Uri uri = new Uri("https://www.alta.ru/");

            labelLicenseTerms.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await OpenBrowser(uri)),
            });

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string displayableVersion = $"{version}";

            labelVersion.Text = "Калькулятор валют " + displayableVersion + "\r\nАльта-Софт, 2018, Все права защищены.";
            
        }        
       
        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri);
        }

       
    }

    
}