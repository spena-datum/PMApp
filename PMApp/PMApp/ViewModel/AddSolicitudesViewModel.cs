
namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using PMApp.Helper;
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
        private ImageSource imageSource;
        private MediaFile file; //en este atributo va a quedar la foto del usuario
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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion


        #region Constructors

        public AddSolicitudesViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiServices();
            this.ImageSource = "noimage.jpg";
        }

        #endregion


        #region Commands

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize(); //Inicializar la libreria de fotos

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "¿De donde quieres tomar la imagen?",
                "Cancelar",
                null,
                "De la galería",
                "Nueva imagen");

            if (source == "Cancelar")
            {
                this.file = null;
                return;
            }

            if (source == "Nueva imagen")
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }

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

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSucess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "OK");
                return;
            }

            string image64 = null;
            if (this.file != null)
            {
                image64 = FilesHelper.StreamToBase64(this.file.GetStream());
            }


            var solicitud = new Solicitudes
            {
                Fecha = DateTime.Now.ToUniversalTime().AddHours(-6),
                Usuario = "aleboy16@gmail.com",
                EstadoId = 1,
                DescripcionPaquete = Description,
                SucursalId = 2,
                Imagen64b = image64
            };
            var urlAPI = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlSolicitudesController"].ToString();
            var response = await this.apiService.Post(urlAPI, prefix, controller, solicitud);

            if (!response.IsSucess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }

            var newSolicitud = (Solicitudes)response.Result; //Obtenemos el resultado
            var viewModel = SolicitudesViewModel.GetInstance(); //Obtenemos la clase del viewModel
            viewModel.Solicitudes.Add(newSolicitud); //Agregamos a la instancia que ya se encuentra en memoria a la página.

            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

    }
}
