// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TakNotify.Core.Test {
    public class DummyProvider : NotificationProvider {
        public const string DefaultName = "Dummy";
        private readonly NotificationResult _returnedResult;
        private readonly Exception _thrownException;

        public DummyProvider (NotificationResult returnedResult) 
            : base (new NotificationProviderOptions (), new NullLoggerFactory ()) {
            _returnedResult = returnedResult;
        }
        
        public DummyProvider (Exception thrownException) 
            : base (new NotificationProviderOptions (), new NullLoggerFactory ()) {
            _thrownException = thrownException;
        }

        public override string Name => DefaultName;

        public override Task<NotificationResult> Send (MessageParameterCollection messageParameters) {
            if (_thrownException != null)
                throw _thrownException;

            return Task.FromResult (_returnedResult);
        }
    }
}