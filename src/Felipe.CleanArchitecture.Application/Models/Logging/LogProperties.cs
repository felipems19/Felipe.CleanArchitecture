namespace Felipe.CleanArchitecture.Application.Models.Logging;

public class LogProperties : Dictionary<string, object>
{
    public string HttpMethod
    {
        get => this["HttpMethod"] as string ?? string.Empty;
        set => this["HttpMethod"] = value;
    }

    public string Username
    {
        get => this["Username"] as string ?? string.Empty;
        set => this["Username"] = value;
    }

    public string RequestedUrl
    {
        get => this["RequestedUrl"] as string ?? string.Empty;
        set => this["RequestedUrl"] = value;
    }

    public TimeSpan TimeTaken
    {
        get => (TimeSpan)(this["TimeTaken"] ?? default(TimeSpan));
        set => this["TimeTaken"] = value;
    }

    public int ResponseCode
    {
        get => (int)(this["ResponseCode"] ?? default(int));
        set => this["ResponseCode"] = value;
    }

    public string ResponseDetails
    {
        get => this["ResponseDetails"] as string ?? string.Empty;
        set => this["ResponseDetails"] = value;
    }
}
