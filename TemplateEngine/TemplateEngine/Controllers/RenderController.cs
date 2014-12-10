using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TemplateEngine.Controllers
{
    public class RenderController : ApiController
    {
        public string Get()
        {
            return "fake result";
        }
    }
}
