using Microsoft.AspNetCore.Http;

namespace AspNetCoreSerilogExample.Web.Extensions
{
    public static class IntExtensions
    {
        public static int ToHTTPStatusCode(this int input)
        {
            switch (input)
            {
                case 200:
                    return StatusCodes.Status200OK;

                case 400:
                    return StatusCodes.Status400BadRequest;

                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}