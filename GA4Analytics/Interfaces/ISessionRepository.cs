using GA4Analytics.Models;

namespace GA4Analytics.Interfaces;

public interface ISessionRepository
{
    SessionModel? GetSession();
    void UpdateSession(SessionModel sessionCookie);
}