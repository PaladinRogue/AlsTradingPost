using System;
using Common.Application.Models;

namespace AlsTradingPost.Application.User.Models
{
    public class UpdateUserAdto : InboundVersionedAdto
    {
        public Guid Id { get; set; }
        public string KnownAs { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}