using uwpEvernote.Model;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace uwpEvernote.View.UserControls {
    public sealed partial class NoteControl: UserControl {

        public Note Note {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NoteControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e) {

            NoteControl note = d as NoteControl;

            if (note != null) {

                note.NoteTitle.Text = (e.NewValue as Note).Title;
                note.EditedTextBlock.Text = (e.NewValue as Note).UpdatedTime.ToShortTimeString();
                note.ContentTextbox.Text = (e.NewValue as Note).Title;
            }
        }

        public NoteControl() {
            this.InitializeComponent();
        }
    }
}
