namespace TakNotify
{
    /// <summary>
    /// The log messages
    /// </summary>
    public static class LogMessages
    {
        /// <summary>
        /// The intro message when a provider is sending a message
        /// </summary>
        public const string Intro_SendingMessage = "Sending message through {providerName}";

        /// <summary>
        /// The simple message when a provider is failed to send a message
        /// </summary>
        public const string Failed_Simple = "Failed to send message through {providerName}";

        /// <summary>
        /// The message when a message is failed to be sent because the provider was not found
        /// </summary>
        public const string Failed_NoProvider = "Failed to send message through {providerName}. The provider was not found";

        /// <summary>
        /// The message when a a provider is failed to send a message because of certain errors
        /// </summary>
        public const string Failed_WithErrors = "Failed to send message through {providerName}. Errors: {errors}";

        /// <summary>
        /// The simple message when a provider has sent a message successfully
        /// </summary>
        public const string Success_Simple = "Message has been sent through {providerName}";
    }
}
