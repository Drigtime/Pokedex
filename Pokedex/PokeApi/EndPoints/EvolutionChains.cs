﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.PokeApi.EndPoints
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

        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public int Gender;

        [JsonProperty("held_item", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource HeldItem;

        [JsonProperty("known_move", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource KnownMove;

        [JsonProperty("known_move_type", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource KnownMoveType;

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource Location;

        [JsonProperty("min_level", NullValueHandling = NullValueHandling.Ignore)]
        public int MinLevel;

        [JsonProperty("min_happiness", NullValueHandling = NullValueHandling.Ignore)]
        public int MinHappiness;

        [JsonProperty("min_beauty", NullValueHandling = NullValueHandling.Ignore)]
        public int MinBeauty;

        [JsonProperty("min_affection", NullValueHandling = NullValueHandling.Ignore)]
        public int MinAffection;

        [JsonProperty("needs_overworld_rain", NullValueHandling = NullValueHandling.Ignore)]
        public bool NeedsOverworldRain;

        [JsonProperty("party_species", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource PartySpecies;

        [JsonProperty("party_type", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource PartyType;

        [JsonProperty("relative_physical_stats", NullValueHandling = NullValueHandling.Ignore)]
        public int RelativePhysicalStats;

        [JsonProperty("time_of_day", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeOfDay;

        [JsonProperty("trade_species", NullValueHandling = NullValueHandling.Ignore)]
        public NamedAPIResource TradeSpecies;

        [JsonProperty("turn_upside_down", NullValueHandling = NullValueHandling.Ignore)]
        public bool TurnUpsideDown;
    }
}