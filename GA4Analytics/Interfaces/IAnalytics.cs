namespace GA4Analytics.Interfaces;

public interface IAnalytics
{
    Task<bool> TrackPageViewAsync(string pageName);
    Task<bool> TrackCustomEventAsync(string eventName, string value);
}