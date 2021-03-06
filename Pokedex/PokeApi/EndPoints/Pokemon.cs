﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Pokedex.PokeApi.EndPoints
{
    public class PokemonAbility
    {
        [JsonProperty("is_hidden")]
        public bool IsHidden;

        [JsonProperty("slot")]
        public int Slot;

        [JsonProperty("ability")]
        public NamedApiResource Ability;

        public override string ToString()
        {
            return Ability.ToString();
        }
    }

    public class VersionGameIndex
    {
        [JsonProperty("game_index")]
        public int GameIndex;

        [JsonProperty("version")]
        public NamedApiResource Version;
    }

    public class PokemonHeldItemVersion
    {
        [JsonProperty("rarity")]
        public int Rarity;

        [JsonProperty("version")]
        public NamedApiResource Version;
    }

    public class PokemonHeldItem
    {
        [JsonProperty("item")]
        public NamedApiResource Item;

        [JsonProperty("version_details")]
        public List<PokemonHeldItemVersion> VersionDetails;
    }

    public class PokemonMoveVersion
    {
        [JsonProperty("level_learned_at")]
        public int LevelLearnedAt;

        [JsonProperty("version_group")]
        public NamedApiResource VersionGroup;

        [JsonProperty("move_learn_method")]
        public NamedApiResource MoveLearnMethod;
    }

    public class PokemonMove
    {
        [JsonProperty("move")]
        public NamedApiResource Move;

        [JsonProperty("version_group_details")]
        public List<PokemonMoveVersion> VersionGroupDetails;

        public override string ToString()
        {
            return Move.ToString();
        }
    }

    public class PokemonSprites
    {
        [JsonProperty("back_female")]
        public Uri BackFemale;

        [JsonProperty("back_shiny_female")]
        public Uri BackShinyFemale;

        [JsonProperty("back_default")]
        public Uri BackDefault;

        [JsonProperty("front_female")]
        public Uri FrontFemale;

        [JsonProperty("front_shiny_female")]
        public Uri FrontShinyFemale;

        [JsonProperty("back_shiny")]
        public Uri BackShiny;

        [JsonProperty("front_default")]
        public Uri FrontDefault;

        [JsonProperty("front_shiny")]
        public Uri FrontShiny;
    }

    public class PokemonStat
    {
        [JsonProperty("base_stat")]
        public int BaseStat;

        [JsonProperty("effort")]
        public int Effort;

        [JsonProperty("stat")]
        public NamedApiResource Stat;
    }

    public class PokemonType
    {
        [JsonProperty("slot")]
        public int Slot;

        [JsonProperty("type")]
        public NamedApiResource Type;

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("base_experience")]
        public int BaseExperience;

        [JsonProperty("height")]
        public int Height;

        [JsonProperty("is_default")]
        public bool IsDefault;

        [JsonProperty("order")]
        public int Order;

        [JsonProperty("weight")]
        public int Weight;

        [JsonProperty("abilities")]
        public List<PokemonAbility> Abilities;

        [JsonProperty("forms")]
        public List<NamedApiResource> Forms;

        [JsonProperty("game_indices")]
        public List<VersionGameIndex> GameIndices;

        [JsonProperty("held_items")]
        public List<PokemonHeldItem> HeldItems;

        [JsonProperty("location_area_encounters")]
        public Uri LocationAreaEncounters;

        [JsonProperty("moves")]
        public List<PokemonMove> Moves;

        [JsonProperty("species")]
        public NamedApiResource Species;

        [JsonProperty("sprites")]
        public PokemonSprites Sprites;

        [JsonProperty("stats")]
        public List<PokemonStat> Stats;

        [JsonProperty("types")]
        public List<PokemonType> Types;

    }
}
