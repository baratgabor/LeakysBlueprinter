using System;

namespace LeakysBlueprinter.Model.Exceptions
{
    public class AppException : Exception
    {
        public ExceptionKind ExceptionKind { get; }

        public AppException(ExceptionKind exceptionKind) : base(exceptionKind.ToString())
            => ExceptionKind = exceptionKind;

        public AppException(ExceptionKind exceptionKind, string message) : base(message)
            => ExceptionKind = exceptionKind;

        public AppException(ExceptionKind exceptionKind, Exception inner) : base(exceptionKind.ToString(), inner)
            => ExceptionKind = exceptionKind;

        public AppException(ExceptionKind exceptionKind, string message, Exception inner) : base(message, inner)
            => ExceptionKind = exceptionKind;
    }
}
