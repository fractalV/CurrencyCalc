using System;
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
using Xamanimation;

namespace CurrencyCalc2
{

    public struct Currency
    {

        public string CharCode { get; set; }
        public string Nominal { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ForPicker { get; set; }       


        public Currency(string charcode, string nominal, string name, string value)
        {
            CharCode = charcode;
            Nominal = nominal;
            Name = name;
            Value = value;
            ForPicker = name;            
        }

    }

    // пытался избавиться от моргания imageLabelTwo
    public class ImagesLabel : INotifyPropertyChanged
    {
        private object img;

        public object ImageOne
        {
            get { return img; }
            set
            {
                if ( img != value )
                {
                    img = value;
                    OnPropertyChanged("ImageOne");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {

        

        private Image _btnImage;        

        enum FavoritesCurrency { RUR, GBP, USD, EUR,  CNY, JPY, CHF };

        string[] FavoritesDefault = new string[7] { "RUR", "GBP", "USD", "EUR", "CNY", "JPY", "CHF" };

        //bool hasInternet = false;

        bool labelOneActive = true;
        bool labelChange = false;
        bool firstnumber = false; //.1
        bool lastnumber = false;  //.x1

        NumberFormatInfo provider = new NumberFormatInfo();
        char separator = Convert.ToChar(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);

        int maxDigits = 16;
        int currDigits = 1;
        double cross_kurs = 1;
        double cross_kurs2 = 1;
        int cross_nominal_one = 1;
        int cross_nominal_two = 1;

        public ObservableCollection<Currency> _valutes = new ObservableCollection<Currency>();     


        public ObservableCollection<Currency> Valuta { get { return _valutes; } }

        Currency rub = new Currency("RUB", "1", "Российский Рубль", "1");
        

        public MainPage()
        {

            

            _valutes.Add(rub);
            InitializeComponent();

            Exrin.Common.ThreadHelper.Init(SynchronizationContext.Current);
            Exrin.Common.ThreadHelper.RunOnUIThread(async () => { await UpdateCurrencyAsync(); });
            Trace.WriteLine(DateTime.Now.ToString() + " - Start of Main");

            UpdateLabelDate();
            buttonDigitComma.Text = separator.ToString();

            pickerCurrencyOne.ItemsSource = Valuta;
            pickerCurrencyOne.ItemDisplayBinding = new Binding("ForPicker");
            pickerCurrencyOne.SelectedIndex = (int)FavoritesCurrency.RUR;
            //labelDigitsOneCurrency.Text = rub.CharCode;
            if (_valutes[(int)FavoritesCurrency.RUR].CharCode != null) labelDigitsOneCurrency.Text = _valutes[(int)FavoritesCurrency.RUR].CharCode;

            pickerCurrencyTwo.ItemsSource = Valuta;
            pickerCurrencyTwo.ItemDisplayBinding = new Binding("ForPicker");
            pickerCurrencyTwo.SelectedIndex = (int)FavoritesCurrency.GBP;
            //labelDigitsTwoCurrency.Text = usd.CharCode;
            if (_valutes[(int)FavoritesCurrency.GBP].CharCode != null) labelDigitsTwoCurrency.Text = _valutes[(int)FavoritesCurrency.GBP].CharCode;

            labelDigitsOne.FontAttributes = FontAttributes.Bold;

            labelDigitsOne.Text = "0";
            labelDigitsTwo.Text = "0";

            string charcode1 = _valutes[(int)FavoritesCurrency.RUR].CharCode; //код первой валюты 
            string charcode2 = _valutes[(int)FavoritesCurrency.GBP].CharCode; //код второй валюты 

            SetlabelCurrencyRate(charcode1, charcode2);

            //imageCurrencyOne.SetValue(Image.SourceProperty, "images\sync24px.png");

            //_labelImageOne = this.FindByName<Image>("imageCurrencyOne");            

            //imageCurrencyOne.Source = new UriImageSource
            //{
            //    Uri = new Uri("http://xamarin.com/content/images/pages/forms/example-app.png"),
            //    CachingEnabled = true,
            //    CacheValidity = new TimeSpan(5, 0, 0, 0)
            //};

            imageCurrencyOne.Source = ImageSource.FromResource("CurrencyCalc2.images.ru.png");
            imageCurrencyTwo.Source = ImageSource.FromResource("CurrencyCalc2.images.gb.png");

            //this.BackgroundImage = ImageSource.FromResource("CurrencyCalc2.images.background.jpg"); //"CurrencyCalc2.images.background.jpg";

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
                labelDigitsTwo.FontAttributes = FontAttributes.None;
                labelDigitsOne.FontAttributes = FontAttributes.Bold;
                frameTwo.BorderColor = Color.Default;
                frameTwo.HasShadow = false;
                frameOne.HasShadow = true;
                frameOne.BorderColor = Color.Accent;

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

                    charcode1 = _valutes[pickerCurrencyOne.SelectedIndex].CharCode; //код первой валюты 
                    charcode2 = _valutes[pickerCurrencyTwo.SelectedIndex].CharCode; //код второй валюты 

                    SetlabelCurrencyRate(charcode1, charcode2);

                    //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text); тобы не пересчитывать, а то курс "едет"
                }
            }

            void tapClickLAbelTwo()
            {
                labelDigitsOne.FontAttributes = FontAttributes.None;
                labelDigitsTwo.FontAttributes = FontAttributes.Bold;
                frameOne.BorderColor = Color.Default;
                frameOne.HasShadow = false;
                frameTwo.BorderColor = Color.Accent;
                frameTwo.HasShadow = true;

                labelOneActive = false;
                labelChange = true;
                firstnumber = false;
                lastnumber = false;

                if ((pickerCurrencyTwo.SelectedIndex != -1) && (pickerCurrencyOne.SelectedIndex != -1))
                {
                    cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal);
                    cross_nominal_two = Int16.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Nominal);

                    charcode1 = _valutes[pickerCurrencyTwo.SelectedIndex].CharCode; //код первой валюты 
                    charcode2 = _valutes[pickerCurrencyOne.SelectedIndex].CharCode; //код второй валюты 

                    double currency_one = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value);
                    double currency_two = Double.Parse(_valutes[pickerCurrencyOne.SelectedIndex].Value);

                    CalculateCrossRate(currency_two, cross_nominal_two, currency_one, cross_nominal_one);

                    SetlabelCurrencyRate(charcode1, charcode2);

                    //labelDigitsOne.Text = CalculateItog(labelDigitsTwo.Text);
                }
            }

            
            
