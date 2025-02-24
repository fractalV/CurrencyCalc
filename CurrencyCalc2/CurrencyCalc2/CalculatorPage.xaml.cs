﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Net;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using static CurrencyCalc2.App;
using System.Collections.Generic;
using System.Xml;

namespace CurrencyCalc2
{

    
      

    public class SwipeCommandPageViewModel : INotifyPropertyChanged
    {
        int taps = 0;

        public ICommand SwipeCommand => new Command<string>(Swipe);

        public string SwipeDirection { get; private set; }

        public SwipeCommandPageViewModel()
        {
            SwipeDirection = "You swiped: ";
        }

        async void Swipe(string value)
        {
            taps++;
            SwipeDirection = $"You swiped: {value}";
            OnPropertyChanged("SwipeDirection");
            Debug.WriteLine(string.Format("{0} taps. {1}", taps, SwipeDirection));            
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }

        #endregion

       

    }

    public partial class CalculatorPage : ContentPage, INotifyPropertyChanged
    {


        private Image _btnImage;

        //bool hasInternet = false;

        bool labelOneActive = true;
        bool labelChange = false;
        bool firstnumber = false; //.1
        bool lastnumber = false;  //.x1

        NumberFormatInfo provider = new NumberFormatInfo();
        char separator = Convert.ToChar(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);

        int maxDigits = 16;
        int currDigits = 1;        
        
        int cross_nominal_one = 1;
        int cross_nominal_two = 1;

        

        //public string symbolAbout;

        Currency rub = new Currency("RUR", "1", Resx.AppResources.RUB, "1", "\U000020BD");
        Currency eur = new Currency("EUR", "1", Resx.AppResources.EUR, "1", "\U000020AC");

