using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TakNotify.Test
{
    public class DummyProvider : NotificationProvider
    {
        public const string DefaultName = "Dummy";

        public DummyProvider() 
            : base(new NotificationProviderOptions(), new NullLoggerFactory())
        {
        }

        public override string Name => DefaultName;

        public override Task<NotificationResult> Send(MessageParameterCollection messageParameters)
        {
            return Task.FromResult(new NotificationResult(true));
        }
    }
}
