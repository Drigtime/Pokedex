using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pokedex.PokeApi.EndPoints
{
    public class LocationAreaEncounter
    {
        [JsonProperty("location_area")]
        public NamedApiResource LocationArea;

        [JsonProperty("version_details")]
        public List<VersionEncounterDetail> VersionDetails;

        public override string ToString()
        {
            return LocationArea.ToString();
        }
    }

    public class VersionEncounterDetail
    {
        [JsonProperty("version")]    
        public NamedApiResource Version;
        
        [JsonProperty("max_chance")]        
        public int MaxChance;
        
        [JsonProperty("encounter_details")]        
        public List<Encounter> EncounterDetails;
        
    }

    public class Encounter
    {
        [JsonProperty("min_level")]    
        public int MinLevel;
        
        [JsonProperty("max_level")]    
        public int MaxLevel;
        
        [JsonProperty("condition_values")]    
        public List<NamedApiResource> ConditionValues;
        
        [JsonProperty("chance")]    
        public int Chance;
        
        [JsonProperty("method")]    
        public NamedApiResource Method;
    }
}