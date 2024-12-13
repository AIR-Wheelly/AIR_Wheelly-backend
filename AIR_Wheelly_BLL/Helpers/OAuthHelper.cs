using AIR_Wheelly_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIR_Wheelly_BLL.Helpers
{
    public static class OAuthHelper
    {
        public static async Task<OAuthReponseUserModel?> ValidateToken(string token)
        {
            using HttpClient http = new();
            var response = await http.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={token}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var oauthUser = JsonSerializer.Deserialize<OAuthReponseUserModel>(responseContent);

            return oauthUser;
        }
    }
}
