using System;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using AdockaClientPCL.Models;
using AdockaClient;
using Adocka.Mobile.Services;
using AdockaClient.Models;

namespace Adocka.Mobile.ViewModels.Contact
{
    [ImplementPropertyChanged]
    public class ContactPageViewModel : INavigationAware
    {
        public AdockaDtoPersonDetails ContactDetails { get; set; }
        

        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ContactPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _adockaApiService = adockaService;

            _adockaUser = _userService.GetUser();
            _api = _adockaApiService.GetApiClient(_adockaUser.PersonId, _adockaUser.ApiKey);
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
    }
}
