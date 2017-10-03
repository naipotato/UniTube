using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using UniTube.Core.Responses;

using Windows.UI.Xaml.Controls;

namespace UniTube.Core.Requests
{
    public class ChannelListRequest : Request
    {
        /// <summary>
        /// Especifica una lista separada por comas de una o más propiedades de recursos de channel que la respuesta de API va a incluir.
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// Especifica una categoría de guía YouTube para solicitar los canales de YouTube asociados con esa categoría.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Especifica un nombre de usuario de YouTube para solicitar el canal asociado con ese nombre de usuario.
        /// </summary>
        public string ForUsername { get; set; }

        /// <summary>
        /// Especifica una lista separada por comas de ID de canal de YouTube para los recursos que se están recuperando.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Establece el valor de este parámetro en true para indicarle a la API que muestre solo canales administrados por el propietario de contenido que el parámetro onBehalfOfContentOwner especifica.
        /// </summary>
        public bool? ManagedByMe { get; set; }

        /// <summary>
        /// Establece el valor de este parámetro en true para indicarle a la API que muestre solo los canales que pertenecen al usuario autenticado.
        /// </summary>
        public bool? Mine { get; set; }

        /// <summary>
        /// Especifica el número máximo de elementos que se deben mostrar en el conjunto de resultados.
        /// </summary>
        public uint? MaxResults { get; set; }

        /// <summary>
        /// Indica que las credenciales de autorización de la solicitud identifican a un usuario de CMS de YouTube que actúa en nombre del propietario de contenido especificado en el valor del parámetro.
        /// </summary>
        public string OnBehalfOfContentOwner { get; set; }

        /// <summary>
        /// Identifica una página específica en el conjunto de resultados que se debe mostrar.
        /// </summary>
        public string PageToken { get; set; }

        public async Task<ChannelListResponse> ExecuteAsync(ContentDialog errorHttpDialog = null)
        {
            string parameters = Part != null ? "part=" + Part + "&" : "";
            parameters += CategoryId != null ? "categoryId=" + CategoryId + "&" : "";
            parameters += ForUsername != null ? "forUsername=" + ForUsername + "&" : "";
            parameters += Id != null ? "id=" + Id + "&" : "";
            parameters += ManagedByMe != null ? "managedByMe=" + ManagedByMe + "&" : "";
            parameters += Mine != null ? "mine=" + Mine + "&" : "";
            parameters += MaxResults != null ? "maxResults=" + MaxResults + "&" : "";
            parameters += OnBehalfOfContentOwner != null ? "onBehalfOfContentOwner=" + OnBehalfOfContentOwner + "&" : "";
            parameters += PageToken != null ? "pageToken=" + PageToken + "&" : "";
            parameters += Access_Token != null ? "access_token=" + Access_Token + "&" : "";
            parameters += Callback != null ? "callback=" + Callback + "&" : "";
            parameters += Fields != null ? "fields=" + Fields + "&" : "";
            parameters += Key != null ? "key=" + Key + "&" : "";
            parameters += PrettyPrint != null ? "prettyPrint=" + PrettyPrint + "&" : "";
            parameters += QuotaUser != null ? "quotaUser=" + QuotaUser + "&" : "";
            parameters += UserIp != null ? "userIp=" + UserIp + "&" : "";
            parameters = parameters.Remove(parameters.Length - 1);

            var httpClient = new HttpClient();

            try
            {
                var httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/channels?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    string contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var channelListResponse = JsonConvert.DeserializeObject<ChannelListResponse>(contentResponse);
                    return channelListResponse;
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
