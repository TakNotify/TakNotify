namespace TakNotify
{
    public static class LogMessages
    {
        public const string Intro_SendingMessage = "Sending message through {providerName}";
        public const string Failed_Simple = "Failed to send message through {providerName}";
        public const string Failed_NoProvider = "Failed to send message through {providerName}. The provider was not found";
        public const string Failed_WithErrors = "Failed to send message through {providerName}. Errors: {errors}";
        public const string Success_Simple = "Message has been sent through {providerName}";
    }
}
