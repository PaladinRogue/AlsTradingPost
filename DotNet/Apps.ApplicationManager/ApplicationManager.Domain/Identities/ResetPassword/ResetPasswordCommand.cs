using System.Threading.Tasks;
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

        public async Task<Identity> ExecuteAsync(ResetPasswordCommandDdto resetPasswordCommandDdto)
        {
            _validator.ValidateAndThrow(resetPasswordCommandDdto);

            Identity identity = await _getIdentityByForgotPasswordTokenQuery.RunAsync(resetPasswordCommandDdto.Token);

            if (identity == null)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            identity.ValidateToken(new ValidateTokenDdto
            {
                Token = resetPasswordCommandDdto.Token,
                TwoFactorAuthenticationType = TwoFactorAuthenticationType.ForgotPassword
            });

            identity.ResetPassword(new ResetPasswordDdto
            {
                Password = resetPasswordCommandDdto.Password,
                Token = resetPasswordCommandDdto.Token
            });

            return identity;
        }
    }
}