﻿using System;
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
        
        public Command DeliverySelected { get; set; }

        public DeliveriesPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _adockaApiService = adockaService;

            _adockaUser = _userService.GetUser();
            _api = _adockaApiService.GetApiClient(_adockaUser.PersonId, _adockaUser.ApiKey);

            DeliverySelected = new MvvmCommand(async (x) => {
                await OnDeliverySelected();
            }, (x) => SelectedDelivery!=null);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("date") && parameters.ContainsKey("tag"))
            {
                var date = (DateTime)parameters["date"];
                var tag = (string)parameters["tag"];
                var deliveries = await _api.Delivery.GetByDeliveryDateAsync(date, date, tag);
                this.Deliveries = new ObservableCollection<AdockaDtoListOrder>(deliveries);
            }
        }
        private async Task OnDeliverySelected()
        {
            await _navigationService.NavigateAsync("NavigationPage/DeliveryPage?id=" + this.SelectedDelivery.OrderId);
        }
    }
}
