using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Pokedex.Models;

namespace Pokedex.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Types { get; set; } = new();
        public List<string> Abilities { get; set; } = new();
        public int Height { get; set; }
        public int Weight { get; set; }
    }

    public class PokemonDetail
    {
        public string Name { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<string> Abilities { get; set; } = new();
        public List<string> Types { get; set; } = new();
    }

    public class PokemonDetailResponse
    {
        public string Name { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<AbilitySlot> Abilities { get; set; } = new();
        public List<TypeSlot> Types { get; set; } = new();
    }

    public class PokemonListResponse
    {
        public List<PokemonListItem> Results { get; set; } = new();
    }

    public class PokemonListItem
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
