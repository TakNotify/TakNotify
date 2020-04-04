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
    }
}
