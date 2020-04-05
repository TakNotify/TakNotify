// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TakNotify
{
    /// <summary>
    /// The base class for notification provider that will send the message
    /// </summary>
    public abstract class NotificationProvider
    {
        /// <summary>
        /// Intantiate the <see cref="NotificationProvider"/>
        /// </summary>
        /// <param name="options">The options for the notification provider</param>
        /// <param name="loggerFactory">The logger factory</param>
        protected NotificationProvider(NotificationProviderOptions options, ILoggerFactory loggerFactory)
        {
            Options = options;
            Logger = loggerFactory.CreateLogger(GetType().FullName);
        }

        /// <summary>
        /// Name of the Notification Provider
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The generic options for the notification provider
        /// </summary>
        protected NotificationProviderOptions Options { get; }

        /// <summary>
        /// The logger object
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// The implementation of Send method that is specific to this provider
        /// </summary>
        /// <param name="messageParameters">The parameters of the message</param>
        /// <returns></returns>
        public abstract Task<NotificationResult> Send(MessageParameterCollection messageParameters);
    }
}
