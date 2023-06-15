using HtmlAgilityPack;

namespace Tildes.Net.Lib.Abstraction;

public interface IHtmlService
{
    Task<HtmlDocument?> GetHtmlDocumentAsync(string path);
}