using System.Collections.Generic;
using System.Linq;

namespace TakNotify
{
    /// <summary>
    /// The factory of the notification providers (singleton)
    /// </summary>
    public class NotificationProviderFactory : INotificationProviderFactory
    {
        private readonly List<NotificationProvider> _providers;
        private static readonly NotificationProviderFactory _instance = new NotificationProviderFactory();

        /// <summary>
        /// Instantiate the <see cref="NotificationProviderFactory"/>
        /// </summary>
        private NotificationProviderFactory()
        {
            _providers = new List<NotificationProvider>();
        }

        /// <summary>
        /// Get the instance of <see cref="NotificationProviderFactory"/> singleton
        /// </summary>
        /// <returns></returns>
        public static NotificationProviderFactory GetInstance() => _instance;

        /// <summary>
        /// Add a provider into the provider collection
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public INotificationProviderFactory AddProvider<TProvider>(TProvider provider)
            where TProvider : NotificationProvider
        {
            _providers.Add(provider);

            return this;
        }

        /// <summary>
        /// Get a notification provider by the name
        /// </summary>
        /// <param name="providerName">The provider name</param>
        /// <returns></returns>
        public NotificationProvider GetProvider(string providerName)
        {
            return _providers.FirstOrDefault(p => p.Name == providerName);
        }
    }
}
