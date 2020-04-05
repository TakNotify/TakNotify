// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System.Collections.Generic;

namespace TakNotify
{
    /// <summary>
    /// The generic options for notification provider
    /// </summary>
    public class NotificationProviderOptions
    {
        /// <summary>
        /// Instantiate the <see cref="NotificationProviderOptions"/>
        /// </summary>
        public NotificationProviderOptions()
        {
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// The parameter dictionary
        /// </summary>
        protected Dictionary<string, object> Parameters { get; }
    }
}
