using Microsoft.Graphics.Canvas.Text;

using SQLite;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using uwpEvernote.Model;
using uwpEvernote.ViewModel;
using uwpEvernote.ViewModel.Commands;

namespace uwpEvernote {
    public class NotesVM: INotifyPropertyChanged {

        public ObservableCollection<NoteBook> NoteBooks { get; set; }

        private NoteBook _SelectedNotebook;

        public NoteBook SelectedNotebook {
            get { return _SelectedNotebook; }
            set {
                if (value != _SelectedNotebook) {
                    _SelectedNotebook = value;
                    ReadNoote();
                }
            }
        }
        public int[] FontsSize { get; set; }
        public string[] Fonts { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public ExitCommand ExitCommand { get; set; }


        public NotesVM() {

            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            ExitCommand = new ExitCommand(this);
            NoteBooks = new ObservableCollection<NoteBook>();
            Notes = new ObservableCollection<Note>();
            Fonts = CanvasTextFormat.GetSystemFontFamilies();
            FontsSize = new int[] { 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 31, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
            ReadNotebooks();
            ReadNoote();
        }

        public void CreateNotebook() {

            var newNotebook = new NoteBook() {
                Name = "New Book"
            };

            DatabaseHelper.Insert(newNotebook);

            ReadNotebooks();
        }

        public void CreateNote(int id) {

            var newNote = new Note() {
                NotebookId = id,
                CratedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            DatabaseHelper.Insert(newNote);

            ReadNoote();
        }

        public void ReadNotebooks() {

            using (var conn = new SQLiteConnection(DatabaseHelper.dbFile)) {

                var notebooks = conn.Table<NoteBook>().ToList();

                NoteBooks.Clear();
                foreach (var item in notebooks) {
                    NoteBooks.Add(item);
                }
            }
        }

        public void ReadNoote() {

            using (var conn = new SQLiteConnection(DatabaseHelper.dbFile)) {

                if (SelectedNotebook != null) {
                    var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                    Notes.Clear();
                    foreach (var item in notes) {
                        Notes.Add(item);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
