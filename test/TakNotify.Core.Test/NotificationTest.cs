// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using TakNotify.Test;
using Xunit;

namespace TakNotify.Core.Test
{
    public class NotificationTest
    {
        private readonly Mock<ILogger<Notification>> _logger;

        public NotificationTest()
        {
            _logger = new Mock<ILogger<Notification>>();
        }

        [Fact]
        public async void Send_Success()
        {
            var provider = new DummyProvider(new NotificationResult(true));
            
            var notification = Notification.GetInstance(_logger.Object, true);
            notification.AddProvider(provider);

            var result = await notification.Send(DummyProvider.DefaultName, new MessageParameterCollection());

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);

            var introMessage = LoggerHelper.FormatLogValues(LogMessages.Intro_SendingMessage, DummyProvider.DefaultName);
            _logger.VerifyLog(LogLevel.Debug, introMessage);

            var successMessage = LoggerHelper.FormatLogValues(LogMessages.Success_Simple, DummyProvider.DefaultName);
            _logger.VerifyLog(LogLevel.Debug, successMessage);
            _logger.VerifyLog(LogLevel.Warning, Times.Never());
        }

        [Fact]
        public async void Send_Failed_NoProvider()
        {
            var notification = Notification.GetInstance(_logger.Object, true);

            var result = await notification.Send(DummyProvider.DefaultName, new MessageParameterCollection());

            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal($"Provider {DummyProvider.DefaultName} was not found", result.Errors[0]);

            var failedMessage = LoggerHelper.FormatLogValues(LogMessages.Failed_NoProvider, DummyProvider.DefaultName);
            _logger.VerifyLog(LogLevel.Warning, failedMessage);
        }

        [Fact]
        public async void Send_Failed_WithErrors()
        {
            var errors = new List<string> { "Error01" };
            var provider = new DummyProvider(new NotificationResult(errors));

            var notification = Notification.GetInstance(_logger.Object, true);
            notification.AddProvider(provider);

            var result = await notification.Send(DummyProvider.DefaultName, new MessageParameterCollection());

            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("Error01", result.Errors[0]);

            var failedMessage = LoggerHelper.FormatLogValues(LogMessages.Failed_WithErrors, DummyProvider.DefaultName, result.Errors);
            _logger.VerifyLog(LogLevel.Warning, failedMessage);

        }

        [Fact]
        public async void Send_Failed_ExceptionErrors()
        {
            var provider = new DummyProvider(new Exception("Exception01"));

            var notification = Notification.GetInstance(_logger.Object, true);
            notification.AddProvider(provider);

            var result = await notification.Send(DummyProvider.DefaultName, new MessageParameterCollection());

            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("Exception01", result.Errors[0]);

            var failedMessage = LoggerHelper.FormatLogValues(LogMessages.Failed_Simple, DummyProvider.DefaultName);
            _logger.VerifyLog(LogLevel.Warning, failedMessage);

        }
    }
}
