using Blog.IService;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.IRepository;

namespace Blog.Service
{
    public class BlogAnnouncementService : Service<BlogAnnouncement>, IBlogAnnouncementService
    {
        public BlogAnnouncementService(IBlogAnnouncementRepository rep) : base(rep)
        {
        }
    }
}
