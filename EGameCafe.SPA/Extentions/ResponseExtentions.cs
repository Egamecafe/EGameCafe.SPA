using EGameCafe.SPA.Data;
using EGameCafe.SPA.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Extentions
{
    public static class ResponseExtentions
    {
        public static async Task<Result> DeserializeResponseMessageStatus(this HttpResponseMessage responseMessage)
        {
            try
            {
                var responseBody = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return CommonResults.UnauthorizedResult("Unauthorized", "دوباره لاگین کنید");
                }

                return JsonConvert.DeserializeObject<Result>(responseBody);
            }
            catch (Exception)
            {
                return CommonResults.ConnectionLostResult("Network problem", "ارتباط شما با سرور قطع شده است");
            }
        }


    }
}
