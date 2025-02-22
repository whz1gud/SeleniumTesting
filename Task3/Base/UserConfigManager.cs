using Newtonsoft.Json;

namespace Task3.Base;

public class UserConfigManager
{
    private static readonly string FilePath = "C:\\Users\\whz1gud\\RiderProjects\\SeleniumTesting\\Task3\\Config\\UserCredentials.json";

    public static bool CredentialsExist()
    {
        return File.Exists(FilePath) && !string.IsNullOrWhiteSpace(File.ReadAllText(FilePath));
    }

    public static UserCredentials ReadCredentials()
    {
        if (!CredentialsExist())
            return null;
        string json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<UserCredentials>(json);
    }

    public static void SaveCredentials(UserCredentials credentials)
    {
        string json = JsonConvert.SerializeObject(credentials, Formatting.Indented);
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
        File.WriteAllText(FilePath, json);
    }
}