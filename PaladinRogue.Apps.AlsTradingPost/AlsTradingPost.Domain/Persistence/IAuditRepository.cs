﻿using AlsTradingPost.Domain.Models;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IAuditRepository
    {
        void Add(Audit audit);
    }
}