using System;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using AdockaClientPCL.Models;
using AdockaClient;
using Adocka.Mobile.Services;
using AdockaClient.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Adocka.Mobile.ViewModels.Contact
{
    [ImplementPropertyChanged]
    public class ContactEditPageViewModel : INavigationAware
    {
        public AdockaDtoPersonDetails ContactDetails { get; set; }
        

        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ICommand SaveCommand { get; set; }

        public ContactEditPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _adockaApiService = adockaService;

            _adockaUser = _userService.GetUser();
            _api = _adockaApiService.GetApiClient(_adockaUser.PersonId, _adockaUser.ApiKey);

            SaveCommand = new Command(async () => await SaveAsync());
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                int personid;
                if(int.TryParse((string)parameters["id"], out personid))
                    this.ContactDetails = await _api.Person.GetContactDetailsAsync(personid);
            }
        }
        public async Task SaveAsync()
        {
            //Save and redirect
            await _api.Person.SaveContactAsync(this.ContactDetails);
            await _navigationService.GoBackAsync();
        }
    }
}
