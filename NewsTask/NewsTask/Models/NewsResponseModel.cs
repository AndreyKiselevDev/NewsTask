using System.Collections.Generic;

namespace NewsTask.Api.Models
{
    public class NewsResponseModel
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string SourceDescription { get; set; }

        public string LastBuildDate { get; set; }

        public List<PieceOfNews> News { get; set; }

        public NewsResponseModel()
        {
            News = new List<PieceOfNews>();
        }
    }

    public class PieceOfNews
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string PublishDate { get; set; }

        public string Description { get; set; }
    }
}
