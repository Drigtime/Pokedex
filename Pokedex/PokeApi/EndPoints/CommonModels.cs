using Newtonsoft.Json;
using System;

namespace Pokedex.PokeApi.EndPoints
{
    public class APIResource
    {
        [JsonProperty("url")]
        public string Url;
    }

    public class NamedAPIResource
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("url")]
        public Uri Url;

        public override string ToString()
        {
            return Name;
        }
    }

    public class Name
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("language")]
        public NamedAPIResource Language;
    }

    public class FlavorText
    {
        [JsonProperty("flavor_text")]
        public string FlavorText;

        [JsonProperty("language")]
        public NamedAPIResource Language;

        [JsonProperty("version")]
        public NamedAPIResource Version;
    }

    public class Description
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("language")]
        public NamedAPIResource Language;
    }
}
