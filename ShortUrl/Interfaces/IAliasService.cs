namespace ShortUrl.Web.Interfaces
{
    public interface IAliasService
    {
        Task<bool> IsAliasExist(string alias);
        Task AddAlias(string alias, string url);
        Task<string> GetUrl(string alias);
    }
}
