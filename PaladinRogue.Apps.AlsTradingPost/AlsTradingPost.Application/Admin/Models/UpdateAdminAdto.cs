using Common.Application.Models;

namespace AlsTradingPost.Application.Admin.Models
{
    public class UpdateAdminAdto : InboundVersionedAdto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}