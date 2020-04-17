using App.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace App.ViewModels {
    class RegisterPageViewModel : ViewModel {

        private string _FirstName;
        public string FirstName {
            get => _FirstName;
            set {

                _FirstName = value;
                RaisePropertyChanged( "FirstName" );
            }
        }

        private string _LastName;
        public string LastName {
            get => _LastName;
            set {

                _LastName = value;
                RaisePropertyChanged( "LastName" );
            }
        }

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

        private string _PasswordConfirm;
        public string PasswordConfirm {
            get => _PasswordConfirm;
            set {

                _PasswordConfirm = value;
                RaisePropertyChanged( "PasswordConfirm" );
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

        public ICommand RegisterCommand { get; set; }

        public RegisterPageViewModel() {

            RegisterCommand = new RelayCommand( OnRegister );
        }

        public async void OnRegister( object _ ) {

            if ( !Validation.EmailAddress( EmailAddress ) ) {

                Error = "Emailadres is incorrect.";
                return;
            }

            if ( string.IsNullOrEmpty( FirstName ) ) {

                Error = "Voornaam kan niet leeg zijn.";
                return;
            }

            if ( string.IsNullOrEmpty( LastName) ) {

                Error = "Achternaam kan niet leeg zijn.";
                return;
            }

            if ( string.IsNullOrEmpty( Password ) ) {

                Error = "Wachtwoord kan niet leeg zijn.";
                return;
            }

            if ( Password != PasswordConfirm ) {

                Error = "Wachtwoorden komen niet overeen.";

                PasswordConfirm = "";
                // edtPasswordConfirm.Focus();

                return;
            }

            var response = await API.CreateUserAsync( FirstName, LastName, EmailAddress, Password );

            if ( !response.IsSuccess ) {

                Error = response.ErrorMessage[ "message" ];
                return;
            }

            API.SetAccessToken( response.Content );
            await Navigation.PopModalAsync();

        }
    }
}
