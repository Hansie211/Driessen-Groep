﻿using App.Web;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels {
    class EventDetailsPageViewModel : ViewModel {

        public static async Task<NavigationPage> Create( int eventId, INavigation Navigation ) {

            NavigationPage page = CreatePage<EventDetailsPage>();
            await Navigation.PushModalAsync( page );

            // Load the event
            ( (EventDetailsPageViewModel)( page.RootPage as EventDetailsPage ).BindingContext ).LoadEvent( eventId );

            return page;
        }

        public ICommand SubscribeButtonCommand { get; set; }

        private Event _MainEvent { get; set; }
        public Event MainEvent {
            get => _MainEvent;
            set {

                _MainEvent = value;
                RaisePropertyChanged( "MainEvent" );
            }
        }

        private bool _LoadingEvent = true;
        public bool LoadingEvent {
            get => _LoadingEvent;
            set {
                _LoadingEvent = value;
                RaisePropertyChanged( "LoadingEvent" );
            }
        }

        private string _Error;
        public string Error {
            get => _Error;
            set {

                _Error = $"Error: {value}";

                RaisePropertyChanged( nameof(Error) );
            }
        }

        public string SubscribeButtonText { get; private set; }

        private bool _UserSubscribed;
        public bool UserSubscribed {
            get => _UserSubscribed;
            set {
                _UserSubscribed = value;

                switch( value ) {

                    case true:
                        SubscribeButtonText = "Afmelden";
                        break;
                    case false:
                        SubscribeButtonText = "Aanmelden";
                        break;
                }

                RaisePropertyChanged( nameof( SubscribeButtonText ) );
                RaisePropertyChanged( nameof( UserSubscribed ) );
            }
        }


        private async void LoadEvent( int eventId ) {

            LoadingEvent = true;

            var response = await API.GetEventAsync( eventId );
            if ( !response.IsSuccess ) {

                // ERROR
                Error = response.ErrorMessage["message"];

                return;
            }

            MainEvent = response.Content;
            LoadingEvent = false;
        }

        public EventDetailsPageViewModel() {

            SubscribeButtonCommand = new RelayCommand( OnSubscribeButton );
        }

        private void OnSubscribeButton( object _ ) {

            UserSubscribed = !UserSubscribed;

            //if ( SubscribeButtonText  == "Aanmelden" ) {

            //    SubscribeButtonText = "Afmelden";
            //} else {

            //    SubscribeButtonText = "Aanmelden";
            //}

            
        }
    }
}
