using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeApi
{
    public class LocationAreaEncounter
    {
        [JsonProperty("location_area")] public NamedApiResource LocationArea;

        [JsonProperty("version_details")] public List<VersionEncounterDetail> VersionDetails;

        public override string ToString()
        {
            return LocationArea.ToString();
        }
    }

    public class VersionEncounterDetail
    {
        [JsonProperty("encounter_details")] public List<Encounter> EncounterDetails;

        [JsonProperty("max_chance")] public int MaxChance;

        [JsonProperty("version")] public NamedApiResource Version;
    }

    public class Encounter
    {
        [JsonProperty("chance")] public int Chance;

        [JsonProperty("condition_values")] public List<NamedApiResource> ConditionValues;

        [JsonProperty("max_level")] public int MaxLevel;

        [JsonProperty("method")] public NamedApiResource Method;

        [JsonProperty("min_level")] public int MinLevel;
    }
}