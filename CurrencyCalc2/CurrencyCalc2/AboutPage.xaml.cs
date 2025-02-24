﻿using CurrencyCalc2.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            VersionTracking.Track();

            Uri uri = new Uri("https://www.alta.ru/");

            labelLicenseTerms.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await OpenBrowser(uri)),
            });


            var currentBuild = VersionTracking.CurrentBuild;

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string displayableVersion = $"{version.Major}";           

            labelVersion.Text = AppResources.NameApp + " " + displayableVersion + " (" + currentBuild + ") \r\n"+AppResources.CopyRightApp;

            imageLogofull.IsVisible = true;
            imageLogofull.RotationY = 0;
            imageLogofull.RotateYTo(360, 2000, Easing.CubicInOut);
            imageLogofull.Opacity = 0;
            imageLogofull.FadeTo(1, 2000);

        }


        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri);
        }
    }
}