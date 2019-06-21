﻿using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.CheckPassword;
using Common.Domain.Models.PasswordProtection;
using Common.Resources;

namespace ApplicationManager.Domain.Identities
{
    public class PasswordIdentity : AuthenticationIdentity
    {
        private const byte MaskLength = 8;
        private readonly string _passwordMask = new string('*', MaskLength);

        protected PasswordIdentity()
        {
        }

        protected PasswordIdentity(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto)
        {
            Identity = identity;
            AuthenticationGrantTypePassword = authenticationGrantTypePassword;
            Identifier = addConfirmedPasswordIdentityDdto.Identifier;
            Password = addConfirmedPasswordIdentityDdto.Password;
        }

        internal static PasswordIdentity Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto)
        {
            return new PasswordIdentity(identity, authenticationGrantTypePassword, addConfirmedPasswordIdentityDdto);
        }

        [MaxLength(FieldSizes.Extended)]
        [Required]
        public string Identifier { get; protected set; }

        public string Password
        {
            get => _passwordMask;
            protected set => ProtectedPassword = PasswordProtection.Protect(value);
        }

        [Required]
        protected virtual ProtectedPassword ProtectedPassword { get; set; }

        public virtual AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }

        internal bool CheckPassword(CheckPasswordDdto checkPasswordDdto)
        {
            return ProtectedPassword == PasswordProtection.Protect(checkPasswordDdto.Password, ProtectedPassword.Salt);
        }

        internal void ChangePassword(ChangePasswordDdto changePasswordDdto)
        {
            ProtectedPassword = PasswordProtection.Protect(changePasswordDdto.Password);
        }
    }
}
