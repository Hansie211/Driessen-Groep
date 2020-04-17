using App.ViewModels;
using App.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App {
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class LoginPage : ContentPage {

        public LoginPage() {
            InitializeComponent();
        }

        private async void ButtonLogin_Clicked( object sender, EventArgs e ) {

            if ( !Validation.EmailAddress( edtEmailAddress.Text ) ) {

                lblError.Text = "Email adres is incorrect.";
                return;
            }

            if ( string.IsNullOrEmpty( edtPassword.Text ) ) {

                lblError.Text = "Wachtwoord kan niet leeg zijn.";
                return;
            }

            var response = await API.LoginUserAsync( edtEmailAddress.Text, edtPassword.Text );
            if ( !response.IsSuccess ) {

                lblError.Text = response.ErrorMessage["message"];
                return;
            }

            await Navigation.PopModalAsync();
        }

        private async void LabelRegister_Clicked( object sender, EventArgs e ) {

            await ViewModel.RunModalAsync<RegisterPage>( this );
        }
    }
}