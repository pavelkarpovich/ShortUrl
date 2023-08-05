using ShortUrl.Web.Models;

namespace ShortUrl.Web.Interfaces
{
    public interface IAliasService
    {
        bool IsAliasExist(string alias);
        void AddAlias(string alias, string url, string userId);
        void DeleteAlias(string alias);
        string? GetUrl(string alias);
        Task<IEnumerable<AliasUrlModel>> GetMyUrls(string userId);
    }
}
