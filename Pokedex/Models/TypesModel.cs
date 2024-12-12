using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Models
{
    public class TypeListResponse
    {
        public List<TypeItem> Results { get; set; } = new();
    }

    public class TypeItem
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class TypeDetailResponse
    {
        public List<TypePokemon> Pokemon { get; set; } = new();
    }

    public class TypePokemon
    {
        public NamedApiResource Pokemon { get; set; } = new();
    }

    public class TypeSlot
    {
        public Type Type { get; set; } = new();
    }

    public class Type
    {
        public string Name { get; set; } = string.Empty;
    }
}

