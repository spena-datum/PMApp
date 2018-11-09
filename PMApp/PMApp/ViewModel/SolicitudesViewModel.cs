namespace PMApp.ViewModel
{
    using Models;
    using PMApp.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class SolicitudesViewModel : BaseViewModel
    {
        private ApiServices apiService;
        private ObservableCollection<Solicitudes> solicitudes;
        public ObservableCollection<Solicitudes> Solicitudes
        {
            get { return this.solicitudes; }
            set { this.SetValue(ref this.solicitudes, value); }
        }

        public SolicitudesViewModel()
        {
            this.apiService = new ApiServices();
            this.LoadSolicitudes();
        }

        private async void LoadSolicitudes()
        {
            var response = await this.apiService.GetList<Solicitudes>("https://packagemail.azurewebsites.net", "/api", "/solicitudesapi");
            if (!response.IsSucess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }

            var list = (List<Solicitudes>)response.Result;
            this.Solicitudes = new ObservableCollection<Solicitudes>(list);
        }
    }
}
