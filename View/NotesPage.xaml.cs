using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections;
using System.Diagnostics;
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
            charactersCount.Text = (value.Length - 1).ToString();
        }

        private async void Actions_Click(object sender, RoutedEventArgs e) {

            var id = sender as Button;

            switch (id.Tag) {

                case "0":
                    using (SpeechRecognizer recognizer = new SpeechRecognizer()) {
                        await recognizer.CompileConstraintsAsync();
                        var result = await recognizer.RecognizeWithUIAsync();
                        var dialog = new MessageDialog(result.Text, "Text spoken");
                        await dialog.ShowAsync();

                        richEbitBox.Document.GetText(TextGetOptions.None, out string value);
                        richEbitBox.Document.SetText(TextSetOptions.None, value += result.Text);
                    }
                    break;
                case "1":
                    if (richEbitBox.Document.Selection.CharacterFormat.Bold == FormatEffect.On) {
                        richEbitBox.Document.Selection.CharacterFormat.Bold = FormatEffect.Off;
                        FormatBoltText.Background = (SolidColorBrush)Resources["disabled"];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Bold = FormatEffect.On;
                        FormatBoltText.Background = (SolidColorBrush)Resources["enabled"];
                    }
                    break;
                case "2":
                    if (richEbitBox.Document.Selection.CharacterFormat.Italic == FormatEffect.On) {
                        richEbitBox.Document.Selection.CharacterFormat.Italic = FormatEffect.Off;
                        formatItalicText.Background = (SolidColorBrush)Resources["disabled"];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Italic = FormatEffect.On;
                        formatItalicText.Background = (SolidColorBrush)Resources["enabled"];
                    }
                    break;
                case "3":
                    if (richEbitBox.Document.Selection.CharacterFormat.Underline == UnderlineType.Single) {
                        richEbitBox.Document.Selection.CharacterFormat.Underline = UnderlineType.None;
                        formatUnderlineText.Background = (SolidColorBrush)Resources["disabled"];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Underline = UnderlineType.Single;
                        formatUnderlineText.Background = (SolidColorBrush)Resources["enabled"];
                    }
                    break;
                default:
                    break;
            }
        }

        private void ComboChanged(object sender, SelectionChangedEventArgs e) {

            var id = sender as ComboBox;

            switch (id.Tag) {

                case "1":
                    //Todo implement new font
                    string fontName = id.SelectedItem.ToString();
                    richEbitBox.Document.Selection.CharacterFormat.Name = fontName;
                    break;
                case "2":
                    var size = (float)id.SelectedItem;
                    break;
                default:
                    break;
            }
        }

        private void Container_Loaded(object sender, RoutedEventArgs e) {
            fontBox.Text = richEbitBox.Document.GetDefaultCharacterFormat().Name;
            fontSizeBox.Text = richEbitBox.Document.GetDefaultCharacterFormat().Size.ToString();
        }
    }
}

