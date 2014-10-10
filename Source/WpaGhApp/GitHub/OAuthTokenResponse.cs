using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Github
{
    // https://developer.github.com/v3/oauth/
    public class OAuthTokenResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string scope { get; set; }
    }
}
