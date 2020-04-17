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
    }
}