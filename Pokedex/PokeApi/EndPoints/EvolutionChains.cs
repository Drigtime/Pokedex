using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.PokeApi.EndPoints
{
    public class EvolutionChain
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("baby_trigger_item")]
        public NamedApiResource BabyTriggerItem;

        [JsonProperty("chain")]
        public ChainLink Chain;
    }

    public class ChainLink
    {
        [JsonProperty("is_baby")]
        public bool IsBaby;

        [JsonProperty("species")]
        public NamedApiResource Species;

        [JsonProperty("evolution_details")]
        public List<EvolutionDetail> EvolutionDetail;

        [JsonProperty("evolves_to")]
        public List<ChainLink> EvolvesTo;

        public override string ToString()
        {
            return Species.ToString();
        }
    }

    public class EvolutionDetail
    {
        [JsonProperty("item")]
        public NamedApiResource Item;

        [JsonProperty("trigger")]
        public NamedApiResource Trigger;

        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public int Gender;

        [JsonProperty("held_item", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource HeldItem;

        [JsonProperty("known_move", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource KnownMove;

        [JsonProperty("known_move_type", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource KnownMoveType;

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource Location;

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
        public NamedApiResource PartySpecies;

        [JsonProperty("party_type", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource PartyType;

        [JsonProperty("relative_physical_stats", NullValueHandling = NullValueHandling.Ignore)]
        public int RelativePhysicalStats;

        [JsonProperty("time_of_day", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeOfDay;

        [JsonProperty("trade_species", NullValueHandling = NullValueHandling.Ignore)]
        public NamedApiResource TradeSpecies;

        [JsonProperty("turn_upside_down", NullValueHandling = NullValueHandling.Ignore)]
        public bool TurnUpsideDown;
    }
}
