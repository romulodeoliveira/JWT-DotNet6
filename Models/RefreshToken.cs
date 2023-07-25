namespace JwtWebApiTutorial;

public class RefreshToken
{
    public string Token { get; set; } = String.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Expires { get; set; }
}