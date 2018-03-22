﻿using System;
using Common.Domain.Models;

namespace Authentication.Domain.Models
{
    public class Identity : Entity
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public Guid SecurityStamp { get; set; }
        public string AuthenticationId { get; set; }
    }
}
