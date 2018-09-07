using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Blog.IRepository;

namespace Blog.Repository
{
    public class FriendslinksRepository : Repository<Friendslinks>, IFriendslinksRepository
    {
        public FriendslinksRepository(BlogDbContext db) : base(db)
        {
        }
    }
}
