using System;
using System.Windows.Input;

using uwpEvernote.ViewModel;

namespace uwpEvernote {
    public class RegisterCommand: ICommand {

        public LoginVM VMM { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginVM vm) {

            VMM = vm;

        }
        public bool CanExecute(object parameter) {

            return true;
        }

        public void Execute(object parameter) {
            //TODO: implement method
        }
    }
}
