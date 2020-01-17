using System;
using System.Windows.Input;
using uwpEvernote.Model;

namespace uwpEvernote.ViewModel.Commands {
    public class NewNoteCommand: ICommand {

        public NotesVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNoteCommand(NotesVM vm) {

            VM = vm;

        }
        public bool CanExecute(object parameter) {

            var selectedNoteBook = parameter as NoteBook;
            if (selectedNoteBook != null) {
                return true;
            }
            return false;
        }

        public void Execute(object parameter) {

            NoteBook selectedNoteBook = parameter as NoteBook;
            VM.CreateNote(selectedNoteBook.Id);
        }
    }
}
