using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using AdockaWork.Models;
using Prism.Services;
using System.Threading.Tasks;
using AdockaClientPCL.Models;
using AdockaWork.Services;

namespace AdockaWork.ViewModels
{
    public class MainMasterDetailPageViewModel : BindableBase
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
        public MainMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IUserService userService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _userService = userService;
            NavigateCommand = DelegateCommand.FromAsyncHandler(async () => await Navigate());
            
            MenuItems = new List<MenuItem>
            {
                new MenuItem() { Title = "Mina uppdrag", Icon = "itemIcon1.png", Target = "MyNavigationPage/MyWorkItemsPage" },
                new MenuItem() { Title = "Min tillgänglighet", Icon = "itemIcon1.png", Target = "MyNavigationPage/MyAvailabilityPage" },
                new MenuItem() { Title = "Min profil", Icon = "itemIcon1.png", Target = "MyNavigationPage/MyProfilePage" }
            };

            User = _userService.GetUser();
            Name = User.FirstName + " " + User.LastName;
        }
        private async Task Navigate()
        {
            //_dialogService.DisplayAlertAsync(name, "Fel användarnamn eller lösenord", "Ok");
            await _navigationService.NavigateAsync(this.SelectedItem.Target);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}
