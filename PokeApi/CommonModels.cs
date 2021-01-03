using System;
using Newtonsoft.Json;

namespace PokeApi
{
    public class ApiResource
    {
        [JsonProperty("url")] public Uri Url;
    }

    public class NamedApiResource
    {
        [JsonProperty("name")] public string Name;

        [JsonProperty("url")] public Uri Url;

        public override string ToString()
        {
            return Name;
        }
    }

    public class Name
    {
        [JsonProperty("language")] public NamedApiResource Language;

        [JsonProperty("name")] public string Text;
    }

    public class FlavorText
    {
        [JsonProperty("language")] public NamedApiResource Language;

        [JsonProperty("flavor_text")] public string Text;

        [JsonProperty("version")] public NamedApiResource Version;
    }

    public class Description
    {
        [JsonProperty("language")] public NamedApiResource Language;

        [JsonProperty("description")] public string Text;
    }
}