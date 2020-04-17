using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace App.ViewModels {
    public class MainPageViewModel : ViewModel {

        public ObservableCollection<Event> Events { get; set; }

        public ICommand AddEventCommand { get; set; }
        public ICommand LoginUserCommand { get; set; }

        public MainPageViewModel() {

            Events = new ObservableCollection<Event>();

            AddEventCommand     = new RelayCommand( OnAddEvent );
            LoginUserCommand    = new RelayCommand( OnLoginUser );

            Events.Add( new Event() {
                Title = "Het superevent",
                Description = "Een beschrijving",
                Location = "Op het veld naast de boom",
                Date = DateTime.Now.AddMinutes( -165498721 ),
                Ownerships = null,
                Programs = null,
                Reviews = null,
                ID = 0,
                Speakers = null,
            } );
        }

        public async void OnLoginUser( object _ ) {

            // Back button
            // Navigation.PushAsync( new NavigationPage( new LoginPage() ) );

            // Geen back button
            // Navigation.PushModalAsync


            await RunModalAsync<LoginPage>();
        }

        public void OnAddEvent( object _ ) {

            //Events.Add( new Event() {
            //    Title = "Het superevent 2x",
            //    Description = "Een beschrijving 2x",
            //    Location = "Op het veld naast de boom 2x",
            //    Date = DateTime.Now.AddMinutes( -149875421 ),
            //    Ownerships = null,
            //    Programs = null,
            //    Reviews = null,
            //    ID = 0,
            //    Speakers = null,
            //} );

            //RaisePropertyChanged("Events");
        }
    }
}
