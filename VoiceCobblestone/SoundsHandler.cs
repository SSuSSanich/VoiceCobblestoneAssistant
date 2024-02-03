using NAudio.Wave;

namespace VoiceCobblestone;

public abstract class SoundsHandler
{
    protected InputSpeechController InputSpeechController;
    
    public SoundsHandler(InputSpeechController inputSpeechController)
    {
        InputSpeechController = inputSpeechController;
    }

    public abstract void Listen(object s, WaveInEventArgs waveInEventArgs);
}