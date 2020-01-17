using System;
using System.Windows.Input;

using uwpEvernote.Model;

namespace uwpEvernote.View.Commands {
    public class LoginCaommand: ICommand {

        public LoginVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCaommand(LoginVM vm) {

            VM = vm;
        }

        public bool CanExecute(object parameter) {

            var user = parameter as User;

            if (string.IsNullOrEmpty(user.Username)) {
                return false;
            }
            if (string.IsNullOrEmpty(user.Password)) {
                return false;
            }
            return true;
        }

        public void Execute(object parameter) {
            VM.Login();
        }
    }
}