        public CalculatorPage()
        {

            if (SourceUrl == App.ecb)
            {             
              _valutes.Add(eur);    //из ecb
            } else _valutes.Add(rub);

            InitializeComponent();
            Trace.WriteLine(DateTime.Now.ToString() + " - Start of Main");

            BindingContext = new SwipeCommandPageViewModel();

            // symbolAbout = buttonInfo.Text;

            SizeChanged += (object sender, EventArgs args) =>
            {
                double size = 0;
                double sizeX = 0;
                const double k = 30;
                if (this.Height > 0)
                {
                    size = this.Height / k;
                    sizeX = this.Width / k;
                    // Debug.WriteLine(size);
                    // Debug.WriteLine(sizeX);
                    // Debug.WriteLine("");
                    labelDigitsOne.FontSize = size;
                    labelDigitsOneCurrency.FontSize = size;
                    labelDigitsTwo.FontSize = size;
                    labelDigitsTwoCurrency.FontSize = size;
                    buttonDigitComma.FontSize = size;
                    buttonDigitBackspace.FontSize = size;
                    buttonDigitCE.FontSize = size;
                    buttonDigitOne.FontSize = size;
                    buttonDigitTwo.FontSize = size;
                    buttonDigitThree.FontSize = size;
                    buttonDigitFour.FontSize = size;
                    buttonDigitFive.FontSize = size;
                    buttonDigitSix.FontSize = size;
                    buttonDigitSeven.FontSize = size;
                    buttonDigitEight.FontSize = size;
                    buttonDigitNine.FontSize = size;
                    buttonDigitZero.FontSize = size;

                    if (sizeX < 12)
                    {
                        labelDigitsOne.FontSize = size * 1.3;
                        labelDigitsTwo.FontSize = size * 1.3;
                        labelChangeStackLayont.FontSize = size * 1.2;
                        //labelUpdateDate.FontSize = size * 0.5;
                        pickerCurrencyOne.FontSize = size * 0.7;
                        pickerCurrencyTwo.FontSize = size * 0.7;
                        labelCurrentCurrencyRate.FontSize = size * 0.6;
                        labelCurrentCurrencyRate2.FontSize = size * 0.6;
                        labelUpdateDate.FontSize = size * 0.5;

                        buttonDigitOne.FontSize = size * 1.3;
                        buttonDigitTwo.FontSize = size * 1.3;
                        buttonDigitThree.FontSize = size * 1.3;
                        buttonDigitFour.FontSize = size * 1.3;
                        buttonDigitFive.FontSize = size * 1.3;
                        buttonDigitSix.FontSize = size * 1.3;
                        buttonDigitSeven.FontSize = size * 1.3;
                        buttonDigitEight.FontSize = size * 1.3;
                        buttonDigitNine.FontSize = size * 1.3;
                        buttonDigitZero.FontSize = size * 1.3;
                    }
                    else if (sizeX >= 12 && sizeX <= 15)
                    {
                        labelDigitsOne.FontSize = size * 1.4;
                        labelDigitsTwo.FontSize = size * 1.4;
                        labelChangeStackLayont.FontSize = size * 1.5;
                        //labelUpdateDate.FontSize = size * 0.8;
                        pickerCurrencyOne.FontSize = size * 0.8;
                        pickerCurrencyTwo.FontSize = size * 0.8;
                        labelCurrentCurrencyRate.FontSize = size * 0.7;
                        labelCurrentCurrencyRate2.FontSize = size * 0.7;
                        labelUpdateDate.FontSize = size * 0.5;

                        buttonDigitOne.FontSize = size * 1.4;
                        buttonDigitTwo.FontSize = size * 1.4;
                        buttonDigitThree.FontSize = size * 1.4;
                        buttonDigitFour.FontSize = size * 1.4;
                        buttonDigitFive.FontSize = size * 1.4;
                        buttonDigitSix.FontSize = size * 1.4;
                        buttonDigitSeven.FontSize = size * 1.4;
                        buttonDigitEight.FontSize = size * 1.4;
                        buttonDigitNine.FontSize = size * 1.4;
                        buttonDigitZero.FontSize = size * 1.4;

                    } else
                    {
                        labelDigitsOne.FontSize = size * 1.5;
                        labelDigitsTwo.FontSize = size * 1.5;
                        labelChangeStackLayont.FontSize = size * 1.7;
                        //labelUpdateDate.FontSize = size;
                        pickerCurrencyOne.FontSize = size;
                        pickerCurrencyTwo.FontSize = size;
                        labelCurrentCurrencyRate.FontSize = size * 0.9;
                        labelCurrentCurrencyRate2.FontSize = size * 0.9;
                        labelUpdateDate.FontSize = size * 0.5;

                        buttonDigitOne.FontSize = size * 1.5;
                        buttonDigitTwo.FontSize = size * 1.5;
                        buttonDigitThree.FontSize = size * 1.5;
                        buttonDigitFour.FontSize = size * 1.5;
                        buttonDigitFive.FontSize = size * 1.5;
                        buttonDigitSix.FontSize = size * 1.5;
                        buttonDigitSeven.FontSize = size * 1.5;
                        buttonDigitEight.FontSize = size * 1.5;
                        buttonDigitNine.FontSize = size * 1.5;
                        buttonDigitZero.FontSize = size * 1.5;
                    }

                    if (size < 20)
                    {
                        frameOne.Padding = 4;
                        // frameTwo.Padding = 4;
                        labelCurrentCurrencyRate.Margin = 4;
                        labelCurrentCurrencyRate2.Margin = 4;
                        imageCurrencyOne.WidthRequest = 30;
                        imageCurrencyTwo.WidthRequest = 30;

                        //buttonDigitOne.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button));

                    } else if (size < 30)
                    {
                        frameOne.Padding = 6;
                        // frameTwo.Padding = 6;
                        labelCurrentCurrencyRate.Margin = 6;
                        labelCurrentCurrencyRate2.Margin = 6;
                        imageCurrencyOne.WidthRequest = 40;
                        imageCurrencyTwo.WidthRequest = 40;
                        //buttonDigitOne.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button));
                    }
                }

            };
            //Debug.WriteLine(GetSizeRequest(stacklayontGlobal));

            Exrin.Common.ThreadHelper.Init(SynchronizationContext.Current);

            //LoadCurrencyFromFile("default.xml");

            Trace.WriteLine(DateTime.Now.ToString() + "Start UpdateCurrencyAsync()");            

            try
            {
                Exrin.Common.ThreadHelper.RunOnUIThread(async () => {
                    await UpdateCurrencyAsync();
                });
            }
            catch (WebException e)
            {
               // Debug.WriteLine("208 строка " + e.Message);  //TODO: Обработать таймаут!                
                //DisplayAlert("Internet", e.Message, "OK");
                //UpdateLabelDate("Ошибка загрузки курсов");
                if (Application.Current.Properties.ContainsKey("time")) UpdateLabelDate((string)Application.Current.Properties["time"]);


                if ((SourceUrl == App.cbr) && (Application.Current.Properties.ContainsKey("USD")))
                {

                    _valutes.Clear();
                    _valutes.Add(rub);
                    LoadCurrencyFromProperties();                   
                    
                }  else
                {
                    _valutes.Clear();
                    _valutes.Add(eur);
                    LoadCurrencyFromProperties();
                }


            }

            if (Valuta.Count > 2) {
                if (Application.Current.Properties.ContainsKey("pickersID0"))
                {
                    System.Diagnostics.Debug.WriteLine($"pickersID0 = {Application.Current.Properties["pickersID0"]}");
                    pickersID[0] = (int)Application.Current.Properties["pickersID0"];
                }
                if (Application.Current.Properties.ContainsKey("pickersID1"))
                {
                    System.Diagnostics.Debug.WriteLine($"pickersID1 = {Application.Current.Properties["pickersID1"]}");                    
                    pickersID[1] = (int)Application.Current.Properties["pickersID1"];
                }               
            }           


            UpdateLabelDate(""); //на текущую дату
            buttonDigitComma.Text = separator.ToString();

            pickerCurrencyOne.ItemsSource = Valuta;
            pickerCurrencyOne.ItemDisplayBinding = new Binding("ForPicker");
            pickerCurrencyOne.SelectedIndex = pickersID[0];
            
            //labelDigitsOneCurrency.Text = rub.CharCode;
            //if (_valutes[(int)FavoritesCurrency.RUR].CharCode != null) labelDigitsOneCurrency.Text = _valutes[(int)FavoritesCurrency.RUR].Symbol;
            labelDigitsOneCurrency.Text = _valutes[pickersID[0]].Symbol;

            pickerCurrencyTwo.ItemsSource = Valuta;
            pickerCurrencyTwo.ItemDisplayBinding = new Binding("ForPicker");
            pickerCurrencyTwo.SelectedIndex = pickersID[1];

            
            
            //labelDigitsTwoCurrency.Text = usd.CharCode;
            //if (_valutes[(int)FavoritesCurrency.GBP].CharCode != null) labelDigitsTwoCurrency.Text = _valutes[(int)FavoritesCurrency.GBP].Symbol;
            labelDigitsTwoCurrency.Text = _valutes[pickersID[1]].Symbol;

            //labelDigitsOne.FontAttributes = FontAttributes.Bold;

            labelDigitsOne.Text = MySumString;
            labelDigitsTwo.Text = "0";


            string charcode1 = _valutes[pickersID[0]].Symbol; //код первой валюты 
            string charcode2 = _valutes[pickersID[1]].Symbol; //код второй валюты 

            SetlabelCurrencyRate(charcode1, charcode2);

            //imageCurrencyOne.SetValue(Image.SourceProperty, "images\sync24px.png");

            //_labelImageOne = this.FindByName<Image>("imageCurrencyOne");            

            //imageCurrencyOne.Source = new UriImageSource
            //{
            //    Uri = new Uri("http://xamarin.com/content/images/pages/forms/example-app.png"),
            //    CachingEnabled = true,
            //    CacheValidity = new TimeSpan(5, 0, 0, 0)
            //};

            //imageCurrencyOne.Source = ImageSource.FromResource("CurrencyCalc2.images.ru.png");
            //imageCurrencyTwo.Source = ImageSource.FromResource("CurrencyCalc2.images.gb.png");

            //imageCurrencyOne.Source = ImageSource.FromResource(
            //    CurrencyImage(_valutes[pickerCurrencyOne.SelectedIndex].CharCode.Remove(_valutes[pickerCurrencyOne.SelectedIndex].CharCode.Length - 1)));

            imageCurrencyOne.Source = _valutes[pickerCurrencyOne.SelectedIndex].GetImg(_valutes[pickerCurrencyOne.SelectedIndex].CharCode);

            //imageCurrencyTwo.Source = ImageSource.FromResource(
            //    CurrencyImage(_valutes[pickerCurrencyTwo.  SelectedIndex].CharCode.Remove(_valutes[pickerCurrencyTwo.SelectedIndex].CharCode.Length - 1)));
            imageCurrencyTwo.Source = _valutes[pickerCurrencyTwo.SelectedIndex].GetImg(_valutes[pickerCurrencyTwo.SelectedIndex].CharCode);

            //this.BackgroundImage = ImageSource.FromResource("CurrencyCalc2.images.background.jpg"); //"CurrencyCalc2.images.background.jpg";

#if DEBUG
            PrintProperties();
#endif

            LabelTwoUpdate(CalculateItog(labelDigitsOne.Text));

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            //buttonInfo tap
            //var buttonInfo_tap = new TapGestureRecognizer();
            //buttonInfo_tap.Tapped += (s, e) =>
            //{
            //    Debug.WriteLine("info click");
            //    var aboutpage = new AboutView();
            //    AboutView_Click(aboutpage);
            //};
            //buttonInfo.GestureRecognizers.Add(buttonInfo_tap);




            //_btnImage = this.FindByName<Image>("ButtonImage");

            //_btnImage.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = new Command(async () => await OnConnect()),
            //});

            void tapClickLabelOne()
            {
                //labelDigitsTwo.FontAttributes = FontAttributes.None;
                //labelDigitsOne.FontAttributes = FontAttributes.Bold;
                // frameTwo.BorderColor = Color.Default;
                // frameTwo.HasShadow = false;
                frameOne.HasShadow = true;
                //frameOne.BorderColor = Color.Accent;

                labelOneActive = true;
                labelChange = true;
                firstnumber = false;
                lastnumber = false;

                if ((pickerCurrencyTwo.SelectedIndex != -1) && (pickerCurrencyOne.SelectedIndex != -1))
                {
                    cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal);
                    cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal);

                    double currency_one = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value);
                    double currency_two = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value);
                    // еще вычисления
                    CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

                    charcode1 = _valutes[pickerCurrencyOne.SelectedIndex].Symbol; //код первой валюты 
                    charcode2 = _valutes[pickerCurrencyTwo.SelectedIndex].Symbol; //код второй валюты 

