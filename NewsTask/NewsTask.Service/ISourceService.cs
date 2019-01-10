using NewsTask.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsTask.Service
{
    public interface ISourceService
    {
        rss GetNewsFromSource(int sourceId);
    }
}
