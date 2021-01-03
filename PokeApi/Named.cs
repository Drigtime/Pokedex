using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeApi
{
    public class NamedApiResourceList
    {
        [JsonProperty("count")] public int Count;

        [JsonProperty("next")] public Uri Next;

        [JsonProperty("previous")] public Uri Previous;

        [JsonProperty("results")] public List<NamedApiResource> Results;
    }
}