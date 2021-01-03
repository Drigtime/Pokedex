using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeApi
{
    public class EvolutionChain
    {
        [JsonProperty("baby_trigger_item")] public NamedApiResource BabyTriggerItem;

        [JsonProperty("chain")] public ChainLink Chain;

        [JsonProperty("id")] public int Id;
    }

    public class ChainLink
    {
        [JsonProperty("evolution_details")] public List<EvolutionDetail> EvolutionDetail;

        [JsonProperty("evolves_to")] public List<ChainLink> EvolvesTo;

        [JsonProperty("is_baby")] public bool IsBaby;

        [JsonProperty("species")] public NamedApiResource Species;

        public override string ToString()
        {
            return Species.ToString();
        }
    }

    public class EvolutionDetail
    {
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public int Gender;

        [JsonProperty("held_item", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource HeldItem;

        [JsonProperty("item")] public NamedApiResource Item;

        [JsonProperty("known_move", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource KnownMove;

        [JsonProperty("known_move_type", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource KnownMoveType;

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource Location;

        [JsonProperty("min_affection", NullValueHandling = NullValueHandling.Ignore)]
        public int MinAffection;

        [JsonProperty("min_beauty", NullValueHandling = NullValueHandling.Ignore)]
        public int MinBeauty;

        [JsonProperty("min_happiness", NullValueHandling = NullValueHandling.Ignore)]
        public int MinHappiness;

        [JsonProperty("min_level", NullValueHandling = NullValueHandling.Ignore)]
        public int MinLevel;

        [JsonProperty("needs_overworld_rain", NullValueHandling = NullValueHandling.Ignore)]
        public bool NeedsOverworldRain;

        [JsonProperty("party_species", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource PartySpecies;

        [JsonProperty("party_type", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource PartyType;

        [JsonProperty("relative_physical_stats", NullValueHandling = NullValueHandling.Ignore)]
        public int RelativePhysicalStats;

        [JsonProperty("time_of_day", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeOfDay;

        [JsonProperty("trade_species", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource TradeSpecies;

        [JsonProperty("trigger")] public NamedApiResource Trigger;

        [JsonProperty("turn_upside_down", NullValueHandling = NullValueHandling.Ignore)]
        public bool TurnUpsideDown;
    }
}