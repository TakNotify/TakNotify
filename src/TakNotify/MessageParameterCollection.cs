// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System.Collections.Generic;

namespace TakNotify
{
    /// <summary>
    /// Message parameters to be used by the generic <see cref="INotification.Send(string, MessageParameterCollection)"/>
    /// </summary>
    public class MessageParameterCollection : Dictionary<string, string>
    {
    }
}
