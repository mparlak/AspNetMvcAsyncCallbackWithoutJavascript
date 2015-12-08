using AspNetMvcAsyncCallbackWithoutJavascript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace AspNetMvcAsyncCallbackWithoutJavascript.Common
{
    public interface IProductClient
    {
        Task<ProductResponse> GetProduct(int productId);
    }

    public class ProductClient : ClientBase, IProductClient
    {
        private const string ProductUri = "api/post/notifications";

        public ProductClient(IApiClient apiClient)
            : base(apiClient)
        {
        }

        public async Task<ProductResponse> GetProduct(int productId)
        {
            var idPair = new KeyValuePair<string, string>("ids", productId.ToString());
            return await GetJsonDecodedContent<ProductResponse, ProductApiModel>(ProductUri, idPair);
        }
    }

    public abstract class ClientBase
    {
        private readonly IApiClient apiClient;

        protected ClientBase(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        protected async Task<TResponse> GetJsonDecodedContent<TResponse, TContentResponse>(string uri, params KeyValuePair<string, string>[] requestParameters) where TResponse : ApiResponse<TContentResponse>, new()
        {
            var apiResponse = await apiClient.GetFormEncodedContent(uri, requestParameters);
            var response = await CreateJsonResponse<TResponse>(apiResponse);
            response.Data = Json.Decode<TContentResponse>(response.ResponseResult);
            return response;
        }

        private static async Task<TResponse> CreateJsonResponse<TResponse>(HttpResponseMessage response) where TResponse : ApiResponse, new()
        {
            var clientResponse = new TResponse
            {
                StatusIsSuccessful = response.IsSuccessStatusCode,
                ResponseCode = response.StatusCode
            };
            if (response.Content != null)
            {
                clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
            }

            return clientResponse;
        }
    }
}
