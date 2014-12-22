using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Common;
using RazorEngine;
using TemplateEngine.Models;
using TemplateEngine.Security;

namespace TemplateEngine.Controllers
{
    [Authorize]
    public class RenderController : ApiController
    {
        public IHttpActionResult Post([FromBody]RenderingData renderingData)
        {
            if (renderingData == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("rendering data is null"),
                    ReasonPhrase = "The Http request body is either empty or invalid."
                };
                throw new HttpResponseException(response);
            }

            if (string.IsNullOrWhiteSpace(renderingData.Template))
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No template provided"),
                    ReasonPhrase = "The Http request body is either empty or invalid."
                };
                throw new HttpResponseException(response);
            }

            if (string.IsNullOrWhiteSpace(renderingData.Template))
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No Model provided"),
                    ReasonPhrase = "The Http request body is either empty or invalid."
                };
                throw new HttpResponseException(response);
            }

            string result = Razor.Parse(renderingData.Template, renderingData.Model);
            return Ok(result);
        }
    }
}
