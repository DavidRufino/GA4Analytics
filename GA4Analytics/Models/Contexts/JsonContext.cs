using System.Text.Json.Serialization;

namespace GA4Analytics.Models.Contexts
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(SessionModel))]
    [JsonSerializable(typeof(MeasurementProtocolModel))]
    [JsonSerializable(typeof(Event))]
    [JsonSerializable(typeof(PageViewParams))]
    [JsonSerializable(typeof(CustomEventParams))]
    public partial class JsonContext : JsonSerializerContext
    {
    }
}