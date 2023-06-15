using Tildes.Net.Lib.Models;

namespace Tildes.Net.Lib.Abstraction;

public interface ITopicService
{
    Task<IEnumerable<Post>> GetPostsAsync(string topic);
    Task<IEnumerable<Post>> GetPostsBeforeAsync(string topic,
        string id);
    Task<IEnumerable<Post>> GetPostsAfterAsync(string topic,
        string id);
}