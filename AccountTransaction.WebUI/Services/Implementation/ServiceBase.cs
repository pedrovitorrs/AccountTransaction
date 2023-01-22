using AccountTransaction.Core.DTO;
using AccountTransaction.WebUI.Configuration.Exceptions;
using System.Text.Json;
using System.Text;

namespace AccountTransaction.WebUI.Services.Implementation
{
    public abstract class Service
    {
        protected StringContent GetContent(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool ManageResponseErrors(HttpResponseMessage Response)
        {
            switch ((int)Response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(Response.StatusCode);

                case 400:
                    return false;
            }

            Response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}
