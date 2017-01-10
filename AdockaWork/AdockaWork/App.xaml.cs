using AdockaClient;
using Prism.Unity;
using AdockaWork.Views;
using Microsoft.Practices.Unity;

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
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterType<IStandardAuth, StandardAuth>();
            Container.RegisterTypeForNavigation<LoginPage>();
        }
    }
}
