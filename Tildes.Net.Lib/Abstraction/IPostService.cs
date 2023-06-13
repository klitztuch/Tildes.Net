using Tildes.Net.Lib.Models;

namespace Tildes.Net.Lib.Abstraction;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsAsync(DateTime? from = null);
}