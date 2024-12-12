using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class AbilityListResponse
    {
        public List<AbilityItem> Results { get; set; } = new();
    }

    public class AbilityItem
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class AbilityDetailResponse
    {
        public List<AbilityPokemon> Pokemon { get; set; } = new();
    }

    public class AbilityPokemon
    {
        public NamedApiResource Pokemon { get; set; } = new();
    }

    public class AbilitySlot
    {
        public Ability Ability { get; set; } = new();
    }

    public class Ability
    {
        public string Name { get; set; } = string.Empty;
    }
}