                    SetlabelCurrencyRate(charcode1, charcode2);                                  

                    //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text); тобы не пересчитывать, а то курс "едет"
                }
            }

            //void tapClickLAbelTwo()
            //{
            //    //labelDigitsOne.FontAttributes = FontAttributes.None;
            //    //labelDigitsTwo.FontAttributes = FontAttributes.Bold;
            //    frameOne.BorderColor = Color.Default;
            //    frameOne.HasShadow = false;
            //    // frameTwo.BorderColor = Color.Accent;
            //    // frameTwo.HasShadow = true;

            //    labelOneActive = false;
            //    labelChange = true;
            //    firstnumber = false;
            //    lastnumber = false;

            //    if ((pickerCurrencyTwo.SelectedIndex != -1) && (pickerCurrencyOne.SelectedIndex != -1))
            //    {
            //        cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal);
            //        cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal);

            //        charcode1 = _valutes[pickerCurrencyTwo.SelectedIndex].Symbol; //код первой валюты 
            //        charcode2 = _valutes[pickerCurrencyOne.SelectedIndex].Symbol; //код второй валюты 

            //        double currency_one = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value);
            //        double currency_two = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value);

            //        CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

            //        SetPickersID(pickerCurrencyOne.SelectedIndex, pickerCurrencyTwo.SelectedIndex);
            //        //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);
            //    }
            //}



            //labelChange tap
            var labelChangeStackLayont_tap = new TapGestureRecognizer();
            labelChangeStackLayont_tap.Tapped += (s, e) =>
            {

                //labelChangeStackLayont.Rotation = 0;
                //labelChangeStackLayont.RotateTo(180, 500);
                labelChangeStackLayont.RotationX = 0;
                labelChangeStackLayont.RotateXTo(360, 500, Easing.CubicInOut);

                int iTmp;
                //if (labelDigitsOne.FontAttributes == FontAttributes.Bold)  //первый  активен!!
                //{
                    iTmp = pickerCurrencyTwo.SelectedIndex;
                    pickerCurrencyTwo.SelectedIndex = pickerCurrencyOne.SelectedIndex;
                    pickerCurrencyOne.SelectedIndex = iTmp;
                SetPickersID(pickerCurrencyOne.SelectedIndex, pickerCurrencyTwo.SelectedIndex);
                tapClickLabelOne();
                //}
                //else
                //{
                //    iTmp = pickerCurrencyOne.SelectedIndex;
                //    pickerCurrencyOne.SelectedIndex = pickerCurrencyTwo.SelectedIndex;
                //    pickerCurrencyTwo.SelectedIndex = iTmp;
                //    tapClickLAbelTwo();

                //    //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text); 
                //};

                
            };
            labelChangeStackLayont.GestureRecognizers.Add(labelChangeStackLayont_tap);

            _btnImage = this.FindByName<Image>("ButtonImage");
            _btnImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await OnConnect()),
            });

            var imageCurrencyOne_tap = new TapGestureRecognizer();
            imageCurrencyOne_tap.Tapped += (s, e) =>
            {
                pickerCurrencyOne.Focus();
               // Debug.WriteLine("imageCurrencyOne_tap");
            };
            imageCurrencyOne.GestureRecognizers.Add(imageCurrencyOne_tap);

            var imageCurrencyTwo_tap = new TapGestureRecognizer();
            imageCurrencyTwo_tap.Tapped += (s, e) =>
            {
                pickerCurrencyTwo.Focus();
              //  Debug.WriteLine("imageCurrencyTwo_tap");
            };
            imageCurrencyTwo.GestureRecognizers.Add(imageCurrencyTwo_tap);

            // labelUpdateStart tap event
            var labelUpdateDate_tap = new TapGestureRecognizer();
            labelUpdateDate_tap.Tapped += (s, e) =>
            {
                //  Debug.WriteLine("Start update currency");
                new Command(async () => await OnConnect());
                Exrin.Common.ThreadHelper.RunOnUIThread(async () => { await UpdateCurrencyAsync(); });
                UpdateLabelDate("");

            };
            //labelUpdateDate.GestureRecognizers.Add(labelUpdateDate_tap);
            ButtonImage.GestureRecognizers.Add(labelUpdateDate_tap);

            // stackLayontCurrencyOne Long tap event
            var stackLayontCurrencyOne_doubletap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            stackLayontCurrencyOne_doubletap.Tapped += (s, e) =>
            {
                //Debug.WriteLine(stackLayontCurrencyOne.ToString() + " Double tapped");
                Clipboard.SetTextAsync(labelDigitsOne.Text);
                DisplayAlert("Copied", labelDigitsOne.Text, "Ok");
            };
            //stackLayontCurrencyOne.GestureRecognizers.Add(stackLayontCurrencyOne_doubletap);
            frameOne.GestureRecognizers.Add(stackLayontCurrencyOne_doubletap);

            // stackLayontCurrencyTwo Long tap event
            var stackLayontCurrencyTwo_doubletap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            stackLayontCurrencyTwo_doubletap.Tapped += (s, e) =>
            {
                //Debug.WriteLine(stackLayontCurrencyOne.ToString() + " Double tapped");
                Clipboard.SetTextAsync(labelDigitsTwo.Text);
                DisplayAlert("Cкопировано", labelDigitsTwo.Text, "Ok");
            };
            stackLayontCurrencyTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_doubletap);
            //frameTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_doubletap);

            // stackLayontCurrencyOne tap event
            var stackLayontCurrencyOne_tap = new TapGestureRecognizer();
            stackLayontCurrencyOne_tap.Tapped += (s, e) =>
            {
                //Debug.WriteLine("Label 2 Tapped ");
                tapClickLabelOne();
            };
            stackLayontCurrencyOne.GestureRecognizers.Add(stackLayontCurrencyOne_tap);
            //imageCurrencyOne.GestureRecognizers.Add(stackLayontCurrencyOne_tap);
            // labelDigitsTwo tap event
            var stackLayontCurrencyTwo_tap = new TapGestureRecognizer();
            stackLayontCurrencyTwo_tap.Tapped += (s, e) =>
            {
                Debug.WriteLine("Label 1 Tapped ");

                //tapClickLAbelTwo();  //пока отменим активацию второгоа поля, вроде как не нравиться
            };
            stackLayontCurrencyTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_tap);
            //imageCurrencyTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_tap);

            void UpdateLabelDate(string msg)
            {
                //labelUpdateDate.Text = "На " + DateTime.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.SortableDateTimePattern);
                //System.Globalization.CultureInfo.CreateSpecificCulture("en-US") DateTimeFormatInfo.InvariantInfo

                if (String.IsNullOrEmpty(msg))
                {
                    dateTime = DateTime.Now.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture(cultureName));
                    labelUpdateDate.Text = $"{Resx.AppResources.ExchangeRateFor} {dateTime}";
                    Application.Current.Properties["time"] = dateTime;
                } else
                {
                    labelUpdateDate.TextColor = Color.Plum;
                    labelUpdateDate.Text = msg;
                }

                
            }

        }

#if DEBUG
        private void PrintProperties ()
        {
            IDictionary<string, object> properties = Application.Current.Properties;

            Debug.WriteLine(string.Format("==============================================================="));
            foreach (var prop in properties)
            {
                Debug.WriteLine(string.Format("found key: {0} value: {1}", prop.Key, prop.Value));

            }
            Debug.WriteLine(string.Format("==============================================================="));
        }
