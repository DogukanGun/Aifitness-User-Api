using System;

namespace Aifitness_User_Api.Service.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() { }
        public BusinessException(string? message) : base(message) { }
        public BusinessException(string? message, Exception? innerException) : base(message, innerException) { }

        public int Status { get; set; } = 400;
        public object Value
        {
            get
            {
                return this.Message ?? "Bir hata oluştu";
            }
        }
    }
}
