using Microsoft.Graphics.Canvas.Text;

using System;
using System.Collections;

using Windows.Media.SpeechRecognition;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace uwpEvernote.View {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesPage: Page {

        const string on = "enabled";
        const string off = "disabled";

        public NotesPage() {
            this.InitializeComponent();
            var fonts = CanvasTextFormat.GetSystemFontFamilies();
            fontBox.ItemsSource = fonts;
            var arrList = new ArrayList();
            for (int i = 0; i < 73; ++i) {
                arrList.Add(i);
            }
            fontSizeBox.ItemsSource = arrList;
        }

        private void Cotent_TextChanged(object sender, RoutedEventArgs e) {

            richEbitBox.Document.GetText(TextGetOptions.None, out string value);
            charactersCount.Text = $"Characters: {value.Length - 1}";
        }

        private async void Actions_Click(object sender, RoutedEventArgs e) {

            var id = sender as Button;

            richEbitBox.Focus(FocusState.Pointer);

            switch (id.Tag) {

                case "0":
                    using (SpeechRecognizer recognizer = new SpeechRecognizer()) {
                        await recognizer.CompileConstraintsAsync();
                        recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromHours(1);
                        recognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromHours(1);

                        recognizer.UIOptions.AudiblePrompt = "Say whatever you want";
                        recognizer.UIOptions.ExampleText = "hello world";
                        recognizer.UIOptions.ShowConfirmation = true;

                        var result = await recognizer.RecognizeWithUIAsync();
                        var dialog = new MessageDialog(result.Text, "Text");

                        richEbitBox.Document.GetText(TextGetOptions.AdjustCrlf, out string value);
                        richEbitBox.Document.SetText(TextSetOptions.None, value += result.Text);
                    }
                    break;
                case "1":
                    if (richEbitBox.Document.Selection.CharacterFormat.Bold == FormatEffect.On) {
                        richEbitBox.Document.Selection.CharacterFormat.Bold = FormatEffect.Off;
                        FormatBoltText.Background = (SolidColorBrush)Resources[off];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Bold = FormatEffect.On;
                        FormatBoltText.Background = (SolidColorBrush)Resources[on];
                    }
                    break;
                case "2":
                    if (richEbitBox.Document.Selection.CharacterFormat.Italic == FormatEffect.On) {
                        richEbitBox.Document.Selection.CharacterFormat.Italic = FormatEffect.Off;
                        formatItalicText.Background = (SolidColorBrush)Resources[off];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Italic = FormatEffect.On;
                        formatItalicText.Background = (SolidColorBrush)Resources[on];
                    }
                    break;
                case "3":
                    if (richEbitBox.Document.Selection.CharacterFormat.Underline == UnderlineType.Single) {
                        richEbitBox.Document.Selection.CharacterFormat.Underline = UnderlineType.None;
                        formatUnderlineText.Background = (SolidColorBrush)Resources[off];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Underline = UnderlineType.Single;
                        formatUnderlineText.Background = (SolidColorBrush)Resources[on];
                    }
                    break;
                case "4":
                    if (Ink_cnvas.Visibility == Visibility.Collapsed) {
                        formatDraw.Background = (SolidColorBrush)Resources[on];
                        Ink_cnvas.Visibility = Visibility.Visible;
                        richEbitBox.Visibility = Visibility.Collapsed;
                    } else if (Ink_cnvas.Visibility == Visibility.Visible) {
                        Ink_cnvas.Visibility = Visibility.Collapsed;
                        formatDraw.Background = (SolidColorBrush)Resources[off];
                        richEbitBox.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }
        }

        private void ComboChanged(object sender, SelectionChangedEventArgs e) {

            richEbitBox.Focus(FocusState.Keyboard);
            var id = sender as ComboBox;
            switch (id.Tag) {

                case "1":
                    //Todo implement new font name
                    string fontName = id.SelectedItem.ToString();
                    richEbitBox.Document.Selection.CharacterFormat.Name = fontName;
                    break;
                case "2":
                    var size = id.SelectedItem.ToString();
                    //set size to the Selection
                    richEbitBox.Document.Selection.CharacterFormat.Size = Convert.ToInt32(size);
                    break;
                default:
                    break;
            }
        }

        private void fontBox_Loaded(object sender, RoutedEventArgs e) {
            fontBox.Text = richEbitBox.FontFamily.Source.ToString();
            fontSizeBox.Text = richEbitBox.FontSize.ToString();
        }

        private void fontBox_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args) {
            richEbitBox.Focus(FocusState.Pointer);
        }
    }
}

