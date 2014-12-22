using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TemplateEngine.Models
{
    public class RenderingData
    {
        /// <summary>
        /// Gets or sets string that represent the template to render
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of object to use when rendering the provided template.
        /// </summary>
        [JsonConverter(typeof(ExpandoObjectConverter))]
        public dynamic Model { get; set; }
    }
}
