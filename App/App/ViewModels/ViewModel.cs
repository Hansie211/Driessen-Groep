using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewModels {
    public abstract class ViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( string propName ) {

            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propName ) );
        }

        public static NavigationPage CreatePage<T>() where T: ContentPage {

            Type VMType = Type.GetType( $"App.ViewModels.{ typeof(T).Name }ViewModel" );
            ViewModel VM = (ViewModel)Activator.CreateInstance( VMType );

            T Page = (T)Activator.CreateInstance( typeof(T) );
            Page.BindingContext = VM;

            return new NavigationPage( Page );
        }

        public static async Task<NavigationPage> RunModalAsync<T>( ContentPage content ) where T : ContentPage {

            NavigationPage page = CreatePage<T>();

            await content.Navigation.PushModalAsync( page );

            return page;
        }
    }
}
