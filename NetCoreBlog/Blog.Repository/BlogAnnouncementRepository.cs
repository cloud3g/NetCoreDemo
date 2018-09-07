using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class BlogAnnouncementRepository : Repository<BlogAnnouncement>, IBlogAnnouncementRepository
    {
        public BlogAnnouncementRepository(BlogDbContext db) : base(db)
        {
        }
    }
}
