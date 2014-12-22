using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
            string result = Razor.Parse(renderingData.Template, renderingData.Model);
            return Ok(result);
        }
    }
}
