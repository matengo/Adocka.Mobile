using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System.Threading;
using Adocka.Mobile.Interfaces;

[assembly: Dependency(typeof(Adocka.Mobile.Droid.PlatformSpecific.PlatformCultureInfo))]
namespace Adocka.Mobile.Droid.PlatformSpecific
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