using EGameCafe.SPA.AccountServices.Services;
using EGameCafe.SPA.Data;
using EGameCafe.SPA.Extentions;
using Laboratory.Client.SPA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ICurrentUserService _currentUser;

        public Repository(HttpClient httpClient, ICurrentUserService currentUser)
        {
            _httpClient = httpClient;
            _currentUser = currentUser;
        }

        public async Task<Result> AuthorizePostAsync(T command, string rout)
        {
            try
            {
                var token = await _currentUser.GetAuthToken();

                var modelAsJson = JsonConvert.SerializeObject(command);

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, rout);

                requestMessage.Headers.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                requestMessage.Content = new StringContent(modelAsJson);

                requestMessage.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                return response.DeserializeResponseMessageStatus().Result;
            }
            catch (Exception)
            {
                return CommonResults.InternalServerError("Internal Server Error", "سرور در حال بارگذاری می باشد");
            }
        }
    }
}
