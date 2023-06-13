using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Tildes.Net.Lib.Abstraction;
using Tildes.Net.Lib.Models;

namespace Tildes.Net.Lib;

public class PostService : IPostService
{
    private readonly string _baseUrl;

    public PostService(string baseUrl)
    {
        _baseUrl = baseUrl;
    }
    public async Task<IEnumerable<Post>> GetPostsAsync(DateTime? from = null)
    {
        var web = new HtmlWeb();

        var htmlDoc = web.Load(_baseUrl);
        var topicListing = htmlDoc.DocumentNode.QuerySelector(".topic-listing");

        var posts = new List<Post>();

        foreach (var article in topicListing.QuerySelectorAll("li > article"))
        {
            var id = article.Id;
            var title = article.QuerySelector(".topic-title > a")?.InnerText;
            var textExcerpt = article.QuerySelector(".topic-text-excerpt > summary > span")?.InnerText;
            var group = article.QuerySelector(".topic-metadata > .topic-group > a")?.InnerText;
            var contentType = article.QuerySelector(".topic-metadata > .topic-content-type")?.InnerText;

            var comments = article.QuerySelector(".topic-info > .topic-info-comments > a > span")?.InnerText;
            var timeStamp = article.QuerySelector(".topic-info time")?
                .GetAttributeValue("datetime", new DateTime());

            var name = article.QuerySelector(".topic-info > .topic-info-source > .link-user")?.InnerText;
            var link = article.QuerySelector(".topic-info > .topic-info-source > .link-user")?
                .GetAttributeValue("href", string.Empty);
            
            var post = new Post()
            {
                Id = id,
                Title = title,
                TextExcerpt = textExcerpt,
                Metadata = new PostMetadata()
                {
                    Group = group,
                    ContentType = contentType
                },
                Info = new PostInfo()
                {
                    Comments = comments,
                    TimeStamp = timeStamp,
                    User = new User()
                    {
                        Name = name,
                        Link = link
                    }
                }
            };
            posts.Add(post);
        }
        return posts;
    }
}