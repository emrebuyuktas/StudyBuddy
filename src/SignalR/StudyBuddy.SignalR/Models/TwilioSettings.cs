namespace StudyBuddy.SignalR.Models;

public static class TwilioSettings
{
    /// <summary>
    /// The primary Twilio account SID, displayed prominently on your twilio.com/console dashboard.
    /// </summary>
    public static string? AccountSid { get; set; } = "ACd395e11bdf4e7046c4c3637d53700e5f";

    /// <summary>
    /// Signing Key SID, also known as the API SID or API Key.
    /// </summary>
    public static string? ApiKey { get; set; } = "SK364dac84823b16c7ffa7e35b0ef3f121";

    /// <summary>
    /// The API Secret that corresponds to the <see cref="ApiKey"/>.
    /// </summary>
    public static string? ApiSecret { get; set; } = "eq2o8rlnFvoRRP5hrybK5nQezk1N31Fy";
}