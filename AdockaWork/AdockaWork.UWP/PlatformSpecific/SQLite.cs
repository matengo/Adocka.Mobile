using AdockaWork.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using AdockaWork.iOS.PlatformSpecific;

[assembly: Dependency(typeof(SQLiteUWP))]
namespace AdockaWork.iOS.PlatformSpecific
{
    public class SQLiteUWP : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var sqliteFilename = "AAAAAAAAA.db3";
            string documentsPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, sqliteFilename);
            var platform = new SQLitePlatformWinRT();
            var param = new SQLiteConnectionString(path, false);
            var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param));
            return connection;
        }
    }
}