using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Pokedex.PokeApi.EndPoints
{
    public class NamedApiResourceList
    {
        [JsonProperty("count")]
        public int Count;

        [JsonProperty("next")]
        public Uri Next;

        [JsonProperty("previous")]
        public Uri Previous;

        [JsonProperty("results")]
        public List<NamedApiResource> Results;
    }

}
