using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokeApi
{
    public class PokemonSpecies
    {
        [JsonProperty("base_happiness")] public int BaseHappiness;

        [JsonProperty("capture_rate")] public int CaptureRate;

        [JsonProperty("color")] public NamedApiResource Color;

        [JsonProperty("egg_groups")] public List<NamedApiResource> EggGroups;

        [JsonProperty("evolution_chain")] public ApiResource EvolutionChain;

        [JsonProperty("evolves_from_species")] public NamedApiResource EvolvesFromSpecies;

        [JsonProperty("flavor_text_entries")] public List<FlavorText> FlavorTextEntries;

        [JsonProperty("form_descriptions")] public List<Description> FormDescriptions;

        [JsonProperty("forms_switchable")] public bool FormsSwitchable;

        [JsonProperty("gender_rate")] public int GenderRate;

        [JsonProperty("genera")] public List<Genus> Genera;

        [JsonProperty("generation")] public NamedApiResource Generation;

        [JsonProperty("growth_rate")] public NamedApiResource GrowthRate;

        [JsonProperty("habitat")] public NamedApiResource Habitat;

        [JsonProperty("has_gender_differences")]
        public bool HasGenderDifference;

        [JsonProperty("hatch_counter")] public int HatchCounter;

        [JsonProperty("id")] public int Id;

        [JsonProperty("is_baby")] public bool IsBaby;

        [JsonProperty("is_legendary")] public bool IsLegendary;

        [JsonProperty("is_mythical")] public bool IsMythical;

        [JsonProperty("name")] public string Name;

        [JsonProperty("names")] public List<Name> Names;

        [JsonProperty("order")] public int Order;

        [JsonProperty("pal_park_encounters")] public List<PalParkEncounterArea> PalParkEncounters;

        [JsonProperty("pokedex_numbers")] public List<PokemonSpeciesDexEntry> PokedexNumbers;

        [JsonProperty("shape")] public NamedApiResource Shape;

        [JsonProperty("varieties")] public List<PokemonSpeciesVariety> Varieties;
    }

    public class Genus
    {
        [JsonProperty("language")] public NamedApiResource Language;

        [JsonProperty("genus")] public string Text;
    }

    public class PokemonSpeciesDexEntry
    {
        [JsonProperty("entry_number")] public int EntryNumber;

        [JsonProperty("pokedex")] public NamedApiResource Pokedex;
    }

    public class PalParkEncounterArea
    {
        [JsonProperty("area")] public NamedApiResource Area;

        [JsonProperty("base_score")] public int BaseScore;

        [JsonProperty("rate")] public int Rate;
    }

    public class PokemonSpeciesVariety
    {
        [JsonProperty("is_default")] public bool IsDefault;

        [JsonProperty("pokemon")] public NamedApiResource Pokemon;
    }
}