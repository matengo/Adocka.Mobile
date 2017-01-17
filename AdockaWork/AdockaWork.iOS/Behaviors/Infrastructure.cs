using System;

namespace Adocka.Mobile.iOS.Behaviors
{
    public static class Infrastructure
    {
        private static DateTime initDate;
        public static void Init()
        {
            initDate = DateTime.UtcNow;
        }
    }
}
