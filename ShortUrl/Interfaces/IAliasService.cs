using ShortUrl.Web.Models;

namespace ShortUrl.Web.Interfaces
{
    public interface IAliasService
    {
        bool IsAliasExist(string alias);
        void AddAlias(string alias, string url, string userId);
        string? GetUrl(string alias);
        Task<IEnumerable<AliasUrl>> GetMyUrls(string userId);
    }
}
