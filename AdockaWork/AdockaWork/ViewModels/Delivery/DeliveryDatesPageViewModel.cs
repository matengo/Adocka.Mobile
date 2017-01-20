using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Adocka.Mobile.Helpers;
using Adocka.Mobile.Models;
using Adocka.Mobile.Services;
using AdockaClient;
using AdockaClientPCL.Models;
using Prism.Navigation;
using PropertyChanged;
using Xamarin.Forms;
using System.Windows.Input;
using Prism.Commands;

namespace Adocka.Mobile.ViewModels.Delivery
{
    [ImplementPropertyChanged]
    public class DeliveryDatesPageViewModel : INavigationAware
    {
        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<DateTime?> DeliveryDates { get; set; }
        public DateTime? SelectedDate { get; set; }
        
        public DeliveryDatesPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
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
            SelectedDate = null;
            var dates = await _api.Delivery.GetDeliveryDatesAsync(DateTime.UtcNow.AddDays(-7), DateTime.Now.AddDays(7));
            this.DeliveryDates = new ObservableCollection<DateTime?>(dates);

        }
        private async void OnSelectedDateChanged()
        {
            if (this.SelectedDate != null)
            {
                var date = SelectedDate.Value.ToString("yyyy-M-d");
                await _navigationService.NavigateAsync("DeliveryTagsPage?date=" + date);
            }
        }
    }
}
