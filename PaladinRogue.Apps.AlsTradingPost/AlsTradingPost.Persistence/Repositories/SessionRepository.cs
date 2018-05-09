﻿using System;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.Persistence;
using Persistence.EntityFramework.Repositories;

namespace AlsTradingPost.Persistence.Repositories
{
    public class SessionRepository : ISessionRepository
	{
        private readonly AlsTradingPostDbContext _context;

        public SessionRepository(AlsTradingPostDbContext context)
        {
            _context = context;
        }
		
	    public Session GetById(Guid id)
	    {
		    return RepositoryHelper.GetById(_context.Sessions, id);
	    }

	    public void Add(Session entity)
	    {
		    RepositoryHelper.Add(_context.Sessions, _context, entity);
	    }

	    public void Update(Session entity)
	    {
		    RepositoryHelper.Update(_context.Sessions, _context, entity);
	    }
	}
}