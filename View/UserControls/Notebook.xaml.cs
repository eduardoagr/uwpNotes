using uwpEvernote.Model;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace uwpEvernote.View.UserControls {

    public sealed partial class Notebook: UserControl {

        public NoteBook DisplayNotebook {
            get { return (NoteBook)GetValue(DisplayNotebookProperty); }
            set { SetValue(DisplayNotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNotebookProperty =
            DependencyProperty.Register("DisplayNotebook", typeof(NoteBook), typeof(Notebook), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e) {

            Notebook notebook = d as Notebook;

            if (notebook != null) {

                notebook.NoteBookTextBlock.Text = (e.NewValue as NoteBook).Name;
            }
        }

        public Notebook() {
            this.InitializeComponent();
        }
    }
}
