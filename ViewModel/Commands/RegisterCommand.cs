using System;
using System.Windows.Input;

using uwpEvernote.Model;
using uwpEvernote.ViewModel;

namespace uwpEvernote.View.Commands {
    public class RegisterCommand: ICommand {

        public LoginVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginVM vm) {

            VM = vm;

        }
        public bool CanExecute(object parameter) {

            var user = parameter as User;

            if (string.IsNullOrEmpty(user.Name)) {
                return false;
            }
            if (string.IsNullOrEmpty(user.LastName)) {
                return false;
            }
            if (string.IsNullOrEmpty(user.Email)) {
                return false;
            }
            if (string.IsNullOrEmpty(user.Username)) {
                return false;
            }
            if (string.IsNullOrEmpty(user.Password)) {
                return false;
            }
            return true;
        }

        public void Execute(object parameter) {

            VM.Register();
        }
    }
}
