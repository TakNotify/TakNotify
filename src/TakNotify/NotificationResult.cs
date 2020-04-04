using System.Collections.Generic;

namespace TakNotify
{
    /// <summary>
    /// Wrapper of a notification process result
    /// </summary>
    public class NotificationResult
    {
        /// <summary>
        /// Instantiate the <see cref="NotificationResult"/>
        /// </summary>
        /// <param name="isSuccess">The success status</param>
        public NotificationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Errors = new List<string>();
        }

        /// <summary>
        /// Instantiate the <see cref="NotificationResult"/> with <code>IsSuccess = false</code>
        /// </summary>
        /// <param name="errors">The list of errors</param>
        public NotificationResult(List<string> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        /// <summary>
        /// The success status
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// The list of errors
        /// </summary>
        public List<string> Errors { get; }
    }
}
