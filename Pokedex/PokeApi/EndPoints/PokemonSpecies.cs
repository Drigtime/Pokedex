﻿using Newtonsoft.Json;
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
        public NamedAPIResource GrowthRate;

        [JsonProperty("pokedex_numbers")]
        public List<PokemonSpeciesDexEntry> PokedexNumbers;

        [JsonProperty("egg_groups")]
        public NamedAPIResource EggGroups;

        [JsonProperty("color")]
        public NamedAPIResource Color;

        [JsonProperty("shape")]
        public NamedAPIResource Shape;

        [JsonProperty("evolves_from_species")]
        public NamedAPIResource EvolvesFromSpecies;

        [JsonProperty("evolution_chain")]
        public APIResource EvolutionChain;

        [JsonProperty("habitat")]
        public NamedAPIResource habitat;

        [JsonProperty("generation")]
        public NamedAPIResource Generation;

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
        public string Genus;

        [JsonProperty("language")]
        public NamedAPIResource Language;
    }

    public class PokemonSpeciesDexEntry
    {
        [JsonProperty("entry_number")]
        public int EntryNumber;

        [JsonProperty("pokedex")]
        public NamedAPIResource Pokedex;
    }

    public class PalParkEncounterArea
    {
        [JsonProperty("base_score")]
        public int BaseScore;

        [JsonProperty("rate")]
        public int Rate;

        [JsonProperty("area")]
        public NamedAPIResource Area;
    }

    public class PokemonSpeciesVariety
    {

        [JsonProperty("is_default")]
        public bool IsDefault;

        [JsonProperty("pokemon")]
        public NamedAPIResource Pokemon;
    }
}