            //labelChange tap
            var labelChangeStackLayont_tap = new TapGestureRecognizer();
            labelChangeStackLayont_tap.Tapped += (s, e) =>
            {
                
                labelChangeStackLayont.Rotation = 0;
                labelChangeStackLayont.RotateTo(180, 500);

                int iTmp;
                if (labelDigitsOne.FontAttributes == FontAttributes.Bold)  //первый  активен!!
                {
                    iTmp = pickerCurrencyTwo.SelectedIndex;
                    pickerCurrencyTwo.SelectedIndex = pickerCurrencyOne.SelectedIndex;
                    pickerCurrencyOne.SelectedIndex = iTmp;
                    tapClickLabelOne();
                }
                else
                {
                    iTmp = pickerCurrencyOne.SelectedIndex;
                    pickerCurrencyOne.SelectedIndex = pickerCurrencyTwo.SelectedIndex;
                    pickerCurrencyTwo.SelectedIndex = iTmp;
                    tapClickLAbelTwo();

                    //labelDigitsTwo.Text = CalculateItog(labelDigitsOne.Text); 
                };
            };
            labelChangeStackLayont.GestureRecognizers.Add(labelChangeStackLayont_tap);

            _btnImage = this.FindByName<Image>("ButtonImage");
            _btnImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await OnConnect()),
            });

            // labelUpdateStart tap event
            var labelUpdateDate_tap = new TapGestureRecognizer();
            labelUpdateDate_tap.Tapped += (s, e) =>
            {
              //  Debug.WriteLine("Start update currency");
                new Command(async () => await OnConnect());
                Exrin.Common.ThreadHelper.RunOnUIThread(async () => { await UpdateCurrencyAsync(); });
                UpdateLabelDate();
                
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
                Clipboard.SetText(labelDigitsOne.Text);
                DisplayAlert("Cкопировано", labelDigitsOne.Text, "Ok");
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
                Clipboard.SetText(labelDigitsTwo.Text);
                DisplayAlert("Cкопировано", labelDigitsTwo.Text, "Ok");
            };
            //stackLayontCurrencyTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_doubletap);
            frameTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_doubletap);

            // stackLayontCurrencyOne tap event
            var stackLayontCurrencyOne_tap = new TapGestureRecognizer();
            stackLayontCurrencyOne_tap.Tapped += (s, e) =>
            {
                //Debug.WriteLine("Label 2 Tapped ");
                tapClickLabelOne();
            };
            stackLayontCurrencyOne.GestureRecognizers.Add(stackLayontCurrencyOne_tap);

            // labelDigitsTwo tap event
            var stackLayontCurrencyTwo_tap = new TapGestureRecognizer();
            stackLayontCurrencyTwo_tap.Tapped += (s, e) =>
            {
                Debug.WriteLine("Label 1 Tapped ");

                tapClickLAbelTwo();
            };
            stackLayontCurrencyTwo.GestureRecognizers.Add(stackLayontCurrencyTwo_tap);

            void UpdateLabelDate()
            {
                //labelUpdateDate.Text = "На " + DateTime.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.SortableDateTimePattern);
                //System.Globalization.CultureInfo.CreateSpecificCulture("en-US") DateTimeFormatInfo.InvariantInfo
                labelUpdateDate.Text = "КУРС НА " + DateTime.Now.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture("ru-ru"));             
            }



            //void Connectivity_ConnectivityChanged(ConnectivityChangedEventArgs e)
            //{
            //    var access = e.NetworkAccess;
            //    var profiles = e.Profiles;
            //    if (access == NetworkAccess.Internet)
            //    {
            //        Debug.WriteLine("Интернет есть");

            //        hasInternet = true;
            //    }
            //    else
            //    {
            //        hasInternet = false;
            //        Debug.WriteLine("отключился интернет");
            //        //labelUpdateDate.Text = "Нет соединения с Интернет";
            //    }
            //}

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


       

        public string CurrencyImage (string CurrencyProperty)
        {
           // Debug.WriteLine(string.Format("CurrencyCalc2.images.{0}.png", CurrencyProperty.ToLower()));
            return string.Format("CurrencyCalc2.images.{0}.png", CurrencyProperty.ToLower()); 
        }
       

        private void ShowAboutClicked(object sender, EventArgs e)
        {
            if (this.overlay.IsVisible)
            {
                this.overlay.IsVisible = false;
                this.stacklayontMain.IsVisible = true;
                //stacklayontGlobal.HorizontalOptions = LayoutOptions.Center;
                //stacklayontGlobal.VerticalOptions = LayoutOptions.Center;
                imageLogofull.IsVisible = false;
                imageLogofull.Opacity = 0;
                //imageLogo.IsVisible = true;
                
            }
            else {               
                this.stacklayontMain.IsVisible = false;
                //stacklayontGlobal.HorizontalOptions = LayoutOptions.StartAndExpand;
                //stacklayontGlobal.VerticalOptions = LayoutOptions.Start;
                this.overlay.IsVisible = true;
                //imageLogo.IsVisible = false;
                imageLogofull.IsVisible = true;
                imageLogofull.Opacity = 0;
                imageLogofull.Animate( new FadeToAnimation());
            };
        }      
      

        private async Task OnConnect()
        {
            //Debug.WriteLine("ONCOnnect!!!");
            _btnImage.Rotation = 0;
            await _btnImage.RotateTo(-360,250);
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

        public async Task UpdateCurrencyAsync()
        {

            //if (!hasInternet) return; //нет инета, выходим.
            try
            {                
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var client = new WebClient() { Encoding = Encoding.GetEncoding(1251) };
                int indx1 = -1;
                int indx2 = -1;
                client.DownloadStringCompleted += (o, e) =>
                {
                  
                    //Debug.WriteLine("DownloadStringCompleted");
                    //XMLparse(e.Result);
                    var doc = XDocument.Parse(e.Result);
                    //Debug.WriteLine(doc.Document);
                    //labelUpdateDate.Text = "Обновлено " + doc.Root.Attribute("Date").Value + System.DateTime.Now.TimeOfDay;                    

                    if (_valutes != null)
                    {


                       
                            indx1 = pickerCurrencyOne.SelectedIndex;
                            indx2 = pickerCurrencyTwo.SelectedIndex;
                                               
                        _valutes.Clear();
                        _valutes.Add(rub);
                    }
                    //Debug.WriteLine(_valutes.Count);
                    foreach (var elem in doc.Descendants("Valute"))//doc.Elements().First().Elements())
                    {
                        //Debug.WriteLine("DownloadStringCompleted444"+elem.Value);
                        string charcode = elem.Element("CharCode").Value;
                        string value = elem.Element("Value").Value;

                        value = value.Replace(",", Convert.ToString(separator)); // cbr выдает курсы с запятой

                        string nominal = elem.Element("Nominal").Value;

                        string name = elem.Element("Name").Value;

                        Currency cur = new Currency(charcode, nominal, name, value);

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
                };

                string data = await client.DownloadStringTaskAsync("http://www.cbr.ru/scripts/XML_daily.asp");
                if (indx1 != -1 && indx2 != -1)
                {
                   // Debug.WriteLine("indx1 " + indx1);
                   // Debug.WriteLine("indx2 " + indx2);
                    pickerCurrencyOne.SelectedIndex = indx1;
                    pickerCurrencyTwo.SelectedIndex = indx2;

                    //pickerCurrencyTwo.SelectedIndexChanged += PickerCurrencyTwo_OnSelectedIndexChanged;

                }
                else
                {
                    pickerCurrencyOne.SelectedIndex = (int)FavoritesCurrency.RUR;
                    pickerCurrencyTwo.SelectedIndex = (int)FavoritesCurrency.GBP;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                await DisplayAlert("Error", e.Message, "Ok");

            }
        }

        public void XMLparse(string tmp)
        {
            var doc = XDocument.Parse(tmp);

            if (doc == null) return;

            //labelUpdateDate.Text = "На " + doc.Root.Attribute("Date").Value;


            foreach (var elem in doc.Descendants("Valute"))//doc.Elements().First().Elements())
            {

                string charcode = elem.Element("CharCode").Value;
                string value = elem.Element("Value").Value;

                value = value.Replace(",", Convert.ToString(separator)); // cbr выдает курсы с запятой

                string nominal = elem.Element("Nominal").Value;

                string name = elem.Element("Name").Value;

                Currency cur = new Currency(charcode, nominal, name, value);

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
                    imageCurrencyOne.Source = ImageSource.FromResource(CurrencyImage(_valutes[pickerCurrencyOne.SelectedIndex].CharCode.Remove(_valutes[pickerCurrencyOne.SelectedIndex].CharCode.Length - 1)));
                    //узнаем какая строчка активна, и изходя из этого делаем вычисления
                    if (labelOneActive)
                    {   //активна первая
                        //устанавливаем сколько за одну единиц валюты активной строки дают валюты из второй строки
                        //берем значения из комбобокса активной строки и комбобокса второй строки
                        charcode1 = _valutes[pickerCurrencyOne.SelectedIndex].CharCode; //код первой валюты 
                        labelDigitsOneCurrency.Text = charcode1; //обновим знак валюты
                        charcode2 = _valutes[pickerCurrencyTwo.SelectedIndex].CharCode; //код второй валюты                                                    

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
                        charcode1 = _valutes[pickerCurrencyTwo.SelectedIndex].CharCode;
                        cross_nominal_one = Int16.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Nominal);
                        currency_one = Double.Parse(_valutes[pickerCurrencyTwo.SelectedIndex].Value);

                        charcode2 = _valutes[pickerCurrencyOne.SelectedIndex].CharCode;
                        
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

        private void PickerCurrencyTwo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string charcode1, charcode2;
                double currency_one, currency_two;
                //if (pickerCurrencyTwo.SelectedIndex != -1) return;
                if (pickerCurrencyTwo.SelectedIndex != -1 && pickerCurrencyTwo.SelectedIndex <= pickerCurrencyTwo.Items.Count && pickerCurrencyOne.SelectedIndex != -1)
                {
                    imageCurrencyTwo.Source = ImageSource.FromResource(CurrencyImage(_valutes[pickerCurrencyTwo.SelectedIndex].CharCode.Remove(_valutes[pickerCurrencyTwo.SelectedIndex].CharCode.Length - 1)));
                    if (labelOneActive)
                    {
                        charcode1 = _valutes[pickerCurrencyOne.SelectedIndex].CharCode; //код первой валюты 
                        charcode2 = _valutes[pickerCurrencyTwo.SelectedIndex].CharCode; //код второй валюты
                        
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
                        charcode1 = _valutes[pickerCurrencyTwo.SelectedIndex].CharCode; //код первой валюты 
                        charcode2 = _valutes[pickerCurrencyOne.SelectedIndex].CharCode; //код второй валюты 
                        
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
                    cross_kurs2 = ((currency1 / nominal1) / (currency2 / nominal2));
                    return cross_kurs = ((currency2 / nominal2) / (currency1 / nominal1));
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

        private string CalculateItog(string tmp)
        {
            if (!String.IsNullOrEmpty(tmp))
            {
                return ((Double.Parse(tmp) * cross_kurs)).ToString("F2", CultureInfo.CurrentCulture);
            }
            else return "0";
        }

        void LabelOneUpdate(string tmp)
        {
            labelDigitsOne.Text = tmp;
            labelDigitsOne.SetBinding(Label.TextProperty, "DigitsOne");
            labelDigitsOne.BindingContext = new { DigitsOne = tmp };
            //Debug.WriteLine("Label1=" + tmp);
        }

        void LabelTwoUpdate(string tmp)
        {

            labelDigitsTwo.Text = tmp;
            labelDigitsTwo.SetBinding(Label.TextProperty, "DigitsTwo");
            labelDigitsTwo.BindingContext = new { DigitsTwo = tmp };
            //Debug.WriteLine("Label2=" + tmp);
        }

        void Onclick(object sender, EventArgs args)
        {
            Debug.WriteLine("Click");
            //#if __MOBILE__
            // Xamarin iOS or Android-specific code
            if (sender is Button button)
            {
                Debug.WriteLine(button.Text);
                //button.FindByName(buttonDigitOne)
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

        void OnClickCE(object sender, EventArgs args)
        {

            System.Diagnostics.Debug.WriteLine("CE Tapped ");
            //labelDigitsOne.Text = "0";
            LabelOneUpdate("0");

            // labelDigitsTwo.Text = "0";
            LabelTwoUpdate("0");

            lastnumber = false;
            firstnumber = false;
            currDigits = 1;
        }

        void OnClickComma(object sender, EventArgs args)
        {
            SetDigitsComma();
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
    }
}
