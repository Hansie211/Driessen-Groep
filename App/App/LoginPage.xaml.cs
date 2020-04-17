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
            BindingContext = this;
            InitializeComponent();
        }

        private void Button_Clicked( object sender, EventArgs e ) {

        }

        private async void Label_Clicked( object sender, EventArgs e ) {

            await Navigation.PushAsync( new NavigationPage( new RegisterPage() ) );
        }
    }
}