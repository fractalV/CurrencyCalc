using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyCalc2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();

            Uri uri = new Uri("https://www.alta.ru/");

            labelLicenseTerms.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await OpenBrowser(uri)),
            });

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string displayableVersion = $"{version}";

            labelVersion.Text = "Конвертер валют " + displayableVersion + "\r\nАльта-Софт, 2018, Все права защищены.";

        }
    

        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri);
        }
    }
}