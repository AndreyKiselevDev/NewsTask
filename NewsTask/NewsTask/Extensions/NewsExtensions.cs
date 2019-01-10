using NewsTask.Api.Models;
using NewsTask.Service.Models;
using System.Collections.Generic;

namespace NewsTask.Api.Extensions
{
    public static class NewsExtensions
    {
        public static List<NewsResponseModel> ParseRSSList(this List<rss> model)
        {
            var result = new List<NewsResponseModel>();

            if (model.Count > 0)
            {
                foreach (var item in model)
                    result.Add(item.ParseRSS());

                return result;
            }

            return null;
        }

        public static NewsResponseModel ParseRSS(this rss model)
        {
            var result = new NewsResponseModel();

            foreach (var item in model.channel.Any)
            {
                switch (item.Name)
                {
                    case "title":
                        {
                            result.Title = item.InnerText;
                            break;
                        }
                    case "link":
                        {
                            result.Link = item.InnerText;
                            break;
                        }
                    case "description":
                        {
                            result.SourceDescription = item.InnerText;
                            break;
                        }
                    case "lastBuildDate":
                        {
                            result.LastBuildDate = item.InnerText;
                            break;
                        }
                    case "item":
                        {
                            var pieceOfNews = new PieceOfNews();

                            for (int i = 0; i < item.ChildNodes.Count; i++)
                            {
                                string value = item.ChildNodes[i].InnerText;

                                switch (item.ChildNodes[i].Name)
                                {
                                    case "title":
                                        {
                                            pieceOfNews.Title = value;
                                            break;
                                        }
                                    case "description":
                                        {
                                            pieceOfNews.Description = value;
                                            break;
                                        }
                                    case "pubDate":
                                        {
                                            pieceOfNews.PublishDate = value;
                                            break;
                                        }
                                    case "link":
                                        {
                                            pieceOfNews.Link = value;
                                            break;
                                        }
                                }
                            }
                            
                            result.News.Add(pieceOfNews);

                            break;
                        }
                }

            }

            return result;
        }

    }
}
