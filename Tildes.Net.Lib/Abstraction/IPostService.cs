using Tildes.Net.Lib.Models;

namespace Tildes.Net.Lib.Abstraction;

public interface IPostService
{
    Task<Post> GetPost(string id);
}