﻿using Common.Application.Interfaces;

namespace Common.Application.Models
{
    public class OutboundVersionedAdto : IOutboundVersionedAdto
    {
        public int Version { get; set; }
    }
}
