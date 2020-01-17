using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace uwpEvernote.View {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage: Page {
        public LoginPage() {
            this.InitializeComponent();
        }

        private void ActivateStackPanels_Click(object sender, RoutedEventArgs e) {

            var tag = sender as Button;

            switch (tag.Tag) {
                case "1":
                    RegisterPanel.Visibility = Visibility.Collapsed;
                    LoginPanel.Visibility = Visibility.Visible;
                    break;

                case "2":
                    RegisterPanel.Visibility = Visibility.Visible;
                    LoginPanel.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }
    }
}
