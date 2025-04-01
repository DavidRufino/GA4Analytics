using System.Text.Json.Serialization;

namespace GA4Analytics.Models
{
    /// <summary>
    /// JSON post body model to create a package to send requests to the Google Analytics Measurement Protocol.
    /// <para>
    /// DOC - https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference?client_type=gtag#payload_post_body
    /// </para>
    /// </summary>
    public partial class MeasurementProtocolModel
    {
        /// <summary>
        /// Required. Uniquely identifies a user instance of a web client. See send event to the Measurement Protocol.
        /// https://developers.google.com/gtagjs/reference/api#get_mp_example
        /// </summary>
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        /*
        /// <summary>
        /// Optional. A unique identifier for a user. See User-ID for cross-platform analysis for more information on this identifier.
        /// https://support.google.com/analytics/answer/9213390
        /// </summary>
        //[JsonPropertyName("user_id")]
        //public string? UserId { get; set; }
        */

        /*
        /// <summary>
        /// Obsolete: Use the ad_personalization field of consent instead of this field. Google Analytics accepts either field, but ad_personalization is recommended. 
        /// </summary>
        [Obsolete]
        [JsonPropertyName("non_personalized_ads")]
        public bool NonPersonalizedAds { get; set; }
        */

        /// <summary>
        /// Required. An array of event items. Up to 25 events can be sent per request. See the events reference for all valid events.
        /// </summary>
        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }
    }

    /// <summary>
    /// https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference/events
    /// </summary>
    public partial class Event
    {
        /// <summary>
        /// Required. The name for the event. See the events reference for all options.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional. The parameters for the event. See events for the suggested parameters for each event.
        /// https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference/events#parameters
        /// </summary>
        [JsonPropertyName("params")]
        public object Params { get; set; }
    }

    /// <summary>
    /// The following event names are reserved and cannot be used: 
    /// https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference?client_type=gtag#reserved_names
    /// </summary>
    public partial class PageViewParams
    {
        [JsonPropertyName("page_title")]
        public string PageTitle { get; set; }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("engagement_time_msec")]
        public long EngagementTimeMsec { get; set; }

        [JsonPropertyName("debug_mode")]
        public bool DebugMode { get; set; }
    }

    public class CustomEventParams
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("engagement_time_msec")]
        public long EngagementTimeMsec { get; set; }

        [JsonPropertyName("debug_mode")]
        public bool DebugMode { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> AdditionalProperties { get; set; }
    }
}