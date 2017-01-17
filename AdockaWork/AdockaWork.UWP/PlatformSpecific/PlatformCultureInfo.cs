using System.Globalization;
using Adocka.Mobile.Interfaces;
using Adocka.Mobile.UWP.PlatformSpecific;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlatformCultureInfo))]
namespace Adocka.Mobile.UWP.PlatformSpecific
{
    public class PlatformCultureInfo : ICultureInfo
    {
        #region ICultureInfo implementation

        public System.Globalization.CultureInfo CurrentCulture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
            set
            {
                CultureInfo.CurrentCulture = value;
            }
        }

        public System.Globalization.CultureInfo CurrentUICulture
        {
            get
            {
                return CultureInfo.CurrentUICulture;
            }
            set
            {
                CultureInfo.CurrentUICulture = value;
            }
        }

        #endregion
    }
}
