using System;
using System.Linq.Expressions;

namespace Common.Domain.Exceptions
{
    public class GetSingleDomainException<T> : DomainException
    {
        public GetSingleDomainException(Expression<Func<T, bool>> predicate)
            : base(_formatGetSingleException(predicate))
        {
        }

        public GetSingleDomainException(Expression<Func<T, bool>> predicate, Exception innerException)
            : base(_formatGetSingleException(predicate), innerException)
        {
        }

        private static string _formatGetSingleException(Expression<Func<T, bool>> predicate)
        {
            return $"Failed to get a single entity of type: { typeof(T).Name } matching predicate: { predicate }";
        }
    }
}
