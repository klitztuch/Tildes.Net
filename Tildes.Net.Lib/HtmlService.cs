using HtmlAgilityPack;
using Tildes.Net.Lib.Abstraction;

namespace Tildes.Net.Lib;

public class HtmlService : IHtmlService
{
    private readonly string _baseUrl;

    public HtmlService(string baseUrl)
    {
        _baseUrl = baseUrl;
    }
    public async Task<HtmlDocument?> GetHtmlDocumentAsync(string path)
    {
        var url = Path.Combine(_baseUrl, path);
        var web = new HtmlWeb();
        return await web.LoadFromWebAsync(url);
    }
}