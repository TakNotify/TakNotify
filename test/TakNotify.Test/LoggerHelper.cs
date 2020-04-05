// Copyright (c) Frandi Dwi 2020. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TakNotify.Test
{
    public static class LoggerHelper
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, Times times)
        {
            loggerMock.VerifyLog(level, null, times, null);
        }

        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message)
        {
            loggerMock.VerifyLog(level, message, Times.Once(), null);
        }

        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Times times, string failMessage = null)
        {
            loggerMock.Verify(l => l.Log(
                    level, 
                    It.IsAny<EventId>(), 
                    It.Is<It.IsAnyType>((v, t) => message == null || v.ToString() == message), 
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)
                ), times, failMessage);
        }

        public static string FormatLogValues(string namedFormat, params object[] values)
        {
            var (num, indexedFormat) = GetIndexedFormat(namedFormat);

            var formattedValues = values.Select(v => FormatArgument(v)).ToArray();

            return string.Format(indexedFormat, formattedValues);
        }

        private static (int, string) GetIndexedFormat(string format)
        {
            var formatDelimiters = new[] { ',', ':' };
            var valueNames = new List<string>();

            var sb = new StringBuilder();
            var scanIndex = 0;
            var endIndex = format.Length;

            while (scanIndex < endIndex)
            {
                var openBraceIndex = FindBraceIndex(format, '{', scanIndex, endIndex);
                var closeBraceIndex = FindBraceIndex(format, '}', openBraceIndex, endIndex);

                if (closeBraceIndex == endIndex)
                {
                    sb.Append(format, scanIndex, endIndex - scanIndex);
                    scanIndex = endIndex;
                }
                else
                {
                    // Format item syntax : { index[,alignment][ :formatString] }.
                    var formatDelimiterIndex = FindIndexOfAny(format, formatDelimiters, openBraceIndex, closeBraceIndex);

                    sb.Append(format, scanIndex, openBraceIndex - scanIndex + 1);
                    sb.Append(valueNames.Count.ToString(CultureInfo.InvariantCulture));
                    valueNames.Add(format.Substring(openBraceIndex + 1, formatDelimiterIndex - openBraceIndex - 1));
                    sb.Append(format, formatDelimiterIndex, closeBraceIndex - formatDelimiterIndex + 1);

                    scanIndex = closeBraceIndex + 1;
                }
            }

            return (valueNames.Count, sb.ToString());
        }

        private static int FindBraceIndex(string format, char brace, int startIndex, int endIndex)
        {
            // Example: {{prefix{{{Argument}}}suffix}}.
            var braceIndex = endIndex;
            var scanIndex = startIndex;
            var braceOccurenceCount = 0;

            while (scanIndex < endIndex)
            {
                if (braceOccurenceCount > 0 && format[scanIndex] != brace)
                {
                    if (braceOccurenceCount % 2 == 0)
                    {
                        // Even number of '{' or '}' found. Proceed search with next occurence of '{' or '}'.
                        braceOccurenceCount = 0;
                        braceIndex = endIndex;
                    }
                    else
                    {
                        // An unescaped '{' or '}' found.
                        break;
                    }
                }
                else if (format[scanIndex] == brace)
                {
                    if (brace == '}')
                    {
                        if (braceOccurenceCount == 0)
                        {
                            // For '}' pick the first occurence.
                            braceIndex = scanIndex;
                        }
                    }
                    else
                    {
                        // For '{' pick the last occurence.
                        braceIndex = scanIndex;
                    }

                    braceOccurenceCount++;
                }

                scanIndex++;
            }

            return braceIndex;
        }

        private static int FindIndexOfAny(string format, char[] chars, int startIndex, int endIndex)
        {
            var findIndex = format.IndexOfAny(chars, startIndex, endIndex - startIndex);
            return findIndex == -1 ? endIndex : findIndex;
        }

        private static object FormatArgument(object value)
        {
            if (value == null)
            {
                return "(null)";
            }

            // since 'string' implements IEnumerable, special case it
            if (value is string)
            {
                return value;
            }

            // if the value implements IEnumerable, build a comma separated string.
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                return string.Join(", ", enumerable.Cast<object>().Select(o => o ?? "(null)"));
            }

            return value;
        }
    }
}
