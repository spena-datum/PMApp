namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using PMApp.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

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

        private async void LoadSolicitudes()
        {
            this.IsRefreshing = true;
            //var urlAPI = Application.Current.Resources["urlAPI"].ToString();
            var response = await this.apiService.GetList<Solicitudes>("https://packagemail.azurewebsites.net", "/api", "/solicitudesapi");
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

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadSolicitudes);
            }
        }
    }
}
