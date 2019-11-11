using System;
using System.Windows.Input;

using uwpEvernote.ViewModel;

using Windows.ApplicationModel.Core;

namespace uwpEvernote {
    public class ExitCommand: ICommand {

        public NotesVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public ExitCommand(NotesVM vm) {

            VM = vm;
        }
        public bool CanExecute(object parameter) {

            return true;
        }

        public void Execute(object parameter) {

            CoreApplication.Exit();
        }
    }
}
