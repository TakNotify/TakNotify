// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System.Threading.Tasks;

namespace TakNotify
{
    /// <summary>
    /// The notification interface
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// Generic method to send a message through a provider
        /// </summary>
        /// <param name="providerName">The name of provider that will send the message</param>
        /// <param name="messageParameters">The parameters of the message (it will be varied depending on the provider)</param>
        /// <returns></returns>
        Task<NotificationResult> Send(string providerName, MessageParameterCollection messageParameters);

        /// <summary>
        /// Add a provider into the provider collection
        /// </summary>
        /// <typeparam name="TProvider">Type of the provider</typeparam>
        /// <param name="provider">The provider object</param>
        /// <returns></returns>
        INotification AddProvider<TProvider>(TProvider provider)
            where TProvider : NotificationProvider;
    }
}
