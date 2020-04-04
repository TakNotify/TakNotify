using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TakNotify.Test
{
    public class NotificationTest
    {
        private const string providerName = "provider01";

        private readonly Mock<ILogger<Notification>> _logger;
        private readonly Mock<INotificationProviderFactory> _providerFactory;
        private readonly Mock<DummyProvider> _provider;

        public NotificationTest()
        {
            _logger = new Mock<ILogger<Notification>>();
            _providerFactory = new Mock<INotificationProviderFactory>();
            _provider = new Mock<DummyProvider>();
        }

        [Fact]
        public async void Send_Success()
        {
            _providerFactory.Setup(factory => factory.GetProvider(It.IsAny<string>()))
                .Returns(_provider.Object);
            _provider.Setup(provider => provider.Send(It.IsAny<MessageParameterCollection>()))
                .ReturnsAsync(new NotificationResult(true));

            var notification = new Notification(_logger.Object, _providerFactory.Object);

            var result = await notification.Send(providerName, new MessageParameterCollection());

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);

            var introMessage = LoggerHelper.FormatLogValues(LogMessages.Intro_SendingMessage, providerName);
            _logger.VerifyLog(LogLevel.Debug, introMessage);

            var successMessage = LoggerHelper.FormatLogValues(LogMessages.Success_Simple, providerName);
            _logger.VerifyLog(LogLevel.Debug, successMessage);
            _logger.VerifyLog(LogLevel.Warning, Times.Never());
        }

        [Fact]
        public async void Send_Failed_NoProvider()
        {
            _providerFactory.Setup(factory => factory.GetProvider(It.IsAny<string>()))
                .Returns((NotificationProvider)null);
           
            var notification = new Notification(_logger.Object, _providerFactory.Object);

            var result = await notification.Send(providerName, new MessageParameterCollection());

            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal($"Provider {providerName} was not found", result.Errors[0]);

            var failedMessage = LoggerHelper.FormatLogValues(LogMessages.Failed_NoProvider, providerName);
            _logger.VerifyLog(LogLevel.Warning, failedMessage);
        }

        [Fact]
        public async void Send_Failed_WithErrors()
        {
            var errors = new List<string> { "Error01" };

            _providerFactory.Setup(factory => factory.GetProvider(It.IsAny<string>()))
                .Returns(_provider.Object);
            _provider.Setup(provider => provider.Send(It.IsAny<MessageParameterCollection>()))
                .ReturnsAsync(new NotificationResult(errors));

            var notification = new Notification(_logger.Object, _providerFactory.Object);

            var result = await notification.Send(providerName, new MessageParameterCollection());

            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("Error01", result.Errors[0]);

            var failedMessage = LoggerHelper.FormatLogValues(LogMessages.Failed_WithErrors, providerName, result.Errors);
            _logger.VerifyLog(LogLevel.Warning, failedMessage);

        }

        [Fact]
        public async void Send_Failed_ExceptionErrors()
        {
            _providerFactory.Setup(factory => factory.GetProvider(It.IsAny<string>()))
                .Returns(_provider.Object);
            _provider.Setup(provider => provider.Send(It.IsAny<MessageParameterCollection>()))
                .Throws(new Exception("Exception01"));

            var notification = new Notification(_logger.Object, _providerFactory.Object);

            var result = await notification.Send(providerName, new MessageParameterCollection());

            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("Exception01", result.Errors[0]);

            var failedMessage = LoggerHelper.FormatLogValues(LogMessages.Failed_Simple, providerName);
            _logger.VerifyLog(LogLevel.Warning, failedMessage);

        }
    }
}
