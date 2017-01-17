using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Adocka.Mobile.Models;
using Adocka.Mobile.Services;
using AdockaClient;
using AdockaClientPCL.Models;
using Prism.Mvvm;
using Prism.Navigation;
using System.ComponentModel;
using PropertyChanged;
using Xamarin.Forms;
using Adocka.Mobile.Helpers;
using System.Threading.Tasks;

namespace Adocka.Mobile.ViewModels
{
    [ImplementPropertyChanged]
    public class DeliveriesPageViewModel : INavigationAware
    {
        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<DeliveryDateModel> DeliveryDates { get; set; }
        public ObservableCollection<string> ShippingTags { get; set; }
        public DeliveryDateModel SelectedDate { get; set; }
        public string SelectedShippingTag { get; set; }
        public bool DateSelected => SelectedDate!=null;
        public bool DateNotSelected => SelectedDate==null;

        public Command TagCommand { get; set; }

        public DeliveriesPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _adockaApiService = adockaService;

            _adockaUser = _userService.GetUser();
            _api = _adockaApiService.GetApiClient(_adockaUser.PersonId, _adockaUser.ApiKey);

            TagCommand = new MvvmCommand(async (x) => {
                await TagSelectedCommand();
            }, (x) => !string.IsNullOrEmpty(SelectedShippingTag));
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var dates = await _api.Delivery.GetDeliveryDatesAsync(DateTime.UtcNow.AddDays(-7), DateTime.Now.AddDays(7));
            this.DeliveryDates = new ObservableCollection<DeliveryDateModel>(dates.Where(x=>x.HasValue).Select(x => new DeliveryDateModel { DeliveryDate = x.Value, QtyDeliveries = 0 }));

            var tags = await _api.Delivery.RecentShippingTagsAsync();
            this.ShippingTags = new ObservableCollection<string>(tags);
            
        }
        private async Task TagSelectedCommand()
        {
            await _navigationService.NavigateAsync("NavigationPage/OrdersPage");
        }
    }
}
