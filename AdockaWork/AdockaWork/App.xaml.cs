using Adocka.Mobile.Repo;
using Adocka.Mobile.Services;
using Adocka.Mobile.ViewModels.Delivery;
using Adocka.Mobile.Views;
using Adocka.Mobile.Views.Delivery;
using AdockaClient;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Adocka.Mobile
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("LoginPage");

            //NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
        }
        protected override void RegisterTypes()
        {
            Container.RegisterType<IAdockaApiService, AdockaApiService>();
            Container.RegisterType<IUserService, UserService>();
            Container.RegisterType(typeof(ILocalRepository<>), typeof(LocalRepository<>));


            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MasterDetailPage>();
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MyWorkItemsPage>();
            Container.RegisterTypeForNavigation<MyProfilePage>();
            Container.RegisterTypeForNavigation<CustomersPage>();
            Container.RegisterTypeForNavigation<OrdersPage>();

            //Deliveries
            Container.RegisterTypeForNavigation<DeliveryDatesPage, DeliveryDatesPageViewModel>();
            Container.RegisterTypeForNavigation<DeliveryTagsPage, DeliveryTagsPageViewModel>();
            Container.RegisterTypeForNavigation<DeliveriesPage, DeliveriesPageViewModel>();
            Container.RegisterTypeForNavigation<DeliveryPage, DeliveriesPageViewModel>();
        }
    }
}
