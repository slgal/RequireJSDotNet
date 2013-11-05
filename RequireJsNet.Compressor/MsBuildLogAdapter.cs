﻿using System;

using EcmaScript.NET;

using Microsoft.Build.Utilities;

namespace RequireJsNet.Compressor
{
    public class MsBuildLogAdapter : ILog
    {
        private readonly TaskLoggingHelper logger;

        public void LogMessage(string message)
        {
            logger.LogMessage(message);
        }

        public void LogBoolean(string name, bool value)
        {
            LogMessage(name + ": " + (value ? "Yes" : "No"));
        }

        public void LogError(string message, params object[] messageArgs)
        {
            logger.LogError(message, messageArgs);
        }

        public void LogErrorFromException(Exception exception)
        {
            this.logger.LogErrorFromException(exception);
        }

        public void LogErrorFromException(Exception exception, bool showStackTrace)
        {
            this.logger.LogErrorFromException(exception, showStackTrace);
        }

        public MsBuildLogAdapter(TaskLoggingHelper logger)
        {
            this.logger = logger;
        }

        public void LogEcmaError(EcmaScriptException ecmaScriptException)
        {
            this.LogError("An error occurred in parsing the Javascript file.");
            if (ecmaScriptException.LineNumber == -1)
            {
                this.LogError("[ERROR] {0} ********", ecmaScriptException.Message);
            }
            else
            {
                this.LogError(
                    "[ERROR] {0} ******** Line: {2}. LineOffset: {3}. LineSource: \"{4}\"",
                    ecmaScriptException.Message,
                    string.IsNullOrEmpty(ecmaScriptException.SourceName)
                        ? string.Empty
                        : "Source: {1}. " + ecmaScriptException.SourceName,
                    ecmaScriptException.LineNumber,
                    ecmaScriptException.ColumnNumber,
                    ecmaScriptException.LineSource);
            }
        }
    }
}
