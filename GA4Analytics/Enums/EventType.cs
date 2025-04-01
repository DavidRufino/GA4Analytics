namespace GA4Analytics.Enums;

/// <summary>
/// Enum Type of An array of event items. Up to 25 events can be sent per request. 
/// See the events reference for all valid events.
/// <para>https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference/events</para>
/// <para>https://developers.google.com/analytics/devguides/collection/protocol/ga4/reference?client_type=gtag#reserved_event_names</para>
/// </summary>
public enum EventType
{
    page_view,
    screen_view,
    session_start,
    user_engagement,
    share,
    purchase,
    add_to_cart,
    remove_from_cart,
    begin_checkout,
    add_payment_info,
    add_shipping_info,
    view_item,
    view_item_list,
    select_item,
    select_item_list,
    login,
    sign_up,
    generate_lead,
    search,
    tutorial_begin,
    tutorial_complete,
    app_install,
    app_update,
    app_exception,
    ad_impression,
    ad_click
}