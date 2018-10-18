using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CurrencyCalc2.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new CurrencyCalc2.App());
            GetDeviceFormFactorType();
        }

        public static DeviceFormFactorType GetDeviceFormFactorType()
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    return DeviceFormFactorType.Phone;
                case "Windows.Desktop":
                    ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 480 });
                    ApplicationView.PreferredLaunchViewSize = new Size { Height = 480, Width = 320 };
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                    return DeviceFormFactorType.Desktop;
                case "Windows.Tablet":
                    return DeviceFormFactorType.Tablet;
                case "Windows.Universal":
                    return DeviceFormFactorType.Iot;
                case "Windows.Team":
                    return DeviceFormFactorType.SurfaceHub;
                default:
                    return DeviceFormFactorType.other;
            }
        }


        public enum DeviceFormFactorType
        {
            Phone,
            Desktop,
            Tablet,
            Iot,
            SurfaceHub,
            other
        }
    }
}
