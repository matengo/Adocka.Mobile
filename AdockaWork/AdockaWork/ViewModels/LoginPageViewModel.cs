using Adocka.Mobile.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Adocka.Mobile.ViewModels
{
    public class LoginPageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public LoginPageViewModel(IUserService userService, INavigationService navigationService, IPageDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (_userService.GetUser() != null)
                _navigationService.NavigateAsync("MasterDetailPage/NavigationPage/DeliveryDatesPage");
        }

        public DelegateCommand LoginCommand => new DelegateCommand(Login);
        public async void Login()
        {
            var user = await _userService.LoginUser(Email, Password);
            if (user == null)
            {
                await _dialogService.DisplayAlertAsync("Fel anvnamn eller lösenord", "Fel användarnamn eller lösenord", "Ok");
            }
            else
            {
                await _navigationService.NavigateAsync("MasterDetailPage/NavigationPage/DeliveryDatesPage");
            }
        }
    }
}
