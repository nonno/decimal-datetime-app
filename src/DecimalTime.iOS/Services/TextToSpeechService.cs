using System;
using System.Globalization;
using AVFoundation;
using DecimalTime.Core.Utils;
using UIKit;

namespace DecimalTime.iOS.Services
{
    public class TextToSpeechService : ITextToSpeech
    {
        public void Speak(string text)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(7, 0)) {
                var speechSynthesizer = new AVSpeechSynthesizer();
                var speechUtterance = new AVSpeechUtterance(text) {
                    Rate = AVSpeechUtterance.MaximumSpeechRate / 3,
                    Voice = AVSpeechSynthesisVoice.FromLanguage(CultureInfo.CurrentCulture.Name),
                    Volume = 0.5f,
                    PitchMultiplier = 1.0f
                };

                speechSynthesizer.SpeakUtterance(speechUtterance);
            }
        }
    }
}
