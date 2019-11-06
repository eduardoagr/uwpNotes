using SQLite;

using uwpEvernote.Interfaces;

namespace uwpEvernote.Model {
    public class NoteBook: Notify {

        private int _id;

        [PrimaryKey, AutoIncrement]
        public int Id {
            get { return _id; }
            set {
                if (value != _id) {
                    _id = value;
                    OnPropertyChanged("id");
                }
            }
        }


        private int _userId;

        [Indexed]
        public int UserId {
            get { return _userId; }
            set {
                if (value != _userId) {
                    _userId = value;
                    OnPropertyChanged("userId");
                }
            }
        }


        private string _name;
        public string Name {
            get { return _name; }
            set {
                if (value != _name) {
                    _name = value;
                    OnPropertyChanged("name");
                }
            }
        }
    }
}
