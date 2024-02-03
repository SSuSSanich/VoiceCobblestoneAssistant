using Configurations;
using NAudio.Wave;
using Pv;
using Vosk;

namespace VoiceCobblestone;

public class InputSpeechController
{
    private WaveInEvent _waveInEvent;
    private readonly int _selectedInputDevice = ValuesProvider.SelectedInputDevice;
    private readonly int _audioInSampleRate = ValuesProvider.AudioInSampleRate;
    private SoundsHandler _currentListener;
    private List<SoundsHandler> _soundsListeners;

    private VoskRecognizer _recognizer;

    public InputSpeechController(Porcupine porcupine, VoskRecognizer recognizer)
    {
        
        _recognizer = recognizer;
        
        _soundsListeners = new List<SoundsHandler>
        {
            new ActivationWordRecognizer(this, porcupine),
            new SpeechRecognizer(this, recognizer)
        };
        _currentListener = _soundsListeners[0];
        
        _waveInEvent = new WaveInEvent
        {
            DeviceNumber = _selectedInputDevice,
            WaveFormat = new WaveFormat(_audioInSampleRate, 1)
        };
        _waveInEvent.DataAvailable += _currentListener.Listen;
        
    }

    public void ChangeListener<T>() where T : SoundsHandler
    {
        _waveInEvent.DataAvailable -= _currentListener.Listen;
        _currentListener = _soundsListeners.FirstOrDefault(listener => listener is T) ??
                           throw new InvalidOperationException("There is no such state");
        _waveInEvent.DataAvailable += _currentListener.Listen;
    }

    public void ChooseOption()
    {
        
        ChangeListener<SpeechRecognizer>();

        Task.Run(Choose);

        //command = CommandRecognizer.Recognize(List of commands)

    }

    private void Choose()
    {
        Thread.Sleep(2000);
        lock (_recognizer)
        {
            string result = _recognizer.PartialResult();
            Console.WriteLine("///////////////////////////////");
            Console.WriteLine(result);
            Console.WriteLine("///////////////////////////////");
        }
    }

    public void RecordingStopped()
    {
        _waveInEvent.RecordingStopped += (_, _) => { _waveInEvent.Dispose(); };
    }

    public void StartRecording()
    {
        _waveInEvent.StartRecording();
    }
    
    public void StopRecording()
    {
        _waveInEvent.StopRecording();
    }

}