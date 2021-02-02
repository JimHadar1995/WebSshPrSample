using System;

namespace Library.Common.Exceptions
{
    /// <summary>
    /// Исклюдчение, возникающее в случае, если сущность не найдена
    /// </summary>
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException()
        :base("")
        {

        }
        public EntityNotFoundException(string? message) 
            : base(message)
        {
        }

        public EntityNotFoundException(string? message, Exception? innerException) 
            : base(message, innerException)
        {
        }
    }
}
