using Configurations;
using Vosk;
using Pv;
using VoiceCobblestone;



string voskModelFolder = FilePathsProvider.VoskModel;
int voskLogLevel = ValuesProvider.VoskLogLevel;
int audioInSampleRate = ValuesProvider.AudioInSampleRate;
int selectedInputDevice = ValuesProvider.SelectedInputDevice;
string accessKey = ValuesProvider.PorcupineAccessKey;
string porcupineRuModelPath = FilePathsProvider.PorcupineModel;

Porcupine porcupine = Porcupine.FromKeywordPaths(
    accessKey,
    new []{FilePathsProvider.PorcupineActivationWord1},
    porcupineRuModelPath
);

Vosk.Vosk.SetLogLevel(voskLogLevel);
VoskRecognizer recognizer;

var model = new Model(voskModelFolder);
recognizer = new VoskRecognizer(model, audioInSampleRate);


InputSpeechController inputSpeechController = 
    new InputSpeechController(porcupine, recognizer);

Console.WriteLine("Начало записи");
inputSpeechController.StartRecording();
Thread.Sleep(100000000);
inputSpeechController.StopRecording();
Console.WriteLine("Конец записи");



