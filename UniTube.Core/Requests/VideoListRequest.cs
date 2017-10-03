using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using UniTube.Core.Responses;

using Windows.UI.Xaml.Controls;

namespace UniTube.Core.Requests
{
    public class VideoListRequest : Request
    {
        /// <summary>
        /// Especifica una lista separada por comas de una o más propiedades de recursos de video que se incluirán en la respuesta de la API.
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// Identifica el gráfico que deseas recuperar.
        /// </summary>
        public string Chart { get; set; }

        /// <summary>
        /// Especifica una lista separada por comas de ID de video de YouTube para los recursos que se están recuperando.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Establece el valor de este parámetro en like o dislike para indicarle a la API que muestre solo videos calificados por el usuario autenticado con "Me gusta" o "No me gusta".
        /// </summary>
        public string MyRating { get; set; }

        /// <summary>
        /// Especifica el número máximo de elementos que se debe mostrar en el conjunto de resultados.
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

        /// <summary>
        /// Indica a la API que seleccione un gráfico de video disponible en la región especificada.
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Identifica la categoría de video para la cual se debe recuperar el gráfico.
        /// </summary>
        public string VideoCategoryId { get; set; }

        public async Task<VideoListResponse> ExecuteAsync(ContentDialog errorHttpDialog = null)
        {
            string parameters = Part != null ? "part=" + Part + "&" : "";
            parameters += Chart != null ? "chart=" + Chart + "&" : "";
            parameters += Id != null ? "id=" + Id + "&" : "";
            parameters += MyRating != null ? "myRating=" + MyRating + "&" : "";
            parameters += MaxResults != null ? "maxResults=" + MaxResults + "&" : "";
            parameters += OnBehalfOfContentOwner != null ? "onBehalfOfContentOwner=" + OnBehalfOfContentOwner + "&" : "";
            parameters += PageToken != null ? "pageToken=" + PageToken + "&" : "";
            parameters += RegionCode != null ? "regionCode=" + RegionCode + "&" : "";
            parameters += VideoCategoryId != null ? "videoCategoryId=" + VideoCategoryId + "&" : "";
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
                var httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/videos?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    string contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var videoListResponse = JsonConvert.DeserializeObject<VideoListResponse>(contentResponse);
                    return videoListResponse;
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