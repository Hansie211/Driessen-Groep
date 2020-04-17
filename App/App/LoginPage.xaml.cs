using App.ViewModels;
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

        private void Button_Clicked( object sender, EventArgs e ) {

        }

        private async void Label_Clicked( object sender, EventArgs e ) {

            await ViewModel.RunModalAsync<RegisterPage>( this );
        }
    }
}