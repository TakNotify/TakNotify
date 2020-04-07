// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakNotify
{
    /// <summary>
    /// The implementation of <see cref="INotification"/>
    /// </summary>
    public class Notification : INotification
    {
        private readonly ILogger<Notification> _logger;
        private readonly List<NotificationProvider> _providers;

        private static INotification _notification;

        /// <summary>
        /// Instantiate the <see cref="Notification"/>
        /// </summary>
        /// <param name="logger">The logger object</param>
        private Notification(ILogger<Notification> logger)
        {
            _logger = logger;
            _providers = new List<NotificationProvider>();
        }

        /// <inheritdoc cref="INotification.Send(string, MessageParameterCollection)"/>
        public async Task<NotificationResult> Send(string providerName, MessageParameterCollection messageParameters)
        {
            _logger.LogDebug(LogMessages.Intro_SendingMessage, providerName);

            var provider = GetProvider(providerName);
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

        /// <inheritdoc cref="INotification.AddProvider{TProvider}(TProvider)"/>
        public INotification AddProvider<TProvider>(TProvider provider)
            where TProvider : NotificationProvider
        {
            if (!_providers.Any(p => p.Name == provider.Name))
                _providers.Add(provider);

            return this;
        }

        /// <summary>
        /// Get the instance <see cref="INotification"/>
        /// </summary>
        /// <param name="logger">The logger object</param>
        /// <param name="reset">Create new instance</param>
        /// <returns></returns>
        public static INotification GetInstance(ILogger<Notification> logger, bool reset = false)
        {
            if (_notification == null || reset)
                _notification = new Notification(logger);

            return _notification;
        }

        private NotificationProvider GetProvider(string providerName)
        {
            return _providers.FirstOrDefault(p => p.Name == providerName);
        }

    }
}
