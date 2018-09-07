using Blog.IService;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.IRepository;

namespace Blog.Service
{
    public class TimeLineService : Service<TimeLine>, ITimeLineService
    {
        public TimeLineService(ITimeLineRepository rep) : base(rep)
        {
        }
    }
}
