
namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using View;
    using Xamarin.Forms;

    public class MainViewModel
    {
        public SolicitudesViewModel Solicitudes { get; set; }
        public AddSolicitudesViewModel AddSolicitudes { get; set; }

        public MainViewModel()
        {
            this.Solicitudes = new SolicitudesViewModel();
        }
        public ICommand AddSolicitudesCommand
        {
            get
            {
                return new RelayCommand(GoToAddSolicitud);
            }
            
        }

        private async void GoToAddSolicitud()
        {
            this.AddSolicitudes = new AddSolicitudesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddSolicitudesPage());
            
        }
    }
}
