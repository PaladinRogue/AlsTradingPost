namespace Authorisation.Application.Restrictions
{
    public class RestrictionResult : IRestrictionResult
    {
        private RestrictionResult(bool success)
        {
            Succeeded = success;
        }

        public bool Succeeded { get; }

        public static IRestrictionResult Succeed => new RestrictionResult(true);

        public static IRestrictionResult Fail => new RestrictionResult(false);

    }
}