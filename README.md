
# GA4Analytics .NET 8

**GA4Analytics .NET 8** is an open-source .NET 8 library that simplifies the integration of Google Analytics 4 (GA4) event tracking in .NET applications. It provides an easy-to-use interface for managing sessions and sending page views and custom events to Google Analytics.

## Features
- Track page views with engagement time in GA4.
- Track custom events with additional properties.
- Session management with automatic session ID and client ID handling.
- Support for HTTP client configuration and custom session repositories.

## Requirements
- .NET 8 SDK
- Google Analytics 4 (GA4) account with a valid Measurement ID and API Secret

## Usage

### Basic Setup

To start using GA4Analytics, you need to initialize the `AnalyticsSession` class, providing a session repository and an optional `HttpClient`.

```csharp
using GA4Analytics;

public partial class MainWindow
{
    public static AnalyticsSession? AnalyticsSession;

    public MainWindow()
    {
        // Initialize the AnalyticsSession
        this.AnalyticsSession = new(CacheRepository.Instance, HttpClientManager.Instance);
        // Or you can initialize without the custom HttpClient:
        // this.AnalyticsSession = new(CacheRepository.Instance);
    }
}
```

### Track Page View

To track a page view, call the `TrackPageViewAsync` method, passing the page name as a string:

```csharp
await AnalyticsSession.TrackPageViewAsync("HomePage");
```

This will send the page view event to Google Analytics, including the session details and engagement time.

### Track Custom Event

To track a custom event, use the `TrackCustomEventAsync` method. Provide the event name and any associated value:

```csharp
await AnalyticsSession.TrackCustomEventAsync("button_click", "cta_button");
```

This will send a custom event to GA4 with the specified event name and value.

### Managing Session Data

The `AnalyticsSession` class also provides methods to update or modify the session and client ID:

```csharp
// Update the session with a new SessionModel
AnalyticsSession.UpdateSession(new SessionModel(clientId, sessionId, 0));

// Update client ID
AnalyticsSession.UpdateClientId("new-client-id");

// Update session ID
AnalyticsSession.UpdateSessionId("new-session-id");
```

## Code Overview

The `AnalyticsSession` class is responsible for managing sessions and sending data to Google Analytics 4.

- **Session Management**: The class tracks session information like `ClientId` and `SessionId`. It ensures that every session is valid before sending events.
- **Track Page View**: The `TrackPageViewAsync` method tracks page views, including engagement time and session details.
- **Track Custom Events**: The `TrackCustomEventAsync` method allows tracking custom events with additional properties.

### Constructor

```csharp
public AnalyticsSession(ISessionRepository sessionRepository, HttpClient? httpClient = null)
```

This constructor initializes the `AnalyticsSession` with a session repository and an optional `HttpClient` for making requests to the Google Analytics API.

### Methods

- **`UpdateSession(SessionModel session)`**: Updates the session data.
- **`UpdateClientId(string clientId)`**: Updates the client ID for the current session.
- **`UpdateSessionId(string sessionId)`**: Updates the session ID for the current session.
- **`TrackPageViewAsync(string pageName)`**: Sends a page view event to GA4.
- **`TrackCustomEventAsync(string eventName, string value)`**: Sends a custom event to GA4.

## Acknowledgements

- [Google Analytics 4 (GA4) Measurement Protocol](https://developers.google.com/analytics/devguides/collection/protocol/ga4)
- [[GA4] Create or modify events](https://support.google.com/analytics/answer/12844695?hl=en)