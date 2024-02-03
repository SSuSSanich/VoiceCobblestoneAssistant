using System.Configuration;

namespace Configurations;

public static class ValuesProvider
{
    private static string _configPath =
        @"F:\code\csharp\2023\VoiceCobblestone\Resources\Configurations\ValueConfigurations\test.config.xml";
    
    public static readonly int VoskLogLevel;
    public static readonly int AudioInSampleRate;
    public static readonly int SelectedInputDevice;
    public static readonly string PorcupineAccessKey;


    static ValuesProvider()
    {
        Configuration configuration = InitializeConfiguration();
        VoskLogLevel = Convert.ToInt32(configuration.AppSettings.Settings["VoskLogLevel"].Value);
        AudioInSampleRate = Convert.ToInt32(configuration.AppSettings.Settings["AudioInSampleRate"].Value);
        SelectedInputDevice = Convert.ToInt32(configuration.AppSettings.Settings["SelectedInputDevice"].Value);
        PorcupineAccessKey = configuration.AppSettings.Settings["PorcupineAccessKey"].Value;
    }

    private static Configuration InitializeConfiguration()
    {
        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = _configPath
        };
        return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

    }
}