using SQLite.Net.Async;

namespace Adocka.Mobile.Interfaces
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetConnection();
    }
}
