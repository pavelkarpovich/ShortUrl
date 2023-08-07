namespace ShortUrl.ApplicationCore.Entities
{
    public class AliasUrl
    {
        public AliasUrl(string? alias, string url)
        {
            Alias = alias;
            Url = url;
        }

        public AliasUrl(string? alias, string url, string? userId)
        {
            Alias = alias;
            Url = url;
            UserId = userId;
        }

        public int Id { get; set; }
        public string? Alias { get; set; }
        public string Url { get; set; }
        public string? UserId { get; set; }
    }
}