using Common.Api.Resources;
using Common.Api.Validation.Attributes;

namespace ApplicationManager.Api.Identities
{
    public class CreatePasswordIdentityResource : IResource
    {
        public CreatePasswordIdentityResource()
        {
        }
        
        public CreatePasswordIdentityResource(string token)
        {
            Token = token;
        }
        
        [Required]
        public string Token { get; set; }
        
        public string Username { get; set; }
        
        [Required]
        [Length(6, 80)]
        public string Password { get; set; }

        [Required]
        [Length(6, 80)]
        public string ConfirmPassword { get; set; }
    }
}