﻿using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.Models
{
    public class User : Entity
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}
