using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TemplateEngine.Models;

namespace TemplateEngine.Formatters
{
    public class RenderingDataMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public RenderingDataMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Clear();
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.templateengine+json"));
        }

    }
}
