using App.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace App.ViewModels {
    class LoginPageViewModel : ViewModel {

        private string _EmailAddress;
        public string EmailAddress {
            get => _EmailAddress;
            set {

                _EmailAddress = value;
                RaisePropertyChanged( "EmailAddress" );
            }
        }

        private string _Password;
        public string Password {
            get => _Password;
            set {

                _Password = value;
                RaisePropertyChanged( "Password" );
            }
        }

        private string _Error;
        public string Error {
            get => _Error;
            set {

                _Error = value;
                RaisePropertyChanged( "Error" );
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginPageViewModel() {

            LoginCommand = new RelayCommand( OnLogin );
            RegisterCommand = new RelayCommand( OnRegister );
        }

        private async void OnLogin( object _ ) {

            if ( !Validation.EmailAddress( EmailAddress ) ) {

                Error = "Email adres is incorrect.";
                return;
            }

            if ( string.IsNullOrEmpty( Password ) ) {

                Error = "Wachtwoord kan niet leeg zijn.";
                return;
            }

            var response = await API.LoginUserAsync( EmailAddress, Password );
            if ( !response.IsSuccess ) {

                Error = response.ErrorMessage[ "message" ];

                Password = "";
                // edtPassword.Focus();

                return;
            }

            await Navigation.PopModalAsync();
        }

        private async void OnRegister( object _ ) {

            await RunModalAsync<RegisterPage>();
        }
    }
}
