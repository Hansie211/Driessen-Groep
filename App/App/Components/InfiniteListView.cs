using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Components {
    public class InfiniteListView : ListView {

        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create( nameof(LoadMoreCommand), typeof(ICommand), typeof(InfiniteListView), default(ICommand) );

        public ICommand LoadMoreCommand {
            get { return (ICommand)GetValue( LoadMoreCommandProperty ); }
            set { SetValue( LoadMoreCommandProperty, value ); }
        }

        public InfiniteListView() {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        void InfiniteListView_ItemAppearing( object sender, ItemVisibilityEventArgs e ) {

            if ( !( ItemsSource is IList items ) ) {
                return;
            }

            if ( e.Item != items[ items.Count - 1 ] ) {
                return;
            }

            if ( LoadMoreCommand == null ) {
                return;
            }

            if ( !LoadMoreCommand.CanExecute( null ) ) {
                return;
            }

            LoadMoreCommand.Execute( null );
        }
    }
}
