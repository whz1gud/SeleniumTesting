using Newtonsoft.Json;

namespace Task3.Base;

public class UserConfigManager
{
    private static readonly string ProjectBasePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
        .Parent.Parent.Parent.FullName;
    private static readonly string FilePath = Path.Combine(ProjectBasePath, "Config", "UserCredentials.json");

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