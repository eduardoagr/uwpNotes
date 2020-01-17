using uwpEvernote.Model;
using uwpEvernote.View.Commands;
using uwpEvernote.ViewModel;

namespace uwpEvernote {
    public class LoginVM {

        private User _User;

        public User User {
            get { return _User; }
            set { _User = value; }
        }

        public LoginCaommand LoginCaommand { get; set; }
        public RegisterCommand RegisterCommand { get; set; }

        public LoginVM() {

            LoginCaommand = new LoginCaommand(this);
            RegisterCommand = new RegisterCommand(this);
        }

        public void Login() {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile)) {

                conn.CreateTable<User>();

                var user = conn.Table<User>().Where(u => u.Username == User.Username).FirstOrDefault();

                if (user.Password == User.Password) {

                    // TODO: IMPLEMENT LOGIN FUNTIONALITY
                }
            }
        }
        public void Register() {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile)) {

                conn.CreateTable<User>();

                var result = DatabaseHelper.Insert(User);

                if (result) {

                    //TODO: Implement registration
                }
            }
        }
    }
}
