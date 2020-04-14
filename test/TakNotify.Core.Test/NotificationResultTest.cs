// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using System.Collections.Generic;
using Xunit;

namespace TakNotify.Core.Test
{
    public class NotificationResultTest
    {
        [Fact]
        public void Check_IsSuccess_True()
        {
            var result = new NotificationResult(true);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ReturnedValues);
            Assert.Empty(result.ReturnedValues);
        }

        [Fact]
        public void Check_IsSuccess_True_WithReturnedValues()
        {
            var result = new NotificationResult(true, new Dictionary<string, object> { { "key1", "value1" } });

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ReturnedValues);
            Assert.NotEmpty(result.ReturnedValues);
            Assert.Equal("value1", result.ReturnedValues["key1"]);
        }

        [Fact]
        public void Check_IsSuccess_False()
        {
            var result = new NotificationResult(false);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ReturnedValues);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Check_IsSuccess_False_WithErrors()
        {
            var result = new NotificationResult(new List<string> { "error1" });

            Assert.False(result.IsSuccess);
            Assert.Null(result.ReturnedValues);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("error1", result.Errors[0]);
        }
    }
}
