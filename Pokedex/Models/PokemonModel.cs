using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Pokedex.Models
{
    public class PokemonResponse
    {
        public string Name { get; set; } = string.Empty;
        public Sprites Sprites { get; set; } = new Sprites();
    }

    public class Sprites
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; } = string.Empty;
    }

    public class Pokemon
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
