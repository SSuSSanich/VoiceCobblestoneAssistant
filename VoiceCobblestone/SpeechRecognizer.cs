using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using NAudio.Wave;
using Vosk;

namespace VoiceCobblestone;

public class SpeechRecognizer : SoundsHandler
{
    private VoskRecognizer _recognizer;
    private Words _words;
    private struct Words
    {
        public string partial { get; set; }
    }
    
    public SpeechRecognizer(InputSpeechController inputSpeechController,
        VoskRecognizer recognizer) : base(inputSpeechController)
    {
        _recognizer = recognizer;
        _recognizer.SetMaxAlternatives(5);
        _recognizer.SetWords(true);
    }

    public override void Listen(object s, WaveInEventArgs waveInEventArgs)
    {
        lock (_recognizer)
        {
            _recognizer.AcceptWaveform(waveInEventArgs.Buffer, waveInEventArgs.BytesRecorded);
            // string result = _recognizer.PartialResult();
            // Console.WriteLine(result);
        // string words = _recognizer.PartialResult();

        // Console.WriteLine(words);
        // _words = JsonSerializer.Deserialize<Words>(words);

        // if (string.IsNullOrEmpty(_words.partial))
        // {
        //     return;
        // }
        // Console.WriteLine(_words.partial);
        // _recognizer.Reset();
        // Console.WriteLine(_words.text);
        // Console.WriteLine(_recognizer.PartialResult());
        // Console.WriteLine(_recognizer.FinalResult());
        }
    }
}