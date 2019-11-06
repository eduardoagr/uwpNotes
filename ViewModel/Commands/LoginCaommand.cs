using System;
using System.Windows.Input;

using uwpEvernote.ViewModel;

namespace uwpEvernote.View.Commands {
    public class LoginCaommand: ICommand {

        public LoginVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCaommand(LoginVM vm) {

            VM = vm;
        }

        public bool CanExecute(object parameter) {

            return true;
        }

        public void Execute(object parameter) {
            // TODO: Implement 
        }
    }
}
