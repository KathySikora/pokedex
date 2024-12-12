using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class MetaService
    {
        private readonly HttpClient _httpClient;

        public MetaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TypeItem>> GetTypesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<TypeListResponse>("https://pokeapi.co/api/v2/type?limit=25&offset=0");
            if (response?.Results == null) return new List<TypeItem>();

            return response.Results.Select(type => new TypeItem
            {
                Name = type.Name,
                Url = type.Url
            }).ToList();
        }

        public async Task<List<AbilityItem>> GetAbilitiesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<AbilityListResponse>("https://pokeapi.co/api/v2/ability?limit=400&offset=0");
            if (response?.Results == null) return new List<AbilityItem>();

            return response.Results.Select(ability => new AbilityItem
            {
                Name = ability.Name,
                Url = ability.Url
            }).ToList();
        }
    }
}
