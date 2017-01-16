using AdockaClient;
using AdockaWork.Interfaces;
using AdockaWork.Repo;
using Prism.Unity;
using AdockaWork.Views;
using Microsoft.Practices.Unity;
using AdockaWork.Services;

namespace AdockaWork
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
            Container.RegisterTypeForNavigation<MainMasterDetailPage>();
            Container.RegisterTypeForNavigation<MyNavigationPage>();
            Container.RegisterTypeForNavigation<MyWorkItemsPage>();
            Container.RegisterTypeForNavigation<MyProfilePage>();
            Container.RegisterTypeForNavigation<MyAvailabilityPage>();
        }
    }
}
