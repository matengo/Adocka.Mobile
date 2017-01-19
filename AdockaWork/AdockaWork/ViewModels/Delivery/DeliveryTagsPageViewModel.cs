using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Adocka.Mobile.Helpers;
using Adocka.Mobile.Services;
using AdockaClient;
using AdockaClientPCL.Models;
using Prism.Navigation;
using PropertyChanged;
using Xamarin.Forms;

namespace Adocka.Mobile.ViewModels.Delivery
{
    [ImplementPropertyChanged]
    public class DeliveryTagsPageViewModel : INavigationAware
    {
        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<string> ShippingTags { get; set; }

        public DateTime SelectedDate { get; set; }
        public string SelectedShippingTag { get; set; }
        
        public DeliveryTagsPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
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
            if (parameters.ContainsKey("date"))
            {
                this.SelectedDate = (DateTime)parameters["date"];
            }

            var tags = await _api.Delivery.RecentShippingTagsAsync();
            this.ShippingTags = new ObservableCollection<string>(tags);
        }

        private async void OnSelectedShippingTagChanged()
        {
            await _navigationService.NavigateAsync("NavigationPage/DeliveriesPage?date=" + this.SelectedDate + "&tag=" + this.SelectedShippingTag);
        }
    }
}
