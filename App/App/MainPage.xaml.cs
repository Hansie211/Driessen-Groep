using App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible( false )]
    public partial class MainPage : ContentPage {

        public MainPage() {

            BindingContext = new MainPageViewModel();
            InitializeComponent();
        }

        private async void Button_Clicked( object sender, EventArgs e ) {

            // Back button
            // Navigation.PushAsync( new NavigationPage( new LoginPage() ) );

            // Geen back button
            await ViewModel.RunModalAsync<LoginPage>( this );
        }

    }


}
