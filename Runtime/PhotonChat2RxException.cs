using System;
using System.Runtime.Serialization;

namespace PhotonChat2Rx
{
    public class PhotonChat2RxException : Exception
    {
        public static PhotonChat2RxException Create(short errorCode, string message)
        {
            return new PhotonChat2RxException(message) { ErrorCode = errorCode };
        }

        public short ErrorCode { get; private set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(ErrorCode)}: {ErrorCode}";
        }

        public PhotonChat2RxException() : base()
        {
        }

        public PhotonChat2RxException(string message) : base(message)
        {
        }

        public PhotonChat2RxException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PhotonChat2RxException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

