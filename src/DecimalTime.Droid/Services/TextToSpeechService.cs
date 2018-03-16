using System;
using Android.Speech.Tts;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Droid.Services
{
    public class TextToSpeechService : Java.Lang.Object, ITextToSpeechService, TextToSpeech.IOnInitListener
    {
        TextToSpeech speaker;
        string toSpeak;

        public void Speak(string text)
        {
            toSpeak = text;
            if (speaker == null) {
                var context = Xamarin.Forms.Forms.Context;
                speaker = new TextToSpeech(context, this);
            } else {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
        }

        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success)) {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
        }
    }
}
