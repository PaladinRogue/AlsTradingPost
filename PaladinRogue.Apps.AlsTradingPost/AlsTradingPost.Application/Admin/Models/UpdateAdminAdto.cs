using System;
using Common.Application.Models;

namespace AlsTradingPost.Application.Admin.Models
{
    public class UpdateAdminAdto : InboundVersionedAdto
    {
        public Guid Id { get; set; }
    }
}