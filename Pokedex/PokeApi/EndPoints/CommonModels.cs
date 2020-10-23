using Newtonsoft.Json;
using System;

namespace Pokedex.PokeApi.EndPoints
{
    public class ApiResource
    {
        [JsonProperty("url")]
        public Uri Url;
    }

    public class NamedApiResource
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
        public string Text;

        [JsonProperty("language")]
        public NamedApiResource Language;
    }

    public class FlavorText
    {
        [JsonProperty("flavor_text")]
        public string Text;

        [JsonProperty("language")]
        public NamedApiResource Language;

        [JsonProperty("version")]
        public NamedApiResource Version;
    }

    public class Description
    {
        [JsonProperty("description")]
        public string Text;

        [JsonProperty("language")]
        public NamedApiResource Language;
    }
}
