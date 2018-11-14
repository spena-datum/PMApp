
namespace PMApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddSolicitudesViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isEnabled;

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
        }
        #endregion

    }
}