#endif


        //загрузка ранее сохраненных валют
        private void LoadCurrencyFromProperties()
        {
            IDictionary<string, object> properties = Application.Current.Properties;

            List<string> vals = new List<string>();

            // foreach (var elem in doc.Descendants("Valute"))
            foreach (var prop in properties) {
                if (prop.Key.Length == 3)
                {
                    Debug.WriteLine(string.Format("found key: ", prop.Key));

                    string tmp = (string)prop.Value;

                    vals = tmp.Split('#').Distinct().ToList();

                    foreach (string elem in vals)
                    {
                        Debug.WriteLine(string.Format("found values: ", elem));
                    }

                    //Currency cur = new Currency(prop.Key, nominal, name, value, symbol);

                    //запишем
                    //Application.Current.Properties[charcode] = nominal + "#" + name + "#" + value + "#" + symbol;
                }
            }

            if (properties.ContainsKey("source"))
            {
                SourceUrl = (string)properties["source"];
            }
        }



        //  protected override void OnSizeAllocated(double widht, double height)
        //  {
        //      base.OnSizeAllocated(widht, height);
        ////      if (isFirst)
        //   //   {
        //          widthAlloc = widht;
        //          heightAlloc = height;
        //   //   }
        //  }

        //  protected override void OnAppearing()
        //  {
        //      base.OnAppearing();
        //     // if (isFirst)
        //     // {
        //          stacklayontGlobal.HeightRequest = heightAlloc;
        //          stacklayontGlobal.WidthRequest = widthAlloc;

        //          this.Content = stacklayontGlobal;

        //          isFirst = false;
        //     // }
        //  }




        public string CurrencyImage(string CurrencyProperty)
        {
            // Debug.WriteLine(string.Format("CurrencyCalc2.images.{0}.png", CurrencyProperty.ToLower()));
            return string.Format("CurrencyCalc2.images.{0}.png", CurrencyProperty.ToLower());
        }


        //private void ShowAboutClicked(object sender, EventArgs e)
        //{
        //    if (this.overlay.IsVisible)
        //    {
        //        this.overlay.IsVisible = false;                
        //        this.stacklayontMain.IsVisible = true;
        //        if (sender is Button button)
        //        {
        //            button.Rotation = 0;
        //            button.RotateTo(180,500);
        //            button.Text = symbolAbout;
        //        }

        //        //stacklayontGlobal.HorizontalOptions = LayoutOptions.Center;
        //        //stacklayontGlobal.VerticalOptions = LayoutOptions.Center;
        //        imageLogofull.IsVisible = false;                
        //        imageLogofull.Opacity = 0;
        //        //imageLogo.IsVisible = true;         

        //       // this.TranslationX = 0;
        //       // this.TranslateTo(0, 0, typeof(double)(Application.Current.MainPage.Width));
        //    }
        //    else {
        //        if (sender is Button button)
        //        {
        //            button.Rotation = 0;
        //            button.RotateTo(180,500);

        //            switch (Device.RuntimePlatform)
        //            {
        //                case Device.UWP:
        //                    button.Text = "\uE8BB";
        //                    break;
        //                default:
        //                    button.Text = "\uf00d";                            
        //                    break;
        //            }


        //        }
        //        this.stacklayontMain.IsVisible = false;                
        //        //stacklayontGlobal.HorizontalOptions = LayoutOptions.StartAndExpand;
        //        //stacklayontGlobal.VerticalOptions = LayoutOptions.Start;
        //        this.overlay.IsVisible = true;                
        //        //imageLogo.IsVisible = false;
        //        imageLogofull.IsVisible = true;
        //        imageLogofull.RotationY = 0;
        //        imageLogofull.RotateYTo(360, 2000, Easing.CubicInOut);
        //        imageLogofull.Opacity = 0;
        //        imageLogofull.FadeTo(1, 2000);
        //        //this.RotationX = 0;
        //        //this.RotateXTo(360, 750, Easing.CubicInOut);
        //        //  imageLogofull.Animate( new FadeToAnimation());
        //    };
        //}      


        private async Task OnConnect()
        {
            //Debug.WriteLine("ONCOnnect!!!");
            _btnImage.Rotation = 0;
            await _btnImage.RotateTo(-360, 250);
            //await Task.Delay(100);
            //await _btnImage.RelRotateTo(-360,250);              
            //await UpdateCurrencyAsync();

        }

        public string GetResourceTextFile(string filename)
        {
            string result = string.Empty;

            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream("CurrencyCalc." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        void LoadCurrencyFromFile(string filename)
        {
            string data = String.Empty;
            int indx1 = -1;
            int indx2 = -1;
            var doc = XDocument.Parse(GetResourceTextFile(filename));
            if (_valutes != null)
            {
                indx1 = pickerCurrencyOne.SelectedIndex;
                indx2 = pickerCurrencyTwo.SelectedIndex;

                _valutes.Clear();
                _valutes.Add(rub);
            }

            foreach (var elem in doc.Descendants("Valute"))//doc.Elements().First().Elements())
            {
                //Debug.WriteLine("DownloadStringCompleted444"+elem.Value);
                string charcode = elem.Element("CharCode").Value;

                string name = "";
               
                name = GetCurrencyName(charcode);
                string symbol = GetCharSymbol(charcode);

                string value = elem.Element("Value").Value;

                value = value.Replace(",", Convert.ToString(separator)); // cbr выдает курсы с запятой

                string nominal = elem.Element("Nominal").Value;

                Currency cur = new Currency(charcode, nominal, name, value, symbol);

                switch (charcode)
                {
                    case "RUR":
                        _valutes.Insert((int)FavoritesCurrency.RUR, cur);
                        break;
                    case "USD":
                        //_valutes.Move(i, (int)FavoritesCurrency.USD);
                        _valutes.Insert((int)FavoritesCurrency.USD, cur);
                        //Debug.WriteLine(i);
                        break;
                    case "EUR":
                        //_valutes.Move(i, (int)FavoritesCurrency.EUR);
                        _valutes.Insert((int)FavoritesCurrency.EUR, cur);
                        break;
                    case "GBP":
                        //_valutes.Move(i, (int)FavoritesCurrency.GBP);                               
                        _valutes.Insert((int)FavoritesCurrency.GBP, cur);
                        break;
                    case "CNY":
                        _valutes.Insert((int)FavoritesCurrency.CNY, cur);
                        //_valutes.Move(i, (int)FavoritesCurrency.CNY);
                        break;
                    case "JPY":
                        //_valutes.Move(i, (int)FavoritesCurrency.JPY);
                        _valutes.Insert((int)FavoritesCurrency.JPY, cur);
                        break;
                    case "CHF":
                        //_valutes.Move(i, (int)FavoritesCurrency.CHF);
                        _valutes.Insert((int)FavoritesCurrency.CHF, cur);
                        break;
                    default:
                        _valutes.Add(cur);
                        break;
                }

            }

            pickerCurrencyOne.SelectedIndex = pickersID[0];
            pickerCurrencyTwo.SelectedIndex = pickersID[1];
          
        }

        public async Task UpdateCurrencyAsync()
        {

            //if (!hasInternet) return; //нет инета, выходим.

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var client = new WebClient() { Encoding = Encoding.GetEncoding(1251) };

            client.Credentials = CredentialCache.DefaultCredentials;            

            string data = string.Empty;

            try
            {
                client.DownloadStringCompleted += (o, e) =>
                {
                    if (e.Error != null) throw e.Error;
                    if (e.Result == null) return;

                    if (SourceUrl == App.cbr)
                    {
                        _valutes.Clear();
                        _valutes.Add(rub);
                        ParseResultCbr(e);
                    } else
                    {
                        _valutes.Clear();
                        _valutes.Add(eur);
                        ParseResultECB(e);                        
                    }
                    
                };

                data = await client.DownloadStringTaskAsync(SourceUrl);

                // data = GetResourceTextFile("default.xml"); //TODO сделать сохранение в этот файл успешно загруженного xml
                //Debug.WriteLine(data);
            }
            catch (Exception)
            {
                //await DisplayAlert("Ошибка", webEx.Message, "OK");
                throw;
            }

        }       


        private void ParseResultCbr(DownloadStringCompletedEventArgs e )
        {
            int indx1, indx2 = -1;            

            XDocument doc = XDocument.Parse(e.Result);

            if (_valutes != null)
            {
                indx1 = pickerCurrencyOne.SelectedIndex;
                indx2 = pickerCurrencyTwo.SelectedIndex;

            }

            foreach (var elem in doc.Descendants("Valute"))//doc.Elements().First().Elements())
            {               
                string charcode = elem.Element("CharCode").Value;

                string name = "";
                name = GetCurrencyName(charcode);
                //if (charcode == "GBP")
                //{
                //    name = "Фунт Соединенного Королевства";
                //}
                //else
                //{
                //    name = elem.Element("Name").Value;
                //}

                string symbol = GetCharSymbol(charcode);

                string value = elem.Element("Value").Value;

                value = value.Replace(",", Convert.ToString(separator)); // cbr выдает курсы с запятой

                string nominal = elem.Element("Nominal").Value;

                Currency cur = new Currency(charcode, nominal, name, value, symbol);

                //запишем
                Application.Current.Properties[charcode] = nominal + "#" + name + "#" + value + "#" + symbol;   

                Debug.WriteLine($"{charcode}, {nominal}, {name}, {value}, {symbol}");

                switch (charcode)
                {
                    case "RUR":
                        _valutes.Insert((int)FavoritesCurrency.RUR, cur);
                        break;
                    case "USD":
                        //_valutes.Move(i, (int)FavoritesCurrency.USD);
                        _valutes.Insert((int)FavoritesCurrency.USD, cur);
                        //Debug.WriteLine(i);
                        break;
                    case "EUR":
                        //_valutes.Move(i, (int)FavoritesCurrency.EUR);
                        _valutes.Insert((int)FavoritesCurrency.EUR, cur);
                        break;
                    case "GBP":
                        //_valutes.Move(i, (int)FavoritesCurrency.GBP);                               
                        _valutes.Insert((int)FavoritesCurrency.GBP, cur);
                        break;
                    case "CNY":
                        _valutes.Insert((int)FavoritesCurrency.CNY, cur);
                        //_valutes.Move(i, (int)FavoritesCurrency.CNY);
                        break;
                    case "JPY":
                        //_valutes.Move(i, (int)FavoritesCurrency.JPY);
                        _valutes.Insert((int)FavoritesCurrency.JPY, cur);
                        break;
                    case "CHF":
                        //_valutes.Move(i, (int)FavoritesCurrency.CHF);
                        _valutes.Insert((int)FavoritesCurrency.CHF, cur);
                        break;
                    default:
                        //_valutes.Add(cur);
                        SetValutes(cur);
                        break;
                }

            }
        }

        //Заполняем коллекцию валют
        private void SetValutes (Currency currency)
        {          

            int indx1, indx2 = -1;

            if (_valutes != null)
            {
                indx1 = pickerCurrencyOne.SelectedIndex;   
                indx2 = pickerCurrencyTwo.SelectedIndex;

                //TODO: сортировать по частоте испольования.
            }

            _valutes.Add(currency);

        }

        //Разбор ответа с ЕЦБ
        public void ParseResultECB(DownloadStringCompletedEventArgs e)
        {   
            XmlDocument xDoc = new XmlDocument();

            xDoc.LoadXml(e.Result);           

            string symbol, charcode, value, nominal, name = String.Empty;         

            foreach (XmlNode node in xDoc.DocumentElement.ChildNodes)
            {
                foreach (XmlNode locNode in node)
                {
                    if (locNode.Name == "Cube")
                    {
                        foreach (XmlNode cubeNode in locNode)
                        {                            
                            bool bOneNode = false;
                            charcode = ""; value = "1";
                            foreach (XmlAttribute atr in cubeNode.Attributes)
                            {   
                                if (atr.Name == "currency") charcode = atr.Value;
                                
                                if (atr.Name == "rate")
                                {
                                    value = atr.Value;                                    
                                    bOneNode = true;
                                }
                                
                                if (bOneNode)
                                {
                                    nominal = "1";
                                    symbol = GetCharSymbol(charcode);
                                    value = value.Replace(".", Convert.ToString(separator));
                                    name = GetCurrencyName(charcode);
                                    Currency cur = new Currency(charcode, nominal, name, value, symbol);
                                    Application.Current.Properties[charcode] = nominal + "#" + name + "#" + value + "#" + symbol;
                                    Debug.WriteLine($"{charcode}, {nominal}, {name}, {value}, {symbol}");
                                    //_valutes.Add(cur);
                                    SetValutes(cur);
                                    bOneNode = false;
                                }
                            }                           
                        }
                    }
                }
            }   
        }

        private string GetCurrencyName(string charcode)
        {
            string result;
           
            switch (charcode)
            {
                case "RUR":
                case "RUB":
                    result = Resx.AppResources.RUB;
                    break;
                case "EUR":
                    result = Resx.AppResources.EUR;
                    break;
                case "AUD":
                    result = Resx.AppResources.AUD;
                    break;
                case "USD":
                    result = Resx.AppResources.USD;
                    break;
                case "JPY":
                    result = Resx.AppResources.JPY;
                    break;
                case "BGN":
                    result = Resx.AppResources.BGN;
                    break;
                case "CZK":
                    result = Resx.AppResources.CZK;
                    break;
                case "DKK":
                    result = Resx.AppResources.DKK;
                    break;
                case "GBP":
                    result = Resx.AppResources.GBP;
                    break;
                case "HUF":
                    result = Resx.AppResources.HUF;
                    break;
                case "PLN":
                    result = Resx.AppResources.PLN;
                    break;
                case "RON":
                    result = Resx.AppResources.RON;
                    break;
                case "SEK":
                    result = Resx.AppResources.SEK;
                    break;
                case "ISK":
                    result = Resx.AppResources.ISK;
                    break;
                case "NOK":
                    result = Resx.AppResources.NOK;
                    break;
                case "HRK":
                    result = Resx.AppResources.HRK;
                    break;
                case "TRY":
                    result = Resx.AppResources.TRY;
                    break;
                case "BRL":
                    result = Resx.AppResources.BRL;
                    break;
                case "CAD":
                    result = Resx.AppResources.CAD;
                    break;
                case "CNY":
                    result = Resx.AppResources.CNY;
                    break;
                case "HKD":
                    result = Resx.AppResources.HKD;
                    break;
                case "IDR":
                    result = Resx.AppResources.IDR;
                    break;
                case "ILS":
                    result = Resx.AppResources.ILS;
                    break;
                case "INR":
                    result = Resx.AppResources.INR;
                    break;
                case "KRW":
                    result = Resx.AppResources.KRW;
                    break;
                case "MXN":
                    result = Resx.AppResources.MXN;
                    break;
                case "MYR":
                    result = Resx.AppResources.MYR;
                    break;
                case "NZD":
                    result = Resx.AppResources.NZD;
                    break;
                case "PHP":
                    result = Resx.AppResources.PHP;
                    break;
                case "SGD":
                    result = Resx.AppResources.SGD;
                    break;
                case "THB":
                    result = Resx.AppResources.THB;
                    break;
                case "ZAR":
                    result = Resx.AppResources.ZAR;
                    break;
                case "CHF":
                    result = Resx.AppResources.CHF;
                    break;
                case "AZN":
                    result = Resx.AppResources.AZN;
                    break;
                case "AMD":
                    result = Resx.AppResources.AMD;
                    break;
                case "BYN":
                    result = Resx.AppResources.BYN;
                    break;
                case "KZT":
                    result = Resx.AppResources.KZT;
                    break;
                case "KGS":
                    result = Resx.AppResources.KGS;
                    break;
                case "XDR":
                    result = Resx.AppResources.XDR;
                    break;
                case "TJS":
                    result = Resx.AppResources.TJS;
                    break;
                case "TMT":
                    result = Resx.AppResources.TMT;
                    break;
                case "UZS":
                    result = Resx.AppResources.UZS;
                    break;
                case "UAH":
                    result = Resx.AppResources.UAH;
                    break;
                case "MDL":
                    result = Resx.AppResources.MDL;
                    break;
                default:
                    result = charcode;
                    break;
            }
            return result;
        }

        private void PickerCurrencyOne_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string charcode1 = "", charcode2 = "";
            double currency_one = 0, currency_two = 0;
            //if (pickerCurrencyOne.SelectedIndex != -1) return;
            try
            {

                //Debug.WriteLine("pickerCurrencyOne.SelectedIndex= "+ pickerCurrencyOne.SelectedIndex);
                //Debug.WriteLine("pickerCurrencyOne.Items.Count= " + pickerCurrencyOne.Items.Count);
                //Debug.WriteLine("pickerCurrencyTwo.SelectedIndex= " + pickerCurrencyTwo.SelectedIndex);
                if (pickerCurrencyOne.SelectedIndex != -1 && pickerCurrencyOne.SelectedIndex <= pickerCurrencyOne.Items.Count && pickerCurrencyTwo.SelectedIndex != -1)
                {
                    Debug.WriteLine("pickerCurrencyTwo.SelectedIndex in");

                    SetPickersID(pickerCurrencyOne.SelectedIndex, pickerCurrencyTwo.SelectedIndex);
                    imageCurrencyOne.Source = ImageSource.FromResource(CurrencyImage(_valutes[pickerCurrencyOne.SelectedIndex].CharCode.Remove(_valutes[pickerCurrencyOne.SelectedIndex].CharCode.Length - 1)));
                    //узнаем какая строчка активна, и изходя из этого делаем вычисления
                    if (labelOneActive)
                    {   //активна первая
                        //устанавливаем сколько за одну единиц валюты активной строки дают валюты из второй строки
                        //берем значения из комбобокса активной строки и комбобокса второй строки
                        charcode1 = _valutes[pickerCurrencyOne.SelectedIndex].Symbol; //код первой валюты 
                        labelDigitsOneCurrency.Text = charcode1; //обновим знак валюты
                        charcode2 = _valutes[pickerCurrencyTwo.SelectedIndex].Symbol; //код второй валюты                                                    

                        cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal); //номинал валюты активной строки                    
                        currency_one = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value); //курс к рублю первой валюты

                        cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal); //номинал второй валюты
                        currency_two = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value); //курс к рублю второй валюты

                        CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

                        // обновляем тект с курсом валют
                        SetlabelCurrencyRate(charcode1, charcode2);
                        //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text);
                        LabelTwoUpdate(CalculateItog(labelDigitsOne.Text));

                    }
                    else // активна вторая 
                    {
                        charcode1 = _valutes[pickerCurrencyTwo.SelectedIndex].Symbol;
                        cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal);
                        currency_one = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value);

                        charcode2 = _valutes[pickerCurrencyOne.SelectedIndex].Symbol;

                        labelDigitsOneCurrency.Text = charcode2; //обновим знак валюты
                        cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal);
                        currency_two = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value);

                        CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

                        SetlabelCurrencyRate(charcode1, charcode2);
                        //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);

                        LabelOneUpdate(CalculateItog(labelDigitsTwo.Text));
                    }

                   

                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //Trace.WriteLine(ex.Source.ToString());
                DisplayAlert("Ошибка PickerCurrencyOne_OnSelectedIndexChanged", ex.Message, "Ок");
            }
        }

        private void SetPickersID(int a, int b)
        {
            if ((a != -1) && (b != -1))
            {
                pickersID[0] = a;                
                pickersID[1] = b;
                Application.Current.Properties["pickersID0"] = pickersID[0];
                Application.Current.Properties["pickersID1"] = pickersID[1];
            }            
        }

        private void PickerCurrencyTwo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string charcode1, charcode2;
                double currency_one, currency_two;
                //if (pickerCurrencyTwo.SelectedIndex != -1) return;
                if (pickerCurrencyTwo.SelectedIndex != -1 && pickerCurrencyTwo.SelectedIndex <= pickerCurrencyTwo.Items.Count && pickerCurrencyOne.SelectedIndex != -1)
                {
                    SetPickersID(pickerCurrencyOne.SelectedIndex, pickerCurrencyTwo.SelectedIndex);
                    imageCurrencyTwo.Source = ImageSource.FromResource(CurrencyImage(_valutes[pickerCurrencyTwo.SelectedIndex].CharCode.Remove(_valutes[pickerCurrencyTwo.SelectedIndex].CharCode.Length - 1)));
                    if (labelOneActive)
                    {
                        charcode1 = _valutes[pickerCurrencyOne.SelectedIndex].Symbol; //код первой валюты 
                        charcode2 = _valutes[pickerCurrencyTwo.SelectedIndex].Symbol; //код второй валюты

                        labelDigitsTwoCurrency.Text = charcode2; //обновим знак валюты

                        cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal); //номинал валюты активной строки                    
                        currency_one = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value); //курс к рублю первой валюты

                        cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal); //номинал второй валюты
                        currency_two = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value); //курс к рублю второй валюты

                        CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

                        // обновляем тект с курсом валют
                        SetlabelCurrencyRate(charcode1, charcode2);
                        // вычисляем
                        //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text);
                        LabelTwoUpdate(CalculateItog(labelDigitsOne.Text));

                    }
                    else // активна вторая 
                    {
                        charcode1 = _valutes[pickerCurrencyTwo.SelectedIndex].Symbol; //код первой валюты 
                        charcode2 = _valutes[pickerCurrencyOne.SelectedIndex].Symbol; //код второй валюты 

                        labelDigitsTwoCurrency.Text = charcode1; //обновим знак валюты

                        cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal);
                        currency_one = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value);

                        cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal);
                        currency_two = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value);

                        CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

                        SetlabelCurrencyRate(charcode1, charcode2);
                        //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);
                        LabelOneUpdate(CalculateItog(labelDigitsTwo.Text));

                    }                    
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //Trace.WriteLine(ex.Source.ToString());
                DisplayAlert("Ошибка PickerCurrencyTwo_OnSelectedIndexChanged", ex.Message, "Ок");
            }

        }

        private double CalculateCrossRate(double currency1, int nominal1, double currency2, int nominal2)
        {
            try
            {
                if ((nominal1 != 0) && (nominal2 != 0))
                {

                    if (SourceUrl == App.ecb)
                    {
                        cross_kurs2 = ((currency2 / nominal2) / (currency1 / nominal1));
                        return cross_kurs = ((currency1 / nominal1) / (currency2 / nominal2)); 
                    }
                    else {
                        cross_kurs2 = ((currency1 / nominal1) / (currency2 / nominal2));
                        return cross_kurs = ((currency2 / nominal2) / (currency1 / nominal1));
                    }
                }
            }
            catch (Exception ex)
            {
                //                Trace.WriteLine("Ошибка: " + ex.Message);  
                DisplayAlert("Ошибка CalculateCrossRate", ex.Message, "Ок");
            }
            return 0;
        }

        private void SetlabelCurrencyRate(string charcode1, string charcode2)
        {
            // обновление текста с котировкой валюты
            string pSpecifier = String.Format("F4");
            labelCurrentCurrencyRate.Text = "1 " + charcode1 + " = " + cross_kurs.ToString(pSpecifier) + " " + charcode2;
            labelCurrentCurrencyRate2.Text = "1 " + charcode2 + " = " + cross_kurs2.ToString(pSpecifier) + " " + charcode1;

        }        

        void LabelOneUpdate(string tmp)
        {
            //labelDigitsOne.RotationX = 0;
            labelDigitsOne.Text = tmp;
            MySumString = tmp;
            labelDigitsOne.SetBinding(Label.TextProperty, "DigitsOne");
            labelDigitsOne.BindingContext = new { DigitsOne = tmp };
            // labelDigitsOne.RotateXTo(360, 500, Easing.CubicIn);
            //Debug.WriteLine("Label1=" + tmp);
        }

        void LabelTwoUpdate(string tmp)
        {
            labelDigitsTwo.RotationX = 0;
            labelDigitsTwo.Text = tmp;
            labelDigitsTwo.SetBinding(Label.TextProperty, "DigitsTwo");
            labelDigitsTwo.BindingContext = new { DigitsTwo = tmp };
            labelDigitsTwo.RotateXTo(360, 750, Easing.CubicInOut);
            //Debug.WriteLine("Label2=" + tmp);
            
        }

        private async void Onclick(object sender, EventArgs args)
        {
            Debug.WriteLine("Click");
            //#if __MOBILE__
            // Xamarin iOS or Android-specific code
            if (sender is Button button)
            {
                Debug.WriteLine(button.Text);
                //button.FindByName(buttonDigitOne)

                await button.ScaleTo(0.5, 100);
                await button.ScaleTo(1, 100);

                switch (button.Text)
                {
                    case "1":
                        SetDigit('1');
                        break;
                    case "2":
                        SetDigit('2');
                        break;
                    case "3":
                        SetDigit('3');
                        break;
                    case "4":
                        SetDigit('4');
                        break;
                    case "5":
                        SetDigit('5');
                        break;
                    case "6":
                        SetDigit('6');
                        break;
                    case "7":
                        SetDigit('7');
                        break;
                    case "8":
                        SetDigit('8');
                        break;
                    case "9":
                        SetDigit('9');
                        break;
                    case "0":
                        SetDigit('0');
                        break;

                    default:
                        Debug.WriteLine(button.Text);
                        break;
                }

            }
            //#endif
        }

        private void OnClickCE(object sender, EventArgs args)
        {

            System.Diagnostics.Debug.WriteLine("CE Tapped ");

            //if (sender is Button button)
            //{
            //    await button.ScaleTo(1.5, 250);
            //    await button.ScaleTo(1, 250);
            //}
            //labelDigitsOne.Text = "0";
            LabelOneUpdate("0");

            // labelDigitsTwo.Text = "0";
            LabelTwoUpdate("0");

            lastnumber = false;
            firstnumber = false;
            currDigits = 1;
        }

        private void OnClickComma(object sender, EventArgs args)
        {

            if (sender is Button button)
            {
                //await button.ScaleTo(1.5, 250);
                //await button.ScaleTo(1, 250);
                SetDigitsComma();
            }

        }

        void SetDigitsComma()
        {
            string tmp = "";
            //Установка разделителя в активную строку
            if (!CheckLimitDigit(currDigits)) return;

            if (labelOneActive)
            {
                if (HaveComma(labelDigitsOne.Text)) return;

                //labelDigitsOne.Text = labelDigitsOne.Text + separator + "00";

                LabelOneUpdate(labelDigitsOne.Text + separator + "00");
                currDigits = currDigits + 1;
                tmp = CalculateItog(labelDigitsOne.Text);
                //labelDigitsTwo.Text = tmp;
                LabelTwoUpdate(tmp);

            }
            else
            {
                if (HaveComma(labelDigitsTwo.Text)) return;
                //labelDigitsTwo.Text = labelDigitsTwo.Text + separator + "00";
                labelDigitsTwo.BindingContext = new { DigitsTwo = labelDigitsTwo.Text + separator + "00" };

                currDigits = currDigits + 1;
                tmp = CalculateItog(labelDigitsTwo.Text);
                //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);
                LabelOneUpdate(tmp);
            };

        }

        bool HaveComma(string tmp)
        {
            return tmp.Contains(separator);
        }

        void OnClickBackSpace(object sender, EventArgs args)
        {
            string tmp = "";
            System.Diagnostics.Debug.WriteLine("BackSpace Tapped ");
            if (labelOneActive)
            {
                tmp = labelDigitsOne.Text;
                StringBuilder sb = new StringBuilder(tmp);
                if (tmp.Length > 1)
                {
                    if (HaveComma(tmp))
                    {
                        if (lastnumber)
                        {
                            sb[tmp.Length - 1] = '0';
                            tmp = sb.ToString(); //заменим на 0 в *.?0
                                                 //labelDigitsOne.Text = tmp;
                            LabelOneUpdate(tmp);
                            lastnumber = false;
                        }
                        else if (firstnumber)
                        {
                            //firstnumber
                            sb[tmp.Length - 2] = '0';
                            tmp = sb.ToString();
                            //labelDigitsOne.Text = tmp;
                            LabelOneUpdate(tmp);
                            firstnumber = false;
                        }
                        else
                        {
                            //удалим всё, вместе с точкой
                            //labelDigitsOne.Text = tmp.Remove(tmp.Length - 3);
                            LabelOneUpdate(tmp.Remove(tmp.Length - 3));
                            firstnumber = false;
                            lastnumber = false;
                            currDigits = currDigits - 2;
                        }
                    }
                    else
                    {
                        //labelDigitsOne.Text = tmp.Remove(tmp.Length - 1);
                        LabelOneUpdate(tmp.Remove(tmp.Length - 1));
                        currDigits = currDigits - 1;
                    }
                    //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text);
                    LabelTwoUpdate(CalculateItog(labelDigitsOne.Text));

                }
                else
                {
                    if (tmp.Length == 1)
                    {
                        //labelDigitsOne.Text = "0";
                        //labelDigitsTwo.Text = "0";
                        LabelOneUpdate("0");
                        LabelTwoUpdate("0");
                        currDigits = 1;
                    }
                }
            }
            else
            {
                tmp = labelDigitsTwo.Text;

                StringBuilder sb = new StringBuilder(tmp);

                if (tmp.Length > 1)
                {
                    if (HaveComma(tmp))
                    {
                        if (lastnumber)
                        {
                            sb[tmp.Length - 1] = '0';
                            tmp = sb.ToString(); //заменим на 0 в *.?0
                                                 //labelDigitsTwo.Text = tmp;
                            LabelTwoUpdate(tmp);

                            lastnumber = false;
                        }
                        else if (firstnumber)
                        {
                            //firstnumber
                            sb[tmp.Length - 2] = '0';
                            tmp = sb.ToString();
                            //labelDigitsTwo.Text = tmp;
                            LabelTwoUpdate(tmp);

                            firstnumber = false;
                        }
                        else
                        {
                            //удалим всё, вместе с точкой
                            //labelDigitsTwo.Text = tmp.Remove(tmp.Length - 3);
                            LabelTwoUpdate(tmp.Remove(tmp.Length - 3));
                            firstnumber = false;
                            lastnumber = false;
                            currDigits = currDigits - 2;
                        }

                    }
                    else
                    {
                        //labelDigitsTwo.Text = tmp.Remove(tmp.Length - 1);
                        LabelTwoUpdate(tmp.Remove(tmp.Length - 1));
                        currDigits = currDigits - 1;
                    }
                    //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);                        
                    LabelOneUpdate(CalculateItog(labelDigitsTwo.Text));
                }
                else
                {
                    if (tmp.Length == 1)
                    {
                        //labelDigitsOne.Text = "0";
                        LabelOneUpdate("0");
                        //labelDigitsTwo.Text = "0";
                        LabelTwoUpdate("0");
                        currDigits = 1;
                    }
                }
            }
        }

        public void SetDigit(char tmp)
        {
            //выводим цифры
            string activestring;

            if (lastnumber) return;

            if (!CheckLimitDigit(currDigits)) return;
            else currDigits = currDigits + 1;

            if (labelOneActive)
            {
                activestring = labelDigitsOne.Text;
                if (labelChange)
                {
                    activestring = "0";
                    labelChange = false;
                    currDigits = 1;
                }
                StringBuilder sb = new StringBuilder(activestring);
                if (activestring != "0")
                {
                    if (HaveComma(activestring))
                    {
                        if (!firstnumber)
                        {
                            firstnumber = true;
                            sb[activestring.Length - 2] = tmp; // заменяем .*?                            
                            activestring = sb.ToString();
                        }
                        else
                        {
                            lastnumber = true;
                            sb[activestring.Length - 1] = tmp; // заменяем .*?
                            activestring = sb.ToString();
                        };
                        //labelDigitsOne.Text = activestring;
                        LabelOneUpdate(activestring);
                    }
                    else
                    {
                        //labelDigitsOne.Text = labelDigitsOne.Text + tmp;
                        LabelOneUpdate(labelDigitsOne.Text + tmp);
                    }

                }
                else
                {
                    //labelDigitsOne.Text = Convert.ToString(tmp); //первый символ ноль
                    LabelOneUpdate(Convert.ToString(tmp));
                }

                //this.OnPropertyChanged("labelDigitsOne");

                //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text);
                LabelTwoUpdate(CalculateItog(labelDigitsOne.Text));
                //this.OnPropertyChanged("labelDigitsTwo");                
            }
            else
            {
                // labelTwoActive
                activestring = labelDigitsTwo.Text;
                if (labelChange)
                {
                    activestring = "0";
                    labelChange = false;
                    currDigits = 1;
                }
                StringBuilder sb = new StringBuilder(activestring);
                if (activestring != "0")
                {
                    if (HaveComma(activestring))
                    {
                        if (!firstnumber)
                        {
                            firstnumber = true;
                            // Trace.WriteLine("firstnumber=" + firstnumber);
                            // Trace.WriteLine("activestring.Length=" + activestring.Length);
                            sb[activestring.Length - 2] = tmp; // заменяем .*?
                                                               // Trace.WriteLine(sb.ToString());
                            activestring = sb.ToString();
                        }
                        else
                        {
                            lastnumber = true;
                            //Trace.WriteLine("lastnumber=" + lastnumber);
                            //Trace.WriteLine("activestring.Length=" + activestring.Length);
                            sb[activestring.Length - 1] = tmp; // заменяем .*?
                            //Trace.WriteLine(sb.ToString());
                            activestring = sb.ToString();
                        };
                        //labelDigitsTwo.Text = activestring;
                        LabelTwoUpdate(activestring);
                    }
                    else
                    {
                        //labelDigitsTwo.Text = labelDigitsTwo.Text + tmp;
                        LabelTwoUpdate(labelDigitsTwo.Text + tmp);
                    }
                }
                else
                {
                    //labelDigitsTwo.Text = Convert.ToString(tmp); //первый символ ноль
                    LabelTwoUpdate(Convert.ToString(tmp));
                }

                //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);
                LabelOneUpdate(CalculateItog(labelDigitsTwo.Text));
            };
        }

        private bool CheckLimitDigit(int max)
        {
            if (max > maxDigits) return false;
            return true;
        }

        private string GetCharSymbol(string s)
        {
            string result = s;

            switch (s)
            {
                case "RUR":
                case "RUB":
                    result = "\U000020BD";
                    break;
                case "AUD":
                case "USD":
                case "CAD":
                case "SGD":
                    result = "\U00000024";
                    break;
                case "GBP":
                    result = "\U000000A3";
                    break;
                case "JPY":
                case "CNY":
                    result = "\U000000A5";
                    break;
                case "KRW":
                    result = "\U000020A9";
                    break;
                case "INR":
                    result = "\U000020B9";
                    break;
                case "EUR":
                    result = "\U000020AC";
                    break;
                case "TRY":
                    result = "\U000020BA";
                    break;
                case "TMT":
                    result = "\U000020BC";
                    break;
                case "BRL":
                    result = "R" + "\U00000024";
                    break;
                case "AZN":
                    result = "\U000020BC";
                    break;
                case "AMD":
                    result = "\U0000058F";
                    break;
                case "HUF":
                    result = "F";
                    break;
                case "BYN":
                    result = "р.";
                    break;
                case "BGN":
                    result = "Лв";
                    break;
                case "HKD":
                    result = "HK" + "\U00000024";
                    break;
                case "KZT":
                    result = "\U000020B8";
                    break;
                case "KGS":
                    result = "c";
                    break;
                case "NOK":
                case "SEK":
                    result = "kr";
                    break;
                case "PLN":
                    result = "zł";
                    break;
                case "XDR":
                    result = "SDR";
                    break;
                case "UAH":
                    result = "UAH";
                    break;
                case "CZK":
                    result = "Kč";
                    break;
                case "ZAR":
                    result = "R";
                    break;
                default:
                    result = s;
                    break;
            }

            return result;
        }        
       
    }


}
