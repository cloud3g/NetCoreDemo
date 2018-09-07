using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class TimeLineRepository : Repository<TimeLine>, ITimeLineRepository
    {
        public TimeLineRepository(BlogDbContext db) : base(db)
        {
        }
    }
}
