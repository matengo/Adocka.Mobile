using System.Collections.Generic;
using System.Threading.Tasks;
using Adocka.Mobile.Models;
using Adocka.Mobile.Services;
using AdockaClientPCL.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Adocka.Mobile.ViewModels
{
    public class MasterDetailPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IUserService _userService;

        public string Name { get; set; }
        public IAdockaApiUser User { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        private MenuItem _selectedItem;
        public MenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                if(value!=null)
                    NavigateCommand.Execute();
            }
        }
        
        public DelegateCommand NavigateCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public MasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IUserService userService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _userService = userService;
            NavigateCommand = DelegateCommand.FromAsyncHandler(async () => await Navigate());
            LogoutCommand = DelegateCommand.FromAsyncHandler(async () => await Logout());

            MenuItems = new List<MenuItem>
            {
                new MenuItem() { Title = "Kunder", Icon = "itemIcon1.png", Target = "NavigationPage/CustomersPage" },
                new MenuItem() { Title = "Beställningar", Icon = "itemIcon1.png", Target = "NavigationPage/OrdersPage" },
                new MenuItem() { Title = "Leveranser", Icon = "itemIcon1.png", Target = "NavigationPage/DeliveryDatesPage" },
                new MenuItem() { Title = "Min profil", Icon = "itemIcon1.png", Target = "NavigationPage/MyProfilePage" }
            };

            User = _userService.GetUser();
            Name = User.FirstName + " " + User.LastName;
        }
        private async Task Navigate()
        {
            //_dialogService.DisplayAlertAsync(name, "Fel användarnamn eller lösenord", "Ok");
            await _navigationService.NavigateAsync(this.SelectedItem.Target);
        }
         
        private async Task Logout()
        {
            await _userService.LogoutUser();
            await _navigationService.NavigateAsync("LoginPage");
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}
