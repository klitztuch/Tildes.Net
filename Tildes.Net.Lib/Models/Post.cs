namespace Tildes.Net.Lib.Models;

public class Post
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public PostMetadata? Metadata { get; set; }
    public string[]? Text { get; set; }
    public string? TextExcerpt { get; set; }
    public PostInfo? Info { get; set; }
}