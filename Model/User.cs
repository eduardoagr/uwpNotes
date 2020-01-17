using SQLite;

using uwpEvernote.Interfaces;

namespace uwpEvernote.Model {
    public class User: Notify {

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

        private string _name;

        [MaxLength(50)]
        public string Name {
            get { return _name; }
            set {
                if (value != _name) {
                    _name = value;
                    OnPropertyChanged("name");
                }
            }
        }


        private string _lastName;

        [MaxLength(50)]
        public string LastName {
            get { return _lastName; }
            set {
                if (value != _lastName) {
                    _lastName = value;
                    OnPropertyChanged("lastName");
                }
            }
        }

        private string _username;
        public string Username {
            get { return _username; }
            set {
                if (value != _username) {
                    _username = value;
                    OnPropertyChanged("username");
                }
            }
        }



        private string _email;
        public string Email {
            get { return _email; }
            set {
                if (value != _email) {
                    _email = value;
                    OnPropertyChanged("email");
                }
            }
        }


        private string _password;
        public string Password {
            get { return _password; }
            set {
                if (value != _password) {
                    _password = value;
                    OnPropertyChanged("password");
                }
            }
        }

    }
}
