using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UniTube.Framework.Utils;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace UniTube.Core.Authentication
{
    public static class Authenticator
    {
        public static async Task<AuthResponse> AuthenticateAsync(ContentDialog userCancelDialog = null, ContentDialog errorHttpDialog = null)
        {
            WebAuthenticationResult authenticationResult = null;
            try
            {
                authenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.UseTitle,
                    new Uri(
                        $"{Client.AuthUri}?client_id={Client.ClientId}" +
                        $"&redirect_uri=urn:ietf:wg:oauth:2.0:oob&response_type=code&scope=https://www.googleapis.com/auth/youtube"),
                    new Uri("https://accounts.google.com/o/oauth2/approval"));
            }
            catch (FileNotFoundException)
            {
                await errorHttpDialog?.ShowAsync();
                return null;
            }

            var authorizationCode = string.Empty;

            switch (authenticationResult.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    authorizationCode = authenticationResult.ResponseData;
                    authorizationCode = authorizationCode.Substring(13);
                    break;
                case WebAuthenticationStatus.UserCancel:
                    await userCancelDialog?.ShowAsync();
                    return null;
                case WebAuthenticationStatus.ErrorHttp:
                    await errorHttpDialog?.ShowAsync();
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("code" , authorizationCode),
                new KeyValuePair<string, string>("client_id", Client.ClientId),
                new KeyValuePair<string, string>("client_secret", Client.ClientSecret),
                new KeyValuePair<string, string>("redirect_uri", Client.RedirectUris[0]),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            };

            var httpClient = new HttpClient();
            var httpContent = new HttpFormUrlEncodedContent(pairs);

            try
            {
                var httpResponse = await httpClient.PostAsync(Client.TokenUri, httpContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var authResponse = await JsonUtils.ToObjectAsync<AuthResponse>(contentResponse);
                    return authResponse;
                }
                else
                {
                    await errorHttpDialog?.ShowAsync();
                }
            }
            catch
            {
                await errorHttpDialog?.ShowAsync();
            }

            return null;
        }

        public static async Task<AuthResponse> RefreshAccesTokenAsync(string refreshToken, ContentDialog errorHttpDialog = null)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", Client.ClientId),
                new KeyValuePair<string, string>("client_secret", Client.ClientSecret),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token")
            };

            var httpClient = new HttpClient();
            var httpContent = new HttpFormUrlEncodedContent(pairs);

            try
            {
                var httpResponse = await httpClient.PostAsync(Client.TokenUri, httpContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var authResponse = await JsonUtils.ToObjectAsync<AuthResponse>(contentResponse);
                    return authResponse;
                }
                else
                {
                    await errorHttpDialog?.ShowAsync();
                }
            }
            catch
            {
                await errorHttpDialog?.ShowAsync();
            }

            return null;
        }
    }
}
