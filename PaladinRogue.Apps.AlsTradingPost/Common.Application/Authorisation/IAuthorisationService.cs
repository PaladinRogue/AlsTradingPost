namespace Common.Application.Authorisation
{
    public interface IAuthorisationService
    {
        bool HasAccess(IAuthorisationRule authorisationRule);

        /// <param name="authorisationRule"></param>
        /// <exception cref="AuthorisationDeniedException"></exception>
        void DemandAccess(IAuthorisationRule authorisationRule);
    }
}
