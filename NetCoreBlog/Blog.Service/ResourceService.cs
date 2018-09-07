using Blog.IService;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.IRepository;

namespace Blog.Service
{
    public class ResourceService : Service<Resource>, IResourceService
    {
        public ResourceService(IResourceRepository rep) : base(rep)
        {
        }
    }
}
