using FluentAssertions;

namespace Tildes.Net.Lib.Tests;

public class TopicServiceTests
{
    private readonly TopicService _topicService;

    public TopicServiceTests()
    {
        var htmlService = new HtmlService("https://tildes.net");
        _topicService = new TopicService(htmlService);
    }
    [Fact]
    public async void GetPostsTest()
    {
        // arrange
        
        // act
        var posts = await _topicService.GetPostsAsync();
        // assert
        posts.Should().NotBeNull();
        posts.Should().HaveCountGreaterOrEqualTo(1);
    }
}