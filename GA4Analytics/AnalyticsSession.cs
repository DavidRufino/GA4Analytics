using GA4Analytics.Enums;
using GA4Analytics.Helpers;
using GA4Analytics.Interfaces;
using GA4Analytics.Models;
using GA4Analytics.Models.Contexts;
using GA4Analytics.Services;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace GA4Analytics;

public class AnalyticsSession : IAnalytics
{
    private readonly ISessionRepository _sessionRepository;
    private readonly GA4Service _service;

    private AnalyticsHelper _analyticsHelper;

    /// <summary>
    /// Obter o tempo que a aplicacao foi aberta
    /// <para>Necessario para saber quanto tempo a aplicacao ficou aberta</para>
    /// </summary>
    private TimeSpan _startApplicationTime { get; set; }

    private SessionModel _session { get; set; }

    public AnalyticsSession(ISessionRepository sessionRepository, HttpClient? httpClient = null)
    {
        string measurementId = "G-XXXX";
        string apiSecret = "";

        this._service = new(measurementId, apiSecret, httpClient ?? new HttpClient());

        this._sessionRepository = sessionRepository;

        Initialize();
    }

    private void Initialize()
    {
        this._startApplicationTime = DateTime.Now.TimeOfDay;
        this._analyticsHelper = new();

        this._session = this._sessionRepository.GetSession();

        if (!Validate(this._session))
        {
            Debug.WriteLine("[AnalyticsSession] Invalid or missing SessionModel");

            var clientId = this._analyticsHelper.GetUniqueVisitorId();
            var sessionId = this._analyticsHelper.GenerateSessionId();

            //  Criar um novo sessao
            SessionModel session = new(clientId, sessionId, 0);
            UpdateSession(session);
        }
    }

    public void UpdateSession(SessionModel session)
    {
        if (!Validate(session)) return;

        this._session = session;
        this._sessionRepository.UpdateSession(this._session);
    }

    public void UpdateClientId(string clientId)
    {
        if (string.IsNullOrWhiteSpace(clientId)) return;

        this._session.ClientId = clientId;
        this._sessionRepository.UpdateSession(this._session);
    }

    public void UpdateSessionId(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId)) return;

        this._session.SessionId2 = sessionId;
        this._sessionRepository.UpdateSession(this._session);
    }

    public async Task<bool> TrackPageViewAsync(string pageName)
    {
        long totalTimeMsec = (long)DateTime.Now.TimeOfDay.Subtract(this._startApplicationTime).TotalMilliseconds;

        PageViewParams pageViewParams = new PageViewParams();
        pageViewParams.PageTitle = pageName;
        pageViewParams.SessionId = this._session.SessionId2;
        pageViewParams.EngagementTimeMsec = totalTimeMsec;
        pageViewParams.DebugMode = false;

        Event eventProtocol = new Event();
        eventProtocol.Name = EventType.page_view.ToString();
        eventProtocol.Params = pageViewParams;

        MeasurementProtocolModel measurementProtocol = new MeasurementProtocolModel();
        measurementProtocol.ClientId = this._session.ClientId;
        measurementProtocol.Events = new();
        measurementProtocol.Events.Add(eventProtocol);

        JsonTypeInfo<MeasurementProtocolModel> typeInfo = JsonContext.Default.MeasurementProtocolModel;
        string jsonString = JsonSerializer.Serialize(measurementProtocol, typeInfo);

        var result = await this._service.PostAsync(jsonString);
        return result;
    }

    /// <summary>
    /// [GA4] Create or modify events
    /// https://support.google.com/analytics/answer/12844695?hl=en
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task<bool> TrackCustomEventAsync(string eventName, string value)
    {
        Debug.WriteLine($"TrackCustomEventAsync eventName: {eventName}; value {value}");

        long totalTimeMsec = (long)DateTime.Now.TimeOfDay.Subtract(this._startApplicationTime).TotalMilliseconds;

        //  Criando uma propriedade customizada baseado no nome do evento e acrescentando: _title
        string customPropertieName = string.Format("{0}_title", eventName.ToLower());

        CustomEventParams eventParams = new();
        eventParams.AdditionalProperties = new();
        eventParams.AdditionalProperties.Add(customPropertieName, value);
        eventParams.SessionId = this._session.SessionId2;
        eventParams.EngagementTimeMsec = totalTimeMsec;
        eventParams.DebugMode = false;

        Event eventProtocol = new Event();
        eventProtocol.Name = eventName;
        eventProtocol.Params = eventParams;

        MeasurementProtocolModel measurementProtocol = new MeasurementProtocolModel();
        measurementProtocol.ClientId = this._session.ClientId;
        measurementProtocol.Events = new();
        measurementProtocol.Events.Add(eventProtocol);

        JsonTypeInfo<MeasurementProtocolModel> typeInfo = JsonContext.Default.MeasurementProtocolModel;
        string jsonString = JsonSerializer.Serialize(measurementProtocol, typeInfo);

        var result = await this._service.PostAsync(jsonString);
        return result;
    }

    private bool Validate(SessionModel session)
    {
        if (session is null) return false;
        if (session.ClientId is null) return false;
        if (session.SessionId2 is null) return false;

        return true;
    }
}