﻿using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder : IBuilder<string, object>
    {
        IResourceBuilder Create(IResource resource);
        IResourceBuilder WithResourceMeta();
    }
}