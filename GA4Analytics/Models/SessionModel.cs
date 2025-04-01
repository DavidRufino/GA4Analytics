namespace GA4Analytics.Models;

public partial class SessionModel
{
    public SessionModel() { }

    public SessionModel(string clientId, string sessionId, int sessionCount)
    {
        this.ClientId = clientId;
        this.SessionId2 = sessionId;
        this.SessionCount = sessionCount;
    }

    /// <summary>
    /// client_id - Required. A unique identifier for a client.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// user_id - Optional. A unique identifier for a user. See User-ID for cross-platform analysis for more information on this identifier.
    /// </summary>
    public string SessionId2 { get; set; }

    public int? SessionCount { get; set; }
}