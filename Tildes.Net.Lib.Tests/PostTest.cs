using FluentAssertions;

namespace Tildes.Net.Lib.Tests;

public class PostTest
{
    private readonly PostService _postService;

    public PostTest()
    {
        _postService = new PostService("https://tildes.net");
    }
    [Fact]
    public async void GetPostsTest()
    {
        // arrange
        
        // act
        var posts = await _postService.GetPostsAsync();
        // assert
        posts.Should().NotBeNull();
        posts.Should().HaveCountGreaterOrEqualTo(1);
    }
}