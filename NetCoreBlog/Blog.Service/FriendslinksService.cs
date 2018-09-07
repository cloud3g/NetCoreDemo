using Blog.IService;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.IRepository;
using Blog.Common;

namespace Blog.Service
{
    public class FriendslinksService : Service<Friendslinks>, IFriendslinksService
    {
        public FriendslinksService(IFriendslinksRepository rep) : base(rep)
        {
        }

      
    }
}
