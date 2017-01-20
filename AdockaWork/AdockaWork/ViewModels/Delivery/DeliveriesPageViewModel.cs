using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Adocka.Mobile.Helpers;
using Adocka.Mobile.Services;
using AdockaClient;
using AdockaClient.Models;
using AdockaClientPCL.Models;
using Prism.Navigation;
using PropertyChanged;
using Xamarin.Forms;

namespace Adocka.Mobile.ViewModels.Delivery
{
    [ImplementPropertyChanged]
    public class DeliveriesPageViewModel : INavigationAware
    {
        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<AdockaDtoListOrder> Deliveries { get; set; }
        public AdockaDtoListOrder SelectedDelivery { get; set; }
        public DateTime SelectedDate { get; set; }
        public string SelectedShippingTag { get; set; }
        
        

        public DeliveriesPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
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
            if (parameters.ContainsKey("date") && parameters.ContainsKey("tag"))
            {
                this.SelectedDate = DateTime.Parse((string)parameters["date"]);
                this.SelectedShippingTag = (string)parameters["tag"];
                var deliveries = await _api.Delivery.GetByDeliveryDateAsync(this.SelectedDate, this.SelectedDate, this.SelectedShippingTag);
                this.Deliveries = new ObservableCollection<AdockaDtoListOrder>(deliveries);
            }
        }
        private async void OnSelectedDeliveryChanged()
        {
            await _navigationService.NavigateAsync("DeliveryPage?id=" + this.SelectedDelivery.OrderId);
        }
    }
}
