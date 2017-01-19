using Adocka.Mobile.Services;
using AdockaClient;
using AdockaClientPCL.Models;
using Prism.Navigation;
using PropertyChanged;

namespace Adocka.Mobile.ViewModels.Delivery
{
    [ImplementPropertyChanged]
    public class DeliveryPageViewModel : INavigationAware
    {
        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        public DeliveryPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
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

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("id"))
            {
                var orderid = (int)parameters["id"];
            }
                
        }
    }
}
