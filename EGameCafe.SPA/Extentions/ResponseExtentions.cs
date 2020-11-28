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

        public static async Task<Result> HandleGetMethodErrors(this HttpResponseMessage responseMessage)
        {
            try
            {
                if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return CommonResults.UnauthorizedResult("Unauthorized", "دوبازه لاگین کنید");
                }

                else if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    return CommonResults.ForbiddenResult("Forbidden", "شما دسترسی به همه امکانات را ندارید");
                }

                var responseBody = await responseMessage.Content.ReadAsStringAsync();

                if (responseBody != null)
                {
                    return JsonConvert.DeserializeObject<Result>(responseBody);
                }
                //else if (responseBody == null && responseMessage.StatusCode == HttpStatusCode.NotFound)
                //{
                //    return CommonResults.NotFoundResult("Not Found", "یافت نشد");
                //}

                return CommonResults.BadRequestResult("unknown", "خطا");
            }
            catch (Exception)
            {
                return CommonResults.ConnectionLostResult("Network problem", "ارتباط شما با سرور قطع شده است");
            }
        }


    }
}
