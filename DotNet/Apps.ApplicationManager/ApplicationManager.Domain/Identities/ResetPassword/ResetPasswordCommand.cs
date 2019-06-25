using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ResetPassword
{
    public class ResetPasswordCommand : IResetPasswordCommand
    {
        private readonly IValidator<ResetPasswordCommandDdto> _validator;

        private readonly IGetIdentityByForgotPasswordTokenQuery _getIdentityByForgotPasswordTokenQuery;

        public ResetPasswordCommand(
            IValidator<ResetPasswordCommandDdto> validator,
            IGetIdentityByForgotPasswordTokenQuery getIdentityByForgotPasswordTokenQuery)
        {
            _validator = validator;
            _getIdentityByForgotPasswordTokenQuery = getIdentityByForgotPasswordTokenQuery;
        }

        public Identity Execute(ResetPasswordCommandDdto resetPasswordCommandDdto)
        {
            _validator.ValidateAndThrow(resetPasswordCommandDdto);

            Identity identity = _getIdentityByForgotPasswordTokenQuery.Run(resetPasswordCommandDdto.Token);

            if (identity == null)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            identity.ResetPassword(new ResetPasswordDdto
            {
                Password = resetPasswordCommandDdto.Password,
                Token = resetPasswordCommandDdto.Token
            });

            return identity;
        }
    }
}