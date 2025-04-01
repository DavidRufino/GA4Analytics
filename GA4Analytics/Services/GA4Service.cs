using System.Diagnostics;
using System.Text;

namespace GA4Analytics.Services;

public class GA4Service
{
    /// <summary>
    /// Base Url for Google Analitycs
    /// <para>https://developers.google.com/analytics/devguides/collection/protocol/ga4/sending-events?client_type=gtag</para>
    /// </summary>
    private const string BaseURL = "https://www.google-analytics.com/";
    private const string MeasurementProtocol_Endpoint = "mp/collect?measurement_id={0}&api_secret={1}";

    /// <summary>
    /// Measurement ID. The identifier for a Data Stream. Found in the Google Analytics UI under:
    /// Admin > Data Streams > choose your stream > Measurement ID
    /// <para> https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference?client_type=gtag#payload_query_parameters </para>
    /// </summary>
    private readonly string _measurementId;

    /// <summary>
    /// Required. An API Secret that is generated through the Google Analytics UI.
    /// To create a new secret, navigate in the Google Analytics UI to:
    /// Admin > Data Streams > choose your stream > Measurement Protocol > Create
    /// We recommend that you keep these private to your organization. If you deploy the measurement protocol 
    /// client-side, you should regularly rotate api_secrets to avoid excessive SPAM.
    /// <para> https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference?client_type=gtag#payload_query_parameters </para>
    /// </summary>
    private readonly string _apiSecret;

    // Debug mode
    //private const string MeasurementProtocol_Endpoint = "debug/mp/collect?measurement_id={0}&api_secret={1}";

    private readonly HttpClient _httpClient;

    public GA4Service(string measurementId, string apiSecret, HttpClient httpClient)
    {
        this._measurementId = measurementId ?? throw new ArgumentNullException(nameof(measurementId));
        this._apiSecret = apiSecret ?? throw new ArgumentNullException(nameof(apiSecret));
        this._httpClient = httpClient ?? new HttpClient();
    }

    /// <summary>
    /// Sends a POST request with a JSON payload.
    /// </summary>
    public async Task<bool> PostAsync(string? data)
    {
        try
        {
            Debug.WriteLine("[BaseAnalytics] PostAsync START");

            string requestUri = string.Format(MeasurementProtocol_Endpoint, this._measurementId, this._apiSecret);
            using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(new Uri(BaseURL), requestUri));

            // Handle null or empty data properly
            if (!string.IsNullOrWhiteSpace(data))
            {
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"[BaseAnalytics] PostAsync response: {result}");

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine($"[BaseAnalytics] PostAsync ERROR: {ex.Message}");
            return false;
        }
    }
}