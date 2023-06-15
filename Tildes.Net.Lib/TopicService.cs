using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Tildes.Net.Lib.Abstraction;
using Tildes.Net.Lib.Models;

namespace Tildes.Net.Lib;

public class TopicService : ITopicService
{
    private readonly IHtmlService _htmlService;

    public TopicService(IHtmlService htmlService)
    {
        _htmlService = htmlService;
    }

    public IEnumerable<Post> GetPostsFromHtmlDocument(HtmlDocument htmlDocument)
    {
        var topicListing = htmlDocument.DocumentNode.QuerySelector(".topic-listing");

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
            
            var post = new Post
            {
                Id = id,
                Title = title,
                TextExcerpt = textExcerpt,
                Metadata = new PostMetadata
                {
                    Group = group,
                    ContentType = contentType
                },
                Info = new PostInfo
                {
                    Comments = comments,
                    TimeStamp = timeStamp,
                    User = new User
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

    public async Task<IEnumerable<Post>> GetPostsAsync(string? topic = null)
    {
        var topicPath = BuildTopicPath(topic);
        var htmlDoc = await _htmlService.GetHtmlDocumentAsync(topicPath);
        return htmlDoc == null ? Array.Empty<Post>() : GetPostsFromHtmlDocument(htmlDoc);
    }

    public async Task<IEnumerable<Post>> GetPostsBeforeAsync(string topic, string id)
    {
        var queryParameter = $"after={id}";
        var topicPath = BuildTopicPath(topic, queryParameter);
        var htmlDoc = await _htmlService.GetHtmlDocumentAsync(topicPath);
        return htmlDoc == null ? Array.Empty<Post>() : GetPostsFromHtmlDocument(htmlDoc);
    }

    public async Task<IEnumerable<Post>> GetPostsAfterAsync(string topic, string id)
    {
        var queryParameter = $"before={id}";
        var topicPath = BuildTopicPath(topic, queryParameter);
        var htmlDoc = await _htmlService.GetHtmlDocumentAsync(topicPath);
        return htmlDoc == null ? Array.Empty<Post>() : GetPostsFromHtmlDocument(htmlDoc);
    }

    public string GetTopicLink(string topic)
    {
        throw new NotImplementedException();
    }

    private string BuildTopicPath(string? topic,
        string? queryParameter = null)
    {
        var topicUrl = topic == null ? string.Empty : $"~{topic}";
        return queryParameter == null ? topicUrl : $"{topicUrl}?{queryParameter}";
    }
}