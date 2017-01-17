using Adocka.Mobile.iOS.PlatformSpecific;
using Adocka.Mobile.Interfaces;
using Xamarin.Forms;
using System.Threading;

[assembly: Dependency(typeof(PlatformCultureInfo))]
namespace Adocka.Mobile.iOS.PlatformSpecific
{
    public class PlatformCultureInfo : ICultureInfo
    {
        #region ICultureInfo implementation

        public System.Globalization.CultureInfo CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture;
            }
            set
            {
                Thread.CurrentThread.CurrentCulture = value;
            }
        }

        public System.Globalization.CultureInfo CurrentUICulture
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                Thread.CurrentThread.CurrentUICulture = value;
            }
        }

        #endregion
    }
}
