namespace NewsTask.Data.Models
{
    public class SourceCollection
    {
        public int SourceId { get; set; }

        public Source Source { get; set; }

        public int CollectionId { get; set; }

        public Collection Collection { get; set; }
    }
}
