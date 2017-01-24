using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ContactsPageViewModel : INavigationAware
    {
        private readonly IAdockaApiUser _adockaUser;
        private readonly IAdocka _api;
        private readonly IAdockaApiService _adockaApiService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public string SearchStr { get; set; }
        public AdockaDtoSearchModel SearchModel { get; set; }
        public ObservableCollection<AdockaDtoPerson> Contacts { get; set; }
        public AdockaDtoPerson SelectedContact { get; set; }

        public ContactsPageViewModel(IUserService userService, INavigationService navigationService, IAdockaApiService adockaService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _adockaApiService = adockaService;

            _adockaUser = _userService.GetUser();
            _api = _adockaApiService.GetApiClient(_adockaUser.PersonId, _adockaUser.ApiKey);

            this.SearchModel = new AdockaDtoSearchModel
            {
                ItemType = "customer",
                SearchStr = "",
                SearchTags = new List<string>(),
                SearchWeeks = new List<AdockaDtoWeekModel>(),
                SearchCountyCouncilName = null,
                SearchPreferedCountyCouncilName = null,
                SearchKnowledgeOfSystemOptionName = "",
                Skip = 0,
                Take = 30,
                ResponsiblePersonId = null,
                OnlyQualityFullfilled = false,
                ContactFilters = new List<string>(),
                YearSpanFilter = new List<AdockaDtoYearSpanFilter>(),
                AreaOfExpertise = null,
                ReminderDate = null,
                CountryId = null,
                AvailableForWorkInCountries = new List<string>()
            };
        }
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }
        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("itemtype"))
                this.SearchModel.ItemType = (string)parameters["itemtype"];
            else
                this.SearchModel.ItemType = "customer";

            OnSearchStrChanged();
        }
        private async void OnSearchStrChanged()
        {
            if (string.IsNullOrEmpty(this.SearchStr) || this.SearchStr.Length > 2)
            {
                this.SearchModel.SearchStr = this.SearchStr;
                var contacts = await _api.Person.SearchContactsAsync(this.SearchModel);
                this.Contacts = new ObservableCollection<AdockaDtoPerson>(contacts.Result);
            }
        }
        private async void OnSelectedContactChanged()
        {
            await _navigationService.NavigateAsync("ContactPage?id=" + this.SelectedContact.PersonId);
        }
    }
}
