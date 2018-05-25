namespace Common.Application.Authorisation
{
    public class AlwaysDenyAuthorisationService : IAuthorisationService
    {
        public bool HasAccess(IAuthorisationRule authorisationRule)
        {
            return false;
        }

        public void DemandAccess(IAuthorisationRule authorisationRule)
        {
            throw new AuthorisationDeniedException();
        }
    }
}
