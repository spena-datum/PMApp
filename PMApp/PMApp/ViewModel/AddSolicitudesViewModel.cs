
namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using PMApp.Models;
    using PMApp.Services;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddSolicitudesViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ApiServices apiService;
        #endregion


        #region Properties
        public string Description { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }


        #endregion


        #region Constructors

        public AddSolicitudesViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiServices();
        }

        #endregion


        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo descripción es necesario", "OK");
                return;
            }

            this.isRunning = true;
            this.isEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSucess)
            {
                this.isRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "OK");
                return;
            }

            var solicitudes = new Solicitudes
            {
                Fecha = DateTime.Now.ToUniversalTime().AddHours(-6),
                Usuario = "aleboy16@gmail.com",
                EstadoId = 1,
                DescripcionPaquete = Description,
                SucursalId = 2
            };
            var urlAPI = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlSolicitudesController"].ToString();
            var response = await this.apiService.Post(urlAPI, prefix, controller, solicitudes);

            if (!response.IsSucess)
            {
                this.isRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }
            this.isRunning = false;
            this.isEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

    }
}
