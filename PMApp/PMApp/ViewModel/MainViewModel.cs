namespace PMApp.ViewModel
{
    public class MainViewModel
    {
        public SolicitudesViewModel Solicitudes { get; set; }

        public MainViewModel()
        {
            this.Solicitudes = new SolicitudesViewModel();
        }
    }
}
