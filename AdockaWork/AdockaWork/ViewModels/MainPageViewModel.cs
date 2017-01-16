using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using AdockaClient;

namespace AdockaWork.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly IAdockaApiService _adocka;
        private readonly INavigationService _navigationService;
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public MainPageViewModel(IAdockaApiService adocka, INavigationService navigationService)
        {
            _adocka = adocka;
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
        public DelegateCommand SaveCommand => new DelegateCommand(Save);
        public async void Save()
        {
            var user = await _adocka.GetApiUserAsync("", "mattias@adocka.com", "tstt1234");
            Title = user.FirstName;
            //Title = "Test";
        }
    }
}
