using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TomyChimmy
{
    //The source of this code is: https://www.talkingdotnet.com/store-complex-objects-in-asp-net-core-session/
    //Make staticso that we can call the methods without creating an instance first
    public static class SessionExtention
    {
        public static void SetObject (this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
