namespace Common.ApplicationServices.Exceptions
{
    public enum ExceptionType
    {
        Unknown,
        ServiceUnavailable,
        BadRequest,
        Concurrency,
        Unauthorized,
        Conflict,
        NotFound
    }
}
