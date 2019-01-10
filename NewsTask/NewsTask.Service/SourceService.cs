using System;
using System.Net.Http;
using NewsTask.Service.Models;
using Microsoft.Extensions.Caching.Memory;
using NewsTask.Service.Extensions;
using NewsTask.Data.Repositories;

namespace NewsTask.Service
{
    public class SourceService : ISourceService
    {
        IMemoryCache _memoryCache;
        ICollectionRepository _collectionRepository;

        public SourceService(IMemoryCache memoryCache, ICollectionRepository collectionRepository)
        {
            _memoryCache = memoryCache;
            _collectionRepository = collectionRepository;
        }

        public rss GetNewsFromSource(int sourceId)
        {
            return _memoryCache.GetOrCreate(sourceId, entry =>
            {
                var source = _collectionRepository.GetSourceById(sourceId);

                if (source == null)
                    return null;

                using (var client = new HttpClient())
                {
                    try
                    {
                        var resultString = client.GetStringAsync(source.Url).Result;
                        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                        return XmlParser.Deserialize<rss>(resultString);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            });
        }
    }
}
