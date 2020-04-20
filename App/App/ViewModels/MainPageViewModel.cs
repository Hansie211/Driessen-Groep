using App.Web;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace App.ViewModels {
    public class MainPageViewModel : ViewModel {

        public ObservableCollection<Event> Events { get; set; }

        public ICommand LoginUserCommand { get; set; }
        public ICommand ClickEventCommand { get; set; }
        public ICommand LoadEventsCommand { get; set; }
        public ICommand LoadMoreCommand { get; set; }

        private string _Error;
        public string Error {
            get => _Error;
            set {

                _Error = value;
                if ( value != null ) {
                    _Error = $"Error: {_Error}";
                }

                RaisePropertyChanged( "Error" );
            }
        }

        private bool _LoadingEvents;
        public bool LoadingEvents {
            get => _LoadingEvents;
            set {
                _LoadingEvents = value;

                RaisePropertyChanged( "LoadingEvents" );
            }
        }

        private bool BottomReached = false;

        public MainPageViewModel() {

            Events = new ObservableCollection<Event>();

            LoginUserCommand    = new RelayCommand( OnLoginUser );
            ClickEventCommand   = new RelayCommand( OnClickEvent );
            LoadEventsCommand   = new RelayCommand( OnLoadEvents );
            LoadMoreCommand     = new RelayCommand( OnLoadMore );

            OnLoadEvents( null );
        }

        private async void OnClickEvent( object @event ) {

            await EventDetailsPageViewModel.Create( ( @event as Event ).ID, Navigation );
        }

        private async void OnLoadMore( object _ ) {

            if ( BottomReached ) {
                return;
            }

            LoadingEvents = true;
            try {

                var response = await API.GetEventsAsync( Events.Count );
                if ( !response.IsSuccess ) {

                    // ERROR
                    Error = response.ErrorMessage[ "message" ];
                    return;
                }

                if ( response.Content.Count() == 0 ) {
                    BottomReached = true;
                    return;
                }

                foreach ( Event @event in response.Content ) {

                    Event listevent = Events.FirstOrDefault( o => o.ID == @event.ID );

                    if ( listevent != null ) {

                        // listevent.CopyFromRequest( @event );
                        continue;
                    }

                    Events.Add( @event );
                }

                RaisePropertyChanged( "Events" );
            } finally {

                LoadingEvents = false;
            }

        }

        private async void OnLoadEvents( object _ ) {

            LoadingEvents = true;
            try {

                var response = await API.GetEventsAsync(0);

                if ( !response.IsSuccess ) {

                    // ERROR
                    Error = response.ErrorMessage[ "message" ];
                    return;
                }

                foreach ( Event @event in response.Content ) {

                    Event listevent = Events.FirstOrDefault( o => o.ID == @event.ID );

                    if ( listevent != null ) {

                        // listevent.CopyFromRequest( @event );
                        continue;
                    }

                    Events.Add( @event );
                }

                RaisePropertyChanged( "Events" );
            } finally {

                LoadingEvents = false;
            }
        }

        public async void OnLoginUser( object _ ) {

            // Back button
            // Navigation.PushAsync( new NavigationPage( new LoginPage() ) );

            // Geen back button
            // Navigation.PushModalAsync


            await RunModalAsync<LoginPage>();
        }
    }
}
