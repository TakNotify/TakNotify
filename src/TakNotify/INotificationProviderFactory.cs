namespace TakNotify
{
    /// <summary>
    /// The interface for notification provider factory
    /// </summary>
    public interface INotificationProviderFactory
    {
        /// <summary>
        /// Add a provider into the provider collection
        /// </summary>
        /// <typeparam name="TProvider">Type of the provider</typeparam>
        /// <param name="provider">The provider object</param>
        /// <returns></returns>
        INotificationProviderFactory AddProvider<TProvider>(TProvider provider) 
            where TProvider : NotificationProvider;

        /// <summary>
        /// Get a notification provider by the name
        /// </summary>
        /// <param name="providerName">The provider name</param>
        /// <returns></returns>
        NotificationProvider GetProvider(string providerName);
    }
}
