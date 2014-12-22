using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Common
{
    public static class DictionaryExtensions
    {
        public static ExpandoObject ToExpandoObject(this IDictionary<string, object> dict)
        {
            var expando = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)expando;

            foreach (var kvp in dict)
            {
                // if value is another dictionary, recursively add this value to expandoDict
                if (kvp.Value is IDictionary<string, object>)
                {
                    var childNode = (IDictionary<string, object>)kvp.Value;
                    expandoDict.Add(kvp.Key, childNode.ToExpandoObject());
                }
                else if (kvp.Value is ICollection)
                {
                    // if value is a collection
                    var collection = (ICollection)kvp.Value;
                    var list = new List<object>();

                    foreach (var item in collection)
                    {
                        if (item is JObject)
                        {
                            
                        }
                    }
                }
                else
                {
                    expandoDict.Add(kvp);
                }
            }
            return expando;
        }
    }
}
