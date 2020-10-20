﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Pokedex.PokeApi.Objects
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class EvolutionChain
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("baby_trigger_item")]
        public NamedAPIResource BabyTriggerItem;

        [JsonProperty("chain")]
        public ChainLink Chain;
    }

    public class ChainLink
    {
        [JsonProperty("is_baby")]
        public bool IsBaby;

        [JsonProperty("species")]
        public NamedAPIResource Species;

        [JsonProperty("evolution_details")]
        public List<EvolutionDetail> EvolutionDetail;

        [JsonProperty("evolves_to")]
        public List<ChainLink> EvolvesTo; 
    }

    public class EvolutionDetail
    {
        [JsonProperty("item")]
        public NamedAPIResource Item;

        [JsonProperty("trigger")]
        public NamedAPIResource Trigger;

        [JsonProperty("gender")]
        public int Gender;

        [JsonProperty("held_item")]
        public NamedAPIResource HeldItem;

        [JsonProperty("known_move")]
        public NamedAPIResource KnownMove;

        [JsonProperty("known_move_type")]
        public NamedAPIResource KnownMoveType;

        [JsonProperty("location")]
        public NamedAPIResource Location;

        [JsonProperty("min_level")]
        public int MinLevel;

        [JsonProperty("min_happiness")]
        public int MinHappiness;

        [JsonProperty("min_beauty")]
        public int MinBeauty;

        [JsonProperty("min_affection")]
        public int MinAffection;

        [JsonProperty("needs_overworld_rain")]
        public bool NeedsOverworldRain;

        [JsonProperty("party_species")]
        public NamedAPIResource PartySpecies;

        [JsonProperty("party_type")]
        public NamedAPIResource PartyType;

        [JsonProperty("relative_physical_stats")]
        public int RelativePhysicalStats;

        [JsonProperty("time_of_day")]
        public string TimeOfDay;

        [JsonProperty("trade_species")]
        public NamedAPIResource TradeSpecies;

        [JsonProperty("turn_upside_down")]
        public bool TurnUpsideDown;
    }
}
