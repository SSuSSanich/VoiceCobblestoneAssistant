using NAudio.Wave;
using Pv;

namespace VoiceCobblestone;

public class ActivationWordRecognizer : SoundsHandler
{
    private short[] _saveBuffer;
    private Porcupine _wordRecognizer;
    private Queue<short[]> _frames;

    public ActivationWordRecognizer(InputSpeechController inputSpeechController,
        Porcupine wordRecognizer) : base(inputSpeechController)
    {
        _saveBuffer = Array.Empty<short>();
        _wordRecognizer = wordRecognizer;
        _frames = new Queue<short[]>();
    }
    
    public override void Listen(object s, WaveInEventArgs waveInEventArgs)
    {
        
        LoadFrames(waveInEventArgs);
        while (_frames.Count != 0)
        {
            int keywordIndex = _wordRecognizer.Process(_frames.Dequeue());
            
            switch (keywordIndex)
            {
                case 0: Console.WriteLine("Shalnoi ti sho");
                    InputSpeechController.ChooseOption();
                    break;
            }
        }
        
        
    }

    private void LoadFrames(WaveInEventArgs waveInEventArgs)
    {
        short[] buffer = new short[waveInEventArgs.BytesRecorded / 2];
        Buffer.BlockCopy(waveInEventArgs.Buffer, 0, buffer, 0, waveInEventArgs.BytesRecorded);
    
        short[] finalBuffer = new short[_saveBuffer.Length + buffer.Length];
        _saveBuffer.CopyTo(finalBuffer, 0);
        buffer.CopyTo(finalBuffer, _saveBuffer.Length);
        
        _saveBuffer = Array.Empty<short>();
        
        try
        {

            for (int i = 0; i < finalBuffer.Length; i += 512)
            {
                int frameLength = 512;
                short[] frame = new short[frameLength];
    
                if (finalBuffer.Length - i < frameLength)
                {
                    _saveBuffer = new short[finalBuffer.Length - i];
                    Array.Copy(finalBuffer, i, _saveBuffer, 0, finalBuffer.Length - i);
    
                    break;
                }
                
                Array.Copy(finalBuffer, i, frame, 0, frameLength);
                
                _frames.Enqueue(frame);
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 
}