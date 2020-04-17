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

        public string[] Items { get; set; }

        public MainPage() {

            Items = new string[] {
                "A",
                "B",
                "C",
            };

            BindingContext = this;
            InitializeComponent();
        }

        private void Button_Clicked( object sender, EventArgs e ) {

            // Back button
            // Navigation.PushAsync( new NavigationPage( new LoginPage() ) );

            // Geen back button
            Navigation.PushModalAsync( new NavigationPage( new LoginPage() ) );

        }

    }


}
