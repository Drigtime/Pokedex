using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Pokedex.PokeApi.EndPoints
{
    // And this is how you deserialize it in your C# code:
    // NamedAPIResourceList namedAPIResourceList = JsonConvert.DeserializeObject(myJsonResponse); 

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
