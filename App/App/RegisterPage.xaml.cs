using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App.Web;
using System.ComponentModel;

namespace App {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class RegisterPage : ContentPage {

        public RegisterPage() {

            InitializeComponent();
        }

        private async void BtnRegister_Clicked( object sender, EventArgs e ) {

            if ( !Validation.EmailAddress( edtEmailAddress.Text ) ) {

                lblError.Text = "Emailadres is incorrect.";
                return;
            }

            if ( string.IsNullOrEmpty( edtFirstName.Text ) ) {

                lblError.Text = "Voornaam kan niet leeg zijn.";
                return;
            }

            if ( string.IsNullOrEmpty( edtLastName.Text ) ) {

                lblError.Text = "Achternaam kan niet leeg zijn.";
                return;
            }

            if ( string.IsNullOrEmpty( edtPassword.Text ) ) {

                lblError.Text = "Wachtwoord kan niet leeg zijn.";
                return;
            }

            if ( edtPassword.Text != edtPasswordConfirm.Text ) {

                lblError.Text = "Wachtwoorden komen niet overeen.";

                edtPasswordConfirm.Text = "";
                edtPasswordConfirm.Focus();

                return;
            }

            var response = await API.CreateUserAsync( edtFirstName.Text, edtLastName.Text, edtEmailAddress.Text, edtPassword.Text );

            if ( !response.IsSuccess ) {

                lblError.Text = response.ErrorMessage["message"];
                return;                
            }

            API.SetAccessToken( response.Content );
            await Navigation.PopToRootAsync();
        }
    }
}