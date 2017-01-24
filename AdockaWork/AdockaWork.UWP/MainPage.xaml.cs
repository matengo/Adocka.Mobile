using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Adocka.Mobile.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Maps
            Xamarin.FormsMaps.Init("INSERT_AUTHENTICATION_TOKEN_HERE");

            LoadApplication(new Mobile.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }

}
