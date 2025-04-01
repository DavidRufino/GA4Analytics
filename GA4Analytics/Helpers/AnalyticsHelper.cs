using System.Globalization;

namespace GA4Analytics.Helpers;

internal class AnalyticsHelper
{
    private readonly Random RandomGenerator;

    public AnalyticsHelper()
    {
        this.RandomGenerator = new Random((int)DateTime.UtcNow.Ticks);
    }

    /// <summary>
    /// Generates a unique visitor ID based on random numbers and a fixed value.
    /// </summary>
    public string GetUniqueVisitorId()
    {
        return string.Format("{0}.{1}", RandomGenerator.Next(100000000, 999999999), RandomGenerator.Next(100000000, 999999999));
    }

    /// <summary>
    /// Generates a new session ID.
    /// </summary>
    public string GenerateSessionId()
    {
        return RandomGenerator.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
    }
}
