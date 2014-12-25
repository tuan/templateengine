using Newtonsoft.Json;

namespace Common
{
    public static class ObjectExtensions
    {
        public static string PrettyPrint(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
