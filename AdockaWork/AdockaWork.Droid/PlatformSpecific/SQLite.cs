using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Adocka.Mobile.Interfaces;
using Xamarin.Forms;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using AdockaWork.Droid.PlatformSpecific;

[assembly: Dependency(typeof(SQLite_Android))]
namespace AdockaWork.Droid.PlatformSpecific
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var sqliteFilename = "AAAAAAAA.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            var platform = new SQLitePlatformAndroid();
            var param = new SQLiteConnectionString(path, false);
            var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param));
            return connection;
        }
    }
}
