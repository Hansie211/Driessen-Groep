using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace App.ViewModels {
    public class RelayCommand : ICommand {

        public delegate void DelExecute( object obj );
        public delegate bool DelCanExecute( object obj );

        private DelExecute FuncExecute;
        private DelCanExecute FuncCanExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand( DelExecute execute, DelCanExecute canExecute = null ) {

            if ( execute == null ) {
                throw new ArgumentNullException( "execute" );
            }

            FuncExecute = execute;
            FuncCanExecute = canExecute;
        }

        public bool CanExecute( object parameter ) {
            return this.FuncCanExecute == null || this.FuncCanExecute( parameter );
        }

        public void Execute( object parameter ) {
            this.FuncExecute( parameter );
        }

        public void Destroy() {
            this.FuncExecute    = _ => { };
            this.FuncCanExecute = _ => { return false; };
        }
    }
}
