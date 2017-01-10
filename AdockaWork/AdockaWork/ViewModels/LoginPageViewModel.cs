using AdockaClient;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace AdockaWork.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly IStandardAuth _standardAuth;

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public LoginPageViewModel(IStandardAuth auth)
        {
            _standardAuth = auth;
        }
        public DelegateCommand LoginCommand => new DelegateCommand(Login);
        public async void Login()
        {
            var user = await _standardAuth.LoginUser("", Email, Password);

            //Fortsätt
            //Application.Current.Properties["PersonId"] = user.FirstName;
            
            Message = user.FirstName;
        }
    }
}
