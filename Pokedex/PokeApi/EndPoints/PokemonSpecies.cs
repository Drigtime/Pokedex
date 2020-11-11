using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.PokeApi.EndPoints
{
    public class PokemonSpecies
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("order")]
        public int Order;

        [JsonProperty("gender_rate")]
        public int GenderRate;

        [JsonProperty("capture_rate")]
        public int CaptureRate;

        [JsonProperty("base_happiness")]
        public int BaseHappiness;

        [JsonProperty("is_baby")]
        public bool IsBaby;

        [JsonProperty("is_legendary")]
        public bool IsLegendary;

        [JsonProperty("is_mythical")]
        public bool IsMythical;

        [JsonProperty("hatch_counter")]
        public int HatchCounter;

        [JsonProperty("has_gender_differences")]
        public bool HasGenderDifference;

        [JsonProperty("forms_switchable")]
        public bool FormsSwitchable;

        [JsonProperty("growth_rate")]
        public NamedApiResource GrowthRate;

        [JsonProperty("pokedex_numbers")]
        public List<PokemonSpeciesDexEntry> PokedexNumbers;

        [JsonProperty("egg_groups")]
        public List<NamedApiResource> EggGroups;

        [JsonProperty("color")]
        public NamedApiResource Color;

        [JsonProperty("shape")]
        public NamedApiResource Shape;

        [JsonProperty("evolves_from_species")]
        public NamedApiResource EvolvesFromSpecies;

        [JsonProperty("evolution_chain")]
        public ApiResource EvolutionChain;

        [JsonProperty("habitat")]
        public NamedApiResource Habitat;

        [JsonProperty("generation")]
        public NamedApiResource Generation;

        [JsonProperty("names")]
        public List<Name> Names;

        [JsonProperty("pal_park_encounters")]
        public List<PalParkEncounterArea> PalParkEncounters;

        [JsonProperty("flavor_text_entries")]
        public List<FlavorText> FlavorTextEntries;

        [JsonProperty("form_descriptions")]
        public List<Description> FormDescriptions;

        [JsonProperty("genera")]
        public List<Genus> Genera;

        [JsonProperty("varieties")]
        public List<PokemonSpeciesVariety> Varieties;
    }

    public class Genus
    {
        [JsonProperty("genus")]
        public string Text;

        [JsonProperty("language")]
        public NamedApiResource Language;
    }

    public class PokemonSpeciesDexEntry
    {
        [JsonProperty("entry_number")]
        public int EntryNumber;

        [JsonProperty("pokedex")]
        public NamedApiResource Pokedex;
    }

    public class PalParkEncounterArea
    {
        [JsonProperty("base_score")]
        public int BaseScore;

        [JsonProperty("rate")]
        public int Rate;

        [JsonProperty("area")]
        public NamedApiResource Area;
    }

    public class PokemonSpeciesVariety
    {

        [JsonProperty("is_default")]
        public bool IsDefault;

        [JsonProperty("pokemon")]
        public NamedApiResource Pokemon;
    }
}
