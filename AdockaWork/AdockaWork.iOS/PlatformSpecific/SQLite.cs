using System;
using System.IO;
using Adocka.Mobile.iOS.PlatformSpecific;
using Adocka.Mobile.Interfaces;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteiOS))]
namespace Adocka.Mobile.iOS.PlatformSpecific
{
    public class SQLiteiOS : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var sqliteFilename = "AAAAAAAAA.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            var platform = new SQLitePlatformIOS();
            var param = new SQLiteConnectionString(path, false);
            var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param));
            return connection;
        }
    }
}