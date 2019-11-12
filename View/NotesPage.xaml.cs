using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.WebUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace uwpEvernote.View {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesPage: Page {

        private const string ON = "enabled";
        private const string OFF = "disabled";
        private SpeechRecognizer speechRecognizer;
        private CoreDispatcher dispatcher;

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

            richEbitBox.Document.Selection.SetRange(0, richEbitBox.Document.Selection.EndPosition);

            var id = sender as Button;

            richEbitBox.Focus(FocusState.Pointer);

            switch (id.Tag) {

                case "0":
                    dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                    speechRecognizer = new SpeechRecognizer();
                    await speechRecognizer.CompileConstraintsAsync();
                    speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
                    speechRecognizer.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromDays(1);
                    speechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
                    await speechRecognizer.ContinuousRecognitionSession.StartAsync();
                    textToSpeech.Background = (SolidColorBrush)Resources[ON];
                    break;
                case "1":
                    if (richEbitBox.Document.Selection.CharacterFormat.Bold == FormatEffect.On) {
                        richEbitBox.Document.Selection.CharacterFormat.Bold = FormatEffect.Off;
                        FormatBoltText.Background = (SolidColorBrush)Resources[OFF];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Bold = FormatEffect.On;
                        FormatBoltText.Background = (SolidColorBrush)Resources[ON];
                    }
                    break;
                case "2":
                    if (richEbitBox.Document.Selection.CharacterFormat.Italic == FormatEffect.On) {
                        richEbitBox.Document.Selection.CharacterFormat.Italic = FormatEffect.Off;
                        formatItalicText.Background = (SolidColorBrush)Resources[OFF];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Italic = FormatEffect.On;
                        formatItalicText.Background = (SolidColorBrush)Resources[ON];
                    }
                    break;
                case "3":
                    if (richEbitBox.Document.Selection.CharacterFormat.Underline == UnderlineType.Single) {
                        richEbitBox.Document.Selection.CharacterFormat.Underline = UnderlineType.None;
                        formatUnderlineText.Background = (SolidColorBrush)Resources[OFF];
                    } else {
                        richEbitBox.Document.Selection.CharacterFormat.Underline = UnderlineType.Single;
                        formatUnderlineText.Background = (SolidColorBrush)Resources[ON];
                    }
                    break;
                case "4":
                    if (Ink_cnvas.Visibility == Visibility.Collapsed) {
                        formatDraw.Background = (SolidColorBrush)Resources[ON];
                        Ink_cnvas.Visibility = Visibility.Visible;
                        richEbitBox.Visibility = Visibility.Collapsed;
                    } else if (Ink_cnvas.Visibility == Visibility.Visible) {
                        Ink_cnvas.Visibility = Visibility.Collapsed;
                        formatDraw.Background = (SolidColorBrush)Resources[OFF];
                        richEbitBox.Visibility = Visibility.Visible;
                    }
                    break;
                case "5":
                    richEbitBox.Document.GetText(TextGetOptions.AdjustCrlf, out string value);
                    speak(value);
                    break;
                default:
                    break;
            }
        }

        private async void speak(string value) {

            MediaElement mediaElement = new MediaElement();

            // The object for controlling the speech synthesis engine (voice).
            var synth = new SpeechSynthesizer();

            VoiceInformation voiceInfo = (from voice in SpeechSynthesizer.AllVoices
                                          where voice.Gender == VoiceGender.Female
                                          select voice).FirstOrDefault();

            synth.Voice = voiceInfo;

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(value);

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args) {

            await speechRecognizer.ContinuousRecognitionSession.StartAsync();
        }

        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args) {

            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {

                richEbitBox.Document.GetText(TextGetOptions.AdjustCrlf, out string value);
                richEbitBox.Document.SetText(TextSetOptions.None, value + args.Result.Text);
                textToSpeech.Background = (SolidColorBrush)Resources[OFF]; // This will indicate that my button is off
                await speechRecognizer.ContinuousRecognitionSession.StopAsync();
            });
        }

        private void ComboChanged(object sender, SelectionChangedEventArgs e) {

            richEbitBox.Focus(FocusState.Pointer);

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

