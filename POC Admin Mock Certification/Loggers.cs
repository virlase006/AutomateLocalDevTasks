using Newtonsoft.Json;
using Stylelabs.M.Sdk.Contracts.Base;
using System;
using System.Collections.Generic;

namespace Mock.Implementation
{
    public enum LogType
    {
        VALIDATION, EXCEPTION, WARNING, INFO
    }

    public abstract class Logger : ILayoutBuilder
    {
        private static NLog.Logger MyLogger { get; set; }
        public LogType Type { get; set; }
        public string About { get; set; }
        public bool Passed { get; set; }
        public ICollection<string> Reason { get; set; }
        public string Message { get; set; }
        public Validation ExpectedConfiguration { get; set; }
        public void Trace() => GetLogger().Trace(GetValidationMessageWithLayout());
        public void Debug() => GetLogger().Debug(GetValidationMessageWithLayout());
        public void Info() => GetLogger().Info(GetValidationMessageWithLayout());
        public void Warn() => GetLogger().Warn(GetValidationMessageWithLayout());
        public void Error() => GetLogger().Error(GetValidationMessageWithLayout());
        public void Fatal() => GetLogger().Fatal(GetValidationMessageWithLayout());

        private static NLog.Logger GetLogger()
        {
            if (MyLogger == null)
            {
                MyLogger = NLog.LogManager.GetLogger("validation");
            }
            return MyLogger;
        }

        public abstract string GetValidationMessageWithLayout();
    }

    /// <summary>
    /// Logger for validating the Search Component Settings
    /// </summary>
    public class VLogger<T> : Logger where T : IMemberDefinition
    {
        public T ActualConfiguration { get; set; }
        private string[] GetParams()
        {
            return new string[]
            {
                DateTime.Now.ToString(),
                Type.ToString(),
                "{" + ExpectedConfiguration.PropertyName + "} - " + About,
                Passed ? "PASSED" : "FAILED",
                Passed ? "":JsonConvert.SerializeObject(Reason),
                Passed ? "": JsonConvert.SerializeObject(ExpectedConfiguration, Formatting.Indented),
                Passed ? "": JsonConvert.SerializeObject(ActualConfiguration, Formatting.Indented)
            };
        }

        public override string GetValidationMessageWithLayout()
        {
            string layout =
                 "____________________________{0}___________________________\n"
                + "Type:                             {1}\n"
                + "What:                             {2}\n"
                + "Result:                           {3}\n"
    + (Passed ? "" : "Misconfigured member attributes:  {4}\n")
    + (Passed ? "" : "Expected Configuration:           {5}\n")
    + (Passed ? "" : "Actual Configuration:             {6}\n\n");
            return string.Format(layout, GetParams());
        }
    }

    /// <summary>
    /// Logger for validating the Search Component Settings
    /// </summary>
    public class SCLogger : Logger
    {
        private string[] GetParams()
        {
            return new string[]
            {
                DateTime.Now.ToString(),
                Type.ToString(),
                "{" + ExpectedConfiguration.PropertyName + "} - " + About,
                Passed ? "PASSED" : "FAILED",
                Passed ? "":JsonConvert.SerializeObject(Reason),
                Message
            };
        }

        public override string GetValidationMessageWithLayout()
        {
            string layout =
                 "____________________________{0}___________________________\n"
                + "Type:                             {1}\n"
                + "What:                             {2}\n"
                + "Result:                           {3}\n"
  + (Passed ? "" : "Reason:                           {4}\n")
  + (Passed ? "" : "Message:                          {5}\n\n");
            return string.Format(layout, GetParams());
        }
    }

    public interface ILayoutBuilder
    {
        string GetValidationMessageWithLayout();
    }
}