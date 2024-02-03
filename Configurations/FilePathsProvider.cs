using System.Configuration;

namespace Configurations;

public static class FilePathsProvider
{
    private static string _configPath =
        @"F:\code\csharp\2023\VoiceCobblestone\Resources\Configurations\RequiredFilesConfiguration\test.config.xml";
    
    public static readonly string VoskModel;
    public static readonly string PorcupineModel;
    
    public static readonly string PorcupineActivationWord1;

    static FilePathsProvider()
    {
        Configuration configuration = InitializeConfiguration();
        VoskModel = configuration.AppSettings.Settings["VoskModel"].Value;
        PorcupineModel = configuration.AppSettings.Settings["PorcupineModel"].Value;
        PorcupineActivationWord1 = configuration.AppSettings.Settings["PorcupineActivationWord1"].Value;
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