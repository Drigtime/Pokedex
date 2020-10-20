using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Pokedex.PokeApi.Objects
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class EvolutionChain
    {
        [JsonProperty("id")]
        public int id;

        [JsonProperty("baby_trigger_item")]
        public NamedAPIResource babyTriggerItem;

        [JsonProperty("chain")]
        public ChainLink chain;
    }

    public class ChainLink
    {
        [JsonProperty("is_baby")]
        public bool isBaby;

        [JsonProperty("species")]
        public NamedAPIResource species;

        [JsonProperty("evolution_details")]
        public List<EvolutionDetail> evolutionDetail;

        [JsonProperty("evolves_to")]
        public List<ChainLink> evolvesTo; 
    }

    public class EvolutionDetail
    {
        [JsonProperty("item")]
        public NamedAPIResource item;

        [JsonProperty("trigger")]
        public NamedAPIResource trigger;

        [JsonProperty("gender")]
        public int gender;

        [JsonProperty("held_item")]
        public NamedAPIResource heldItem;

        [JsonProperty("known_move")]
        public NamedAPIResource knownMove;

        [JsonProperty("known_move_type")]
        public NamedAPIResource knownMoveType;

        [JsonProperty("location")]
        public NamedAPIResource location;

        [JsonProperty("min_level")]
        public int minLevel;

        [JsonProperty("min_happiness")]
        public int minHappiness;

        [JsonProperty("min_beauty")]
        public int minBeauty;

        [JsonProperty("min_affection")]
        public int minAffection;

        [JsonProperty("needs_overworld_rain")]
        public bool needsOverworldRain;

        [JsonProperty("party_species")]
        public NamedAPIResource partySpecies;

        [JsonProperty("party_type")]
        public NamedAPIResource partyType;

        [JsonProperty("relative_physical_stats")]
        public int relativePhysicalStats;

        [JsonProperty("time_of_day")]
        public string timeOfDay;

        [JsonProperty("trade_species")]
        public NamedAPIResource tradeSpecies;

        [JsonProperty("turn_upside_down")]
        public bool turnUpsideDown;
    }
}
