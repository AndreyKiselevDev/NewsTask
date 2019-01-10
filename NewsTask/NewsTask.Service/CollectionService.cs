using NewsTask.Data.Repositories;
using NewsTask.Service.Models;
using System.Collections.Generic;

namespace NewsTask.Service
{
    public class CollectionService : ICollectionService
    {
        ISourceService _sourceService;
        ICollectionRepository _collectionRepository;

        public CollectionService(ISourceService sourceService, ICollectionRepository contextRepository)
        {
            _sourceService = sourceService;
            _collectionRepository = contextRepository;
        }

        public List<rss> GetCollectionNews(int collectionId)
        {
            var collectionsources = _collectionRepository.GetSourcesForCollection(collectionId);

            if (collectionsources.Count == 0)
                return null;

            var result = new List<rss>();

            foreach (var collectionsource in collectionsources)
                result.Add(_sourceService.GetNewsFromSource(collectionsource.SourceId));

            return result;
        }
    }
}
