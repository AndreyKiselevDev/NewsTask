using NewsTask.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsTask.Data.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        NewsContext _newsContext;

        public CollectionRepository(NewsContext newsContext)
        {
            _newsContext = newsContext;
        }

        public int CreateCollection(string title)
        {
            if (_newsContext.Collections.FirstOrDefault(e => e.Title == title) != null)
                return 0;

            _newsContext.Collections.Add(new Collection() { Title = title });

            _newsContext.SaveChanges();

            return _newsContext.Collections.Single(e => e.Title == title).Id;
        }

        public bool AddSourceToCollection(int sourceId, int collectionId)
        {
            if (_newsContext.Sources.FirstOrDefault(e => e.Id == sourceId) == null ||
                _newsContext.Collections.FirstOrDefault(e => e.Id == collectionId) == null ||
                _newsContext.SourceCollections.FirstOrDefault(e => e.SourceId == sourceId && e.CollectionId == collectionId) != null)
                return false;

            _newsContext.SourceCollections.Add(new SourceCollection()
            {
                SourceId = sourceId,
                CollectionId = collectionId
            });

            _newsContext.SaveChanges();

            return true;
        }

        public List<SourceCollection> GetSourcesForCollection(int collectionId)
        {
            return _newsContext.SourceCollections.Where(e => e.CollectionId == collectionId).ToList();
        }

        public Source GetSourceById(int sourceId)
        {
            return _newsContext.Sources.FirstOrDefault(e => e.Id == sourceId);
        }

        public List<string> GetAllSources()
        {
            return _newsContext.Sources.Select(e => e.Id + " - " + e.Title).ToList(); 
        }
    }
}
