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

            this.BindingContext = this;
            InitializeComponent();
        }

        private async void Button_Clicked( object sender, EventArgs e ) {

            if ( edtPassword.Text != edtPasswordConfirm.Text ) {

                lblError.Text = "ERROR IN PASSWORD!!";
                return;
            }

            var response = await API.CreateUserAsync( edtFirstName.Text, edtLastName.Text, edtEmailAddress.Text, edtPassword.Text );

            if ( !response.IsSuccess ) {

                lblError.Text = "ERROR!";
                return;                
            }

            API.SetAccessToken( response.Content );
            await Navigation.PopAsync();
        }
    }
}