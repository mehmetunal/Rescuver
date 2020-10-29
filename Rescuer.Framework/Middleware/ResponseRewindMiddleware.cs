using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Rescuer.Framework.Middleware
{
    public class ResponseRewindMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            try
            {
                var buffer = await FormatResponse(context);
                await using var output = new MemoryStream(buffer);
                await output.CopyToAsync(originalBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private async Task<byte[]> FormatResponse(HttpContext context)
        {
            string responseBody = null;
            await using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;
                await _next(context);
                memStream.Position = 0;
                responseBody = await new StreamReader(memStream).ReadToEndAsync();
            }

            var json = ResponseJson(responseBody);
            var buffer = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(json));
            return buffer;
        }

        private static object ResponseJson(string responseBody)
        {
            var data = JsonConvert.DeserializeObject(responseBody);
            var json = new
            {
                data = data,
                apiVersion = "1.2",
                otherInfoHere = "here",
                systemError = "",
                message = "",
            };
            return json;
        }
    }
}