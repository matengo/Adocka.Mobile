using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdockaClientPCL.Models;
using AdockaClient;
using Newtonsoft.Json;
using Plugin.Settings;

namespace AdockaWork.Services
{
    public interface IUserService
    {
        IAdockaApiUser GetUser();
        Task<IAdockaApiUser> LoginUser(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IAdockaApiService _adocka;
        public UserService(IAdockaApiService adocka)
        {
            _adocka = adocka;
        }
        public IAdockaApiUser GetUser()
        {
            if (CrossSettings.Current.Contains("User"))
                return JsonConvert.DeserializeObject<AdockaApiUser>(CrossSettings.Current.GetValueOrDefault("User", default(string)));
            else
                return null;
        }
        public async Task<IAdockaApiUser> LoginUser(string username, string password)
        {
            var user = await _adocka.GetApiUserAsync("", username, password);

            var serializeduser = JsonConvert.SerializeObject(user);
            CrossSettings.Current.AddOrUpdateValue("User", serializeduser);

            return user;
        }
    }
}
