namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using PMApp.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Input;
    using Xamarin.Forms;
    using ViewModel;

    public class SolicitudesViewModel : BaseViewModel
    {
        private ApiServices apiService;
        private ObservableCollection<Solicitudes> solicitudes;
        private bool isRefreshing;
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

        public SolicitudesViewModel()
        {
            this.apiService = new ApiServices();
            this.LoadSolicitudes();
        }

        //private Image convertedImage;
        //public Image ConvertedImage
        //{
        //    get { return convertedImage; }
        //    set { convertedImage = value; }
        //}

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

            //foreach (var item in Solicitudes)
            //{
            //    ConvertedImage.Source = new Image(Base64StringToImageSource(item.Imagen64b));
                
            //}

            this.IsRefreshing = false;
        }
        //public ImageSource Base64StringToImageSource(string source)
        //{
        //    var byteArray = Convert.FromBase64String(source);
        //    Stream stream = new MemoryStream(byteArray);
        //    return ImageSource.FromStream(() => stream) ;
        //}

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadSolicitudes);
            }
        }
    }
}
