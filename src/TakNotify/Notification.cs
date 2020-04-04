using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TakNotify
{
    /// <summary>
    /// The implementation of <see cref="INotification"/>
    /// </summary>
    public class Notification : INotification
    {
        private readonly ILogger<Notification> _logger;
        private readonly INotificationProviderFactory _providerFactory;

        /// <summary>
        /// Instantiate the <see cref="Notification"/>
        /// </summary>
        /// <param name="logger">The logger object</param>
        /// <param name="providerFactory">The factory for the notification providers</param>
        public Notification(ILogger<Notification> logger, INotificationProviderFactory providerFactory)
        {
            _logger = logger;
            _providerFactory = providerFactory;
        }

        /// <inheritdoc cref="INotification.Send(string, MessageParameterCollection)"/>
        public async Task<NotificationResult> Send(string providerName, MessageParameterCollection messageParameters)
        {
            _logger.LogDebug(LogMessages.Intro_SendingMessage, providerName);

            var provider = _providerFactory.GetProvider(providerName);
            if (provider == null)
            {
                _logger.LogWarning(LogMessages.Failed_NoProvider, providerName);
                return new NotificationResult(new List<string> { $"Provider {providerName} was not found" });
            }

            try
            {
                var result = await provider.Send(messageParameters);
                if (result.IsSuccess)
                    _logger.LogDebug(LogMessages.Success_Simple, providerName);
                else
                    _logger.LogWarning(LogMessages.Failed_WithErrors, providerName, result.Errors);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, LogMessages.Failed_Simple, providerName);
                return new NotificationResult(new List<string> { ex.Message });
            }
        }
    }
}
