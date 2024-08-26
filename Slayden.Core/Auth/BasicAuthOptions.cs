namespace Slayden.Core.Auth;

public class BasicAuthOptions
{
    public static string SectionKey = "BasicAuth";

    public string Username { get; set; }

    public string Password { get; set; }
}
