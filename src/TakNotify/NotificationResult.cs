// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
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
        /// <param name="returnedValues">
        /// Returned values when <see cref="IsSuccess"/> is <c>true</c>.
        /// <br/>If <see cref="IsSuccess"/> is <c>false</c>, it will be ignored.
        /// </param>
        public NotificationResult(bool isSuccess, Dictionary<string, object> returnedValues = null)
        {
            IsSuccess = isSuccess;
            Errors = new List<string>();

            if (isSuccess)
                ReturnedValues = returnedValues ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Instantiate the <see cref="NotificationResult"/> with <see cref="IsSuccess"/> is set to <c>false</c>
        /// </summary>
        /// <param name="errors">The list of errors</param>
        public NotificationResult(List<string> errors)
        {
            IsSuccess = false;
            Errors = errors ?? new List<string>();
        }

        /// <summary>
        /// The success status
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// The list of errors
        /// </summary>
        public List<string> Errors { get; }

        /// <summary>
        /// Returned values when <see cref="IsSuccess"/> is <c>true</c>.
        /// <br/>If <see cref="IsSuccess"/> is <c>false</c>, it will be <c>null</c>
        /// </summary>
        public Dictionary<string, object> ReturnedValues { get; }
    }
}
