using uwpEvernote.Model;

namespace uwpEvernote.ViewModel {
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
    }
}
