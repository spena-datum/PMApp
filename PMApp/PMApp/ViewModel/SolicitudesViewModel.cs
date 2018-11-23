namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using PMApp.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class SolicitudesViewModel : BaseViewModel
    {
        #region Attributes
        private ApiServices apiService;
        
        private bool isRefreshing;
        #endregion

        #region Properties
        private ObservableCollection<Solicitudes> solicitudes;

        public ObservableCollection<Solicitudes> Solicitudes
        {
            get { return this.solicitudes; }
            set { this.SetValue(ref this.solicitudes, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        #endregion

        #region Constructors
        public SolicitudesViewModel()
        {
            instances = this;
            this.apiService = new ApiServices();
            this.LoadSolicitudes();
        }

        #endregion

        #region Singleton

        private static SolicitudesViewModel instances;

        public static SolicitudesViewModel GetInstance()
        {
            if (instances == null)
            {
                return new SolicitudesViewModel();
            }

            return instances;
        }

        #endregion

        #region Methods
        private async void LoadSolicitudes()
        {
            this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSucess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "OK");
                return;
            }
            var urlAPI = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlSolicitudesController"].ToString();
            var response = await this.apiService.GetList<Solicitudes>(urlAPI, prefix, controller);
            if (!response.IsSucess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }

            var list = (List<Solicitudes>)response.Result;
            this.Solicitudes = new ObservableCollection<Solicitudes>(list);


            this.IsRefreshing = false;


        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string base64Image = (string)value;

            if (base64Image == null)
                return null;

            // Convert base64Image from string to byte-array
            var imageBytes = System.Convert.FromBase64String(base64Image);

            // Return a new ImageSource
            return ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not implemented as we do not convert back
            throw new NotSupportedException();
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadSolicitudes);
            }
        }
        #endregion





    }
}
