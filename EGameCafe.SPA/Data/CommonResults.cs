using Laboratory.Client.SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Data
{
    public static class CommonResults
    {
        public static Result BadRequestResult(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", "https://tools.ietf.org/html/rfc7231#section-6.5.1", 400, "Bad Request");
        }

        public static Result InvalidTokenResult(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", "https://tools.ietf.org/html/rfc7231#section-6.6.1", 498, "Invalid Token");
        }

        public static Result ConnectionLostResult(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", " ", 0, "Connection lost");
        }

        public static Result UnauthorizedResult(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", "https://tools.ietf.org/html/rfc7235#section-3.1", 401, "Unauthorized ");
        }

        public static Result NotFoundResult(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", "https://tools.ietf.org/html/rfc7231#section-6.5.4", 404, "Not found");
        }

        public static Result InternalServerError(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", "https://tools.ietf.org/html/rfc7231#section-6.5.4", 500, "InternalServerError");
        }

        public static Result ForbiddenResult(string enErrorMessage, string faErrorMessage)
        {
            return Result.Failure(enErrorMessage, faErrorMessage, "Failure", "https://tools.ietf.org/html/rfc7235#section-3.1", 403, " Forbidden");
        }
    }
}
