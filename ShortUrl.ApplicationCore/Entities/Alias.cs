using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.ApplicationCore.Entities
{
    public class Alias
    {
        public Alias(string? aliasName, string? url)
        {
            AliasName = aliasName;
            Url = url;
        }

        public Alias(string? aliasName, string? url, string? userId)
        {
            AliasName = aliasName;
            Url = url;
            UserId = userId;
        }

        public int Id { get; set; }
        public string? AliasName { get; set; }
        public string? Url { get; set; }
        public string? UserId { get; set; }
    }
}