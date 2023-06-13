using Tildes.Net.Lib.Models;

namespace Tildes.Net.Lib.Abstraction;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsAsync();
    Task<IEnumerable<Post>> GetPostsBeforeAsync(string id);
    Task<IEnumerable<Post>> GetPostsAfterAsync(string id);
}