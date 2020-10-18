using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pokedex.PokeApi
{
    // And this is how you deserialize it in your C# code:
    // NamedAPIResourceList namedAPIResourceList = JsonConvert.DeserializeObject(myJsonResponse); 

    public class NamedAPIResource
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("url")]
        public Uri Url;
    }

    public class NamedAPIResourceList
    {
        [JsonProperty("count")]
        public int Count;

        [JsonProperty("next")]
        public Uri Next;

        [JsonProperty("previous")]
        public Uri Previous;

        [JsonProperty("results")]
        public List<NamedAPIResource> Results;
    }

}
