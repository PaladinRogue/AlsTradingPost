﻿using System;
using AlsTradingPost.Resources;
using Common.Application.Concurrency;

namespace AlsTradingPost.Application.User.Models
{
    public class UserAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }

        public Persona Personas { get; set; }
    }
}