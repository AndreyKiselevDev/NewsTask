using NewsTask.Data.Models;
using System.Collections.Generic;

namespace NewsTask.Data.Repositories
{
    public interface ICollectionRepository
    {
        int CreateCollection(string title);

        bool AddSourceToCollection(int sourceId, int collectionId);

        List<SourceCollection> GetSourcesForCollection(int collectionId);

        Source GetSourceById(int sourceId);

        List<string> GetAllSources();
    }
}
